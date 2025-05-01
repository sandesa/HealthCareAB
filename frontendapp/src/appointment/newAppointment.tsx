import React, { useState, useEffect } from 'react';
import api from '../api';
import './newAppointment.css';

interface Response {
    data: any;
    message: string;
    isSuccess: boolean;
}

interface Appointment {
    careGiverId: number;
    meetingDate?: string;
    meetingType?: string;
    clinic?: string;
    address?: string;
}

interface Availability {
    id: number;
    caregiverId: number;
    startTime: string;
    endTime: string;
    notes: string;
}

const AppointmentForm: React.FC = () => {
    const [caregiverId, setCaregiverId] = useState(0);
    const [meetingDate, setMeetingDate] = useState<string>('');
    const [meetingType, setMeetingType] = useState<string>('In person');
    const [clinic, setClinic] = useState<string>('TestClinic');
    const [address, setAddress] = useState<string>('Testgatan 1');
    const [appointmentData, setAppointmentData] = useState<Appointment>();
    const [error, setError] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(true);
    const [message, setMessage] = useState<string | null>(null);
    const [availableCaregivers, setAvailableCaregivers] = useState<Availability[]>([]);
    const [selectedSlot, setSelectedSlot] = useState<Availability | null>(null);
    const [incDate, setIncDate] = useState<number>(0);
    const [selectedDate, setSelectedDate] = useState<Date>(new Date());

    useEffect(() => {
        const loadAvailableCaregivers = async () => {
            try {
                const updatedDate = new Date();
                if (incDate !== 0) {
                    updatedDate.setMonth(updatedDate.getMonth() + incDate);
                    updatedDate.setDate(1);
                }

                setSelectedDate(updatedDate);

                const response = await api.get<Response>(
                    `api/availability/get/from/${updatedDate.getFullYear()}-${updatedDate.getMonth() + 1}-${updatedDate.getDate()}`
                );

                if (response.data.isSuccess) {
                    setAvailableCaregivers(response.data.data);
                }

                setMessage(response.data.message);
            } catch (error: any) {
                console.error('Error fetching available caregivers:', error);
                setError('Failed to load available caregivers. Please try again later.');
            } finally {
                setLoading(false);
            }
        };

        loadAvailableCaregivers();
    }, [incDate]);


    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        setLoading(true);
        setError('');

        try {
            const response = await api.post<Response>('api/booking/create', {
                caregiverId,
                meetingDate,
                meetingType,
                clinic,
                address,
            });
            if (response.data.isSuccess) {
                setMessage(response.data.message);
                setAppointmentData(response.data.data);
            } else {
                setError(response.data.message);
            }
        } catch (error: any) {
            console.error('Error creating appointment:', error);
            setError('Failed to create appointment. Please try again later.');
        } finally {
            setLoading(false);
        }
    };

    const handleSelectedSlot = (slot: Availability) => {
        setSelectedSlot(slot);
        setCaregiverId(slot.caregiverId);
        setMeetingDate(slot.startTime?.slice(0, 16) || '');
    };

    return (
        <div className="appointment-container">
            <div className="appointment-form">
                <h2>Create New Appointment</h2>
                <form onSubmit={handleSubmit}>
                    <label>
                        Meeting Date:
                        <input
                            type="datetime-local"
                            value={meetingDate}
                            onChange={(e) => setMeetingDate(e.target.value)}
                        />
                    </label>
                    <label>
                        Meeting Type:
                        <input
                            type="text"
                            value={meetingType}
                            onChange={(e) => setMeetingType(e.target.value)}
                        />
                    </label>
                    <label>
                        Clinic:
                        <input
                            type="text"
                            value={clinic}
                            onChange={(e) => setClinic(e.target.value)}
                        />
                    </label>
                    <label>
                        Address:
                        <input
                            type="text"
                            value={address}
                            onChange={(e) => setAddress(e.target.value)}
                        />
                    </label>
                    <button type="submit" disabled={loading}>
                        {loading ? 'Creating Appointment...' : 'Create Appointment'}
                    </button>
                </form>
                {message && <div className="message">{message}</div>}
                {error && <div className="error">{error}</div>}
            </div>

            <div className="calendar-view">
                <div className="calendar">
                <h2>Available Caregivers from {selectedDate.toDateString()}</h2>
                    {loading ? (
                        <p>Loading available caregivers...</p>
                    ) : (
                        Array.isArray(availableCaregivers) && availableCaregivers.length > 0 ? (
                            availableCaregivers.map((slot) => (
                                <div
                                    key={slot.id}
                                    className="caregiver-slot"
                                    onClick={() => handleSelectedSlot(slot)}
                                >
                                    <p>{new Date(slot.startTime!).toLocaleString()}</p>
                                    <p>{slot.notes}</p>
                                </div>
                            ))
                            ) : (
                                <p>No available caregivers</p>
                        )
                    )}
                    <button onClick={() => setIncDate((prev) => Math.max(prev - 1, 0))}>
                        Previous Month
                    </button>
                    <button onClick={() => setIncDate((prev) => prev + 1)}>
                        Next Month
                    </button>
                </div>
                {selectedSlot && (
                    <div className="selected-slot">
                        <h3>Selected Time Slot:</h3>
                        <p>Caregiver: {selectedSlot.caregiverId}</p>
                        <p>
                            Time: {new Date(selectedSlot.startTime!).toLocaleString()} -{' '}
                            {new Date(selectedSlot.endTime!).toLocaleString()}
                        </p>
                    </div>
                )}
            </div>
        </div>
    );
};

export default AppointmentForm;
