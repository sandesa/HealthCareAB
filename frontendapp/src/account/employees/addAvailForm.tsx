import React, { useState, useEffect } from 'react';
import api from '../../api';

interface Response {
    data: Availability;
    message: string;
    isSuccess: boolean;
}

interface AvailabilityCreate {
    startTime: string;
    endTime: string;
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
    const [startTime, setStartDate] = useState<string>('');
    const [endTime, setEndDate] = useState<string>('');
    const [notes, setNotes] = useState<string>('');
    const [availsData, setAvailsData] = useState<Availability | null>(null);
    const [error, setError] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(false);
    const [message, setMessage] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        const availability: AvailabilityCreate = {
            startTime,
            endTime,
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
                        value={startTime}
                        onChange={(e) => setStartDate(e.target.value)}
                        required
                    />
                </div>

                <div className="form-group">
                    <label htmlFor="endDate">End Date</label>
                    <input
                        type="datetime-local"
                        id="endDate"
                        name="endDate"
                        value={endTime}
                        onChange={(e) => setEndDate(e.target.value)}
                        required
                    />
                </div>

                <div className="form-group">
                    <label htmlFor="notes">Notes (optional)</label>
                    <textarea
                        id="notes"
                        name="notes"
                        placeholder="Add any notes"
                        value={notes}
                        onChange={(e) => setNotes(e.target.value)}
                    />
                </div>

                <button type="submit">Submit</button>
            </form>
        </div>
    );
};

export default AddAvailForm;
