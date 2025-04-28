import React from 'react';
import './accountSidebar.css';

function AccountSidebar() {
    return (
        <div className="account-sidebar">
            <ul>
                <li><a href="#personal-info">Personal Information</a></li>
                <li><a href="#previous-appointments">Previous Appointments</a></li>
                <li><a href="#update-info">Update Information</a></li>
                <li><a href="#logout">Logout</a></li>
            </ul>
        </div>
    );
}

export default AccountSidebar;