import './navbar.css';

function Navbar() {
    return (
        <nav className="navbar">
            <div className="navbar-container">
                <div className="navbar-logo"><a href="/">HealthCareAB</a></div>
                <ul className="nav-links">
                    <li><a href="#about">Book appointment</a></li>
                    <li><a href="#services">Calendar</a></li>
                    <li><a href="#contact">Contact</a></li>
                    <li><a href="/login.html">Login</a></li>
                </ul>
            </div>
        </nav>
    );
}

export default Navbar;
