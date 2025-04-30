import React, { useState, useEffect } from 'react';
import api from '../../api';

interface Response {
    data: Availability;
    message: string;
    isSuccess: boolean;
}

interface AvailabilityCreate {
    startDate: Date;
    endDate: Date;
    notes?: string;
}

interface Availability {
    id: number;
    caregiverId: number;
    startTime: Date;
    endTime: Date;
    notes: string;
}

const AddAvailForm: React.FC = () => {
    const [availsData, setAvailsData] = useState<Availability | null>(null);
    const [error, setError] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(false);
    const [message, setMessage] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        const form = e.currentTarget as HTMLFormElement;
        const formData = new FormData(form);
        const startDate = formData.get('startDate') as string;
        const endDate = formData.get('endDate') as string;
        const notes = formData.get('notes') as string;

        const availability: AvailabilityCreate = {
            startDate: new Date(startDate),
            endDate: new Date(endDate),
            notes,
        };

        setLoading(true);
        try {
            const response = await api.post<Response>('api/availability/create', availability);
            if (response.status === 200) {
                setMessage(response.data.message);
                setAvailsData(response.data.data);
            } else {
                setError(response.data.message);
            }
        } catch (error: any) {
            console.error('Error creating availability:', error);
            setError('Failed to create availability. Please try again later.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="add-avail-form">
            <h2>Add Availability</h2>

            {loading && <p>Loading...</p>}

            {error && <p className="error">{error}</p>}

            {message && <p className="success">{message}</p>}

            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="startDate">Start Date</label>
                    <input
                        type="datetime-local"
                        id="startDate"
                        name="startDate"
                        required
                    />
                </div>

                <div className="form-group">
                    <label htmlFor="endDate">End Date</label>
                    <input
                        type="datetime-local"
                        id="endDate"
                        name="endDate"
                        required
                    />
                </div>

                <div className="form-group">
                    <label htmlFor="notes">Notes (optional)</label>
                    <textarea
                        id="notes"
                        name="notes"
                        placeholder="Add any notes"
                    />
                </div>

                <button type="submit">Submit</button>
            </form>

            {availsData && (
                <div className="submitted-data">
                    <h3>Availability Created</h3>
                    <p><strong>Start Time:</strong> {new Date(availsData.startTime).toLocaleString()}</p>
                    <p><strong>End Time:</strong> {new Date(availsData.endTime).toLocaleString()}</p>
                    <p><strong>Notes:</strong> {availsData.notes}</p>
                </div>
            )}
        </div>
    );
};

export default AddAvailForm;
