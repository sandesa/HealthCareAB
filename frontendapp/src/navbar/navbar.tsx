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

    const handleLogin = () => {
        window.location.href = '/login.html';
    }

    const handleAccount = () => {
        window.location.href = '/accountInfo.html';
    }

    return (
        <nav className="navbar">
            <div className="navbar-container">
                {/*<div className="navbar-special-links-container">*/}
                {/*    <a href="#employees" className="navbar-employees-link">Employees</a>*/}
                {/*</div>*/}
                <div className="navbar-top-container">
                    <div className="navbar-logo"><a href="/">HealthCareAB</a></div>
                    <div className="navbar-account-btns">
                        {!isLoggedIn ? (
                            <button className="sign-up-btn">Sign up</button>
                        ) : (
                            <button className="sign-up-btn" onClick={handleAccount} >My account</button>
                        ) }

                        {!isLoggedIn ? (
                            <button onClick={handleLogin} className="sign-in-btn">Sign in</button>
                        ) : (
                            <button onClick={handleLogout} className="sign-in-btn">Sign out</button>
                        )}
                    </div>
                </div>
                <div className="navbar-bottom-container">
                    <ul className="nav-links">
                        {isLoggedIn ? (
                            <li><a href="/appointmentNew.html">Book appointment</a></li>
                        ) : (
                            <li><a href="/login.html">Book appointment</a></li>
                        )}
                        {isLoggedIn ? (
                            <li><a href="#services">Calendar</a></li>
                        ) : (
                            <li><a href="/login.html">Calendar</a></li>
                        )}
                        {isLoggedIn &&
                            <li><a href="#contact">Contact</a></li>
                        }
                    </ul>
                    <div className="search-bar-container">
                        <input
                            type="text"
                            className="search-input"
                            placeholder="Explore topics and care"
                        />
                        <button className="search-button">Search</button>
                    </div>
                </div>
            </div>
        </nav>
    );
}

export default Navbar;
