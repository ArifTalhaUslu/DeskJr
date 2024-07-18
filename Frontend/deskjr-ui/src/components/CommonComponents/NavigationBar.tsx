import React from "react";
import { Link } from "react-router-dom";

interface NavigationBarProps {
  brand: { name: string; to: string };
  links: { name: string; to: string }[];
}

const NavigationBar: React.FC<NavigationBarProps> = ({ brand, links }) => {
  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-light">
      <div className="container-fluid">
        <Link className="navbar-brand" to={brand.to}>
          {brand.name}
        </Link>
        <div className="collapse navbar-collapse">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            {links.map((link, index) => (
              <li className="nav-item" key={index}>
                <Link className="nav-link" to={link.to}>
                  {link.name}
                </Link>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default NavigationBar;
