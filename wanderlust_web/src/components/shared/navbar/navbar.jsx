import { Link } from "react-router-dom";
import React, { useState, useEffect } from 'react';
import './navbar.css'

function Navbar () {
    const [click, setClick] = useState(false);

    const handleClick = () => setClick(!click);
    const closeMobileMenu = () => setClick(false);

    return (
    <nav className="navbar">
        <div className="navbar-container">
            <Link to="/" className="navbar-logo">
                Wanderlust
            </Link>
            <div className="menu-icon" onClick={handleClick}>
                <i className={click ? "fas fa-times" : "fas fa-bars"} />                        
            </div>
            <ul className={click ? "nav-menu active" : "nav-menu"}>
                <li className="nav-item">
                    <Link to="/" className="nav-links" onClick={closeMobileMenu}>
                        Home
                    </Link>
                </li>
                <li className="nav-item">
                    <Link to="/Travel" className="nav-links">
                        Countries
                    </Link>
                </li>
                <li className="nav-item">
                    <Link to="/Travel" className="nav-links">
                        Regions
                    </Link>
                </li>
            </ul>
            <ul className="nav-admin-menu">
                <li className="nav-dropdown">

                </li>
            </ul>
        </div>
    </nav>);
};

export default Navbar;