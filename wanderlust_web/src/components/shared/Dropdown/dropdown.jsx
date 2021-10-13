import { useState } from "react";
import { Link } from 'react-router-dom';
import './dropdown.css'

function Dropdown({data}) {
    const [click, setClick] = useState(false);
  
    const handleClick = () => setClick(!click);
  
    return (
      <>
        <ul onClick={handleClick} className={click ? 'dropdown-menu clicked' : 'dropdown-menu'}
        >
          {data.map((item, index) => {
            return (
              <li key={index}>
                <Link className="dropdown-link" to={item.path} onClick={() => setClick(false)}>
                  {item.title}
                </Link>
              </li>
            );
          })}
        </ul>
      </>
    );
  }
  
  export default Dropdown;