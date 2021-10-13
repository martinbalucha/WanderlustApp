import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import './navbar.css';
import Dropdown from '../dropdown/dropdown';

function Navbar() {
  const [click, setClick] = useState(false);
  const [dropdown, setDropdown] = useState(false);

  const handleClick = () => setClick(!click);
  const closeMobileMenu = () => setClick(false);

  const onMouseEnter = () => {
    if (window.innerWidth < 960) {
      setDropdown(false);
    } else {
      setDropdown(true);
    }
  };

  const onMouseLeave = () => {
    if (window.innerWidth < 960) {
      setDropdown(false);
    } else {
      setDropdown(false);
    }
  };

  const data = [
    {
        "path": "/Travel",
        "title": "Travel"
    },
    {
        "path": "/Country",
        "title": "Countries"
    }
  ]

  return (
    <>
      <nav className='navbar'>
        <Link to='/' className='navbar-logo' onClick={closeMobileMenu}>
          Wanderlust
        </Link>
        <div className='menu-icon' onClick={handleClick}>
          <i className={click ? 'fas fa-times' : 'fas fa-bars'} />
        </div>
        <ul className={click ? 'nav-menu active' : 'nav-menu'}>
          <li className='nav-item'>
            <Link to='/' className='nav-links' onClick={closeMobileMenu}>
              Home
            </Link>
          </li>
          <li className='nav-item' onMouseEnter={onMouseEnter} onMouseLeave={onMouseLeave}>
            <Link to='/services' className='nav-links' onClick={closeMobileMenu}>
              Services <i className='fas fa-caret-down' />              
            </Link>
            {dropdown && <Dropdown data={data} />}
          </li>
          <li className='nav-item'>
            <Link to='/products' className='nav-links' onClick={closeMobileMenu}>
                Products
            </Link>
          </li>
          <li className='nav-item'>
            <Link to='/contact-us' className='nav-links' onClick={closeMobileMenu}>
                Contact Us
            </Link>
          </li>
        </ul>
        <ul className='nav-admin-menu'>
          <div className='nav-user'>
            <i className='fa fa-user-circle fa-2x' aria-hidden='true'></i>
          </div>
        </ul>        
      </nav>
    </>
  );
}

export default Navbar;