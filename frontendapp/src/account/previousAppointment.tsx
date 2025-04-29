import React, { useState, useEffect } from 'react';
import api from '../api';
import './previousAppointment.css';
import Cookies from 'js-cookie';

interface Response {
    data: Appointment[];
    message: string;
    isSuccess: boolean;
}

interface Appointment {
    id: number;
    careGiverId: number;
    patientId: number;
    meetingDate?: Date | null;
    meetingType?: string;
    clinic?: string;
    address?: string;
    isCancelled: boolean;
    cancelDate?: Date | null;
    created: Date | null;
    updated?: Date | null;
}

const PreviousAppointment: React.FC = () => {
    const [appointmentData, setAppointmentData] = useState<Appointment[]>([]);
    const [error, setError] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(true);
    const [message, setMessage] = useState<string | null>(null);
    useEffect(() => {
        const loadAppointmentData = async () => {
            try {
                const response = await api.get<Response>(`api/booking/user`);

                if (response.status === 200) {
                    setMessage(response.data.message);
                    setAppointmentData(response.data.data);
                } else {
                    setError(response.data.message);
                }
            } catch (error: any) {
                console.error('Error fetching appointment data:', error);
                setError('Failed to load appointment information. Please try again later.');
            } finally {
                setLoading(false);
            }
        };
        loadAppointmentData();
    }, []);

    return (
        <div className="pre-appiontment-container">
            <h2>Previous appointments:</h2>

            {loading && <p>Loading previous appointments...</p>}

            {error && <p className="error">{error}</p>}

            {message && <p className="message">{message}</p>}

            {!appointmentData && <p>No previous appointments found.</p>}

            {appointmentData && appointmentData.map((appointment) => (
                <div key={appointment.id} className="appointment-card">
                    <p>Caregiver ID: {appointment.careGiverId}</p>
                    <p>Patient ID: {appointment.patientId}</p>
                    <p>Meeting Date: {appointment.meetingDate ? new Date(appointment.meetingDate).toLocaleString() : 'N/A'}</p>
                    <p>Meeting Type: {appointment.meetingType || 'N/A'}</p>
                    <p>Clinic: {appointment.clinic || 'N/A'}</p>
                    <p>Address: {appointment.address || 'N/A'}</p>
                    <p>Status: {appointment.isCancelled ? 'Cancelled' : 'Active'}</p>
                </div>
            ))}
        </div>
    );
}

export default PreviousAppointment;
