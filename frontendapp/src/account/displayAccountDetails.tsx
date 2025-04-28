import React, { useState, useEffect } from 'react';
import api from '../api';
import './displayAccountDetails.css';
import Cookies from 'js-cookie';

interface Response {
    data: User;
    message: string;
    isSuccess: boolean;
}

interface User {
    id: number;
    email: string;
    phoneNumber?: string;
    firstName: string;
    lastName: string;
    dateOfBirth: Date;
    userType?: string;
    userAccountType?: string;
    createdAt?: Date | null;
    updatedAt?: Date | null;
}

const AccountInformation: React.FC = () => {
    const [userData, setUserData] = useState<User | null>(null);
    const [error, setError] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(true);
    const [message, setMessage] = useState<string | null>(null);

    useEffect(() => {
        const loadUserBookingData = async () => {
            try {
                const id = Cookies.get('user_id');

                const response = await api.get<Response>(`api/user/get/${id}`);

                if (response.status === 200) {
                    setMessage(response.data.message);
                    setUserData(response.data.data);
                } else {
                    setError(response.data.message);
                }
            } catch (error: any) {
                console.error('Error fetching user data:', error);
                setError('Failed to load user information. Please try again later.');
            } finally {
                setLoading(false);
            }
        };

        loadUserBookingData();
    }, []);

    return (
        <div className="account-container">
            <h2>Account Information</h2>

            {loading && <p>Loading your information...</p>}

            {error && <p className="error">{error}</p>}

            {message && <p className="message">{message}</p>}

            {userData && (
                <div className="user-info">
                    <p><strong>First Name:</strong> {userData.firstName}</p>
                    <p><strong>Last Name:</strong> {userData.lastName}</p>
                    <p><strong>Email:</strong> {userData.email}</p>
                    <p><strong>Phone Number:</strong> {userData.phoneNumber ?? "No data"}</p>
                    <p><strong>Date of Birth:</strong> {userData.dateOfBirth.toString().slice(0, 10)}</p>
                    <p><strong>User Type:</strong> {userData.userType ?? "No data"}</p>
                    <p><strong>Account Type:</strong> {userData.userAccountType ?? "No data"}</p>
                    <p><strong>Created:</strong> {userData.createdAt?.toString().slice(0, 10) ?? "No data"}</p>
                </div>
            )}
        </div>
    );
};

export default AccountInformation;
