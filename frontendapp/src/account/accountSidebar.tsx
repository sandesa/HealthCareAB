import React, { useState, useEffect } from 'react';
import './accountSidebar.css';
import Cookies from 'js-cookie';

const AccountSidebar: React.FC = () => {
    const [userType, setUserType] = useState<string>('');

    useEffect(() => {
        const userTypeCookie = Cookies.get('user_type');
        if (userTypeCookie) {
            setUserType(userTypeCookie);
        }
    }, []);

    return (
        <div className="account-sidebar">
            <ul>
                <li><a href="/accountInfo.html">Account Information</a></li>
                <li><a href="/accountPreAppointment.html">Previous Appointments</a></li>
                <li><a href="#update-info">Update Information</a></li>
                {(userType === 'Developer' || userType === 'Admin' || userType === 'Caregiver') && (
                    <>
                        <li><a href="/accountViewAvails.html">View your availabilities</a></li>
                        <li><a href="/accountAddAvail.html">Add new availability</a></li>
                        <li><a href="#other">Other link</a></li>
                    </>
                )}
            </ul>
        </div>
    );
}

export default AccountSidebar;