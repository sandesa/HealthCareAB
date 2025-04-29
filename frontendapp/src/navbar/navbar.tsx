import './navbar.css';
import api from '../api';
import Cookies from 'js-cookie';
import React, { useEffect, useState } from 'react';

const Navbar: React.FC = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    useEffect(() => {
        const loggedInStatus = Cookies.get('logged_in') === 'true';
        setIsLoggedIn(loggedInStatus);
    }, []);

    const handleLogout = async () => {
        try {
            const response = await api.post('api/logout');
            Cookies.set('logged_in', 'false');
            setIsLoggedIn(false);
            window.location.href = '/index.html';
        } catch {
            console.error('An error occurred when trying to logout!');
        }
    }

    return (
        <nav className="navbar">
            <div className="navbar-container">
                <div className="navbar-logo"><a href="/">HealthCareAB</a></div>
                <ul className="nav-links">
                    <li><a href="/appointmentNew.html">Book appointment</a></li>
                    <li><a href="#services">Calendar</a></li>
                    <li><a href="#contact">Contact</a></li>
                    {isLoggedIn &&
                    <li><a href="/accountHome.html">Account</a></li>
                    }
                    {!isLoggedIn ? (
                        <li><a href="/login.html">Login</a></li>
                    ) : (
                        <li><a onClick={handleLogout} className="logout-button">Logout</a></li>
                    )}
                </ul>
            </div>
        </nav>
    );
}

export default Navbar;
