import React, { useState, useEffect } from 'react';
import api from '../../api';

interface Response {
    data: Availability[];
    message: string;
    isSuccess: boolean;
}

interface Availability {
    id: number;
    caregiverId: number;
    startTime: Date;
    endTime: Date;
    notes: string;
}

const ViewAvails: React.FC = () => {
    const [availsData, setAvailsData] = useState<Availability[]>([]);
    const [error, setError] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(true);
    const [message, setMessage] = useState<string | null>(null);
    useEffect(() => {
        const loadAvailsData = async () => {
            try {
                const response = await api.get<Response>(`api/availability/caregiver`);

                if (response.status === 200) {
                    setMessage(response.data.message);
                    setAvailsData(response.data.data);
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
        loadAvailsData();
    }, []);

    const handleDelete = async (id: number) => {
        setLoading(true);
        try {
            const response = await api.delete<Response>(`api/availability/delete/${id}`);
            if (response.status === 200) {
                setMessage(response.data.message);
                setAvailsData((prevData) => prevData.filter((avail) => avail.id !== id));
            } else {
                setError(response.data.message);
            }
        } catch (error: any) {
            console.error('Error deleting availability:', error);
            setError('Failed to delete availability. Please try again later.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="avails-container">
            <h2>Your availabilites:</h2>

            {loading && <p>Loading availabilites...</p>}

            {error && <p className="error">{error}</p>}

            {!availsData && <p>No avails found.</p>}

            {availsData && availsData.length > 0 && availsData.map((avails) => (
                <div key={avails.id} className="avails-card">
                    <p>Caregiver ID: {avails.caregiverId}</p>
                    {avails.startTime && <p>Start: {new Date(avails.startTime).toISOString()}</p>}
                    {avails.endTime && <p>End: {new Date(avails.endTime).toISOString()}</p>}
                    <p>Notes: {avails.notes}</p>
                    <button className="avails-delete-btn" onClick={() => handleDelete(avails.id)}>Delete</button>
                </div>
            ))}
        </div>
    );
}

export default ViewAvails;
