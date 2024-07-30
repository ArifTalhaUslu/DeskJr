import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import EmployeeService from "../../services/EmployeeService";

interface NavigationBarProps {
    brand: {
        name: string;
        to?: string;
    };
    links: {
        name: string;
        to?: string;
        isDropDown?: boolean;
        subLinks?: { name: string; to: string }[];
    }[];
}

const NavigationBar: React.FC<NavigationBarProps> = ({ brand, links }) => {
    const [employee, setEmployee] = useState<any>(null);
    const id = '78190b40-d9cf-4b09-bc50-08dcad679a5c';

    useEffect(() => {
        const fetchEmployee = async () => {
            try {
                const fetchedEmployeeData = await EmployeeService.getEmployeeById(id);
                setEmployee(fetchedEmployeeData);
            } catch (error) {
                console.error("Error fetching employee data: ", error);
            }
        };

        fetchEmployee();
    }, [id]);

    return (
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <div className="container-fluid">
                <Link className="navbar-brand" to={brand.to!}>
                    {brand.name}
                </Link>
                <button
                    className="navbar-toggler"
                    type="button"
                    data-toggle="collapse"
                    data-target="#navbarNavDropdown"
                    aria-controls="navbarNavDropdown"
                    aria-expanded="false"
                    aria-label="Toggle navigation"
                >
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNavDropdown">
                    <ul className="navbar-nav mr-auto">
                        {links.map((link, index) => {
                            if (!link.isDropDown) {
                                return (
                                    <li className="nav-item" key={index}>
                                        <Link className="nav-link" to={link.to!}>
                                            {link.name}
                                        </Link>
                                    </li>
                                );
                            } else {
                                return (
                                    <li className="nav-item dropdown" key={index}>
                                        <a
                                            className="nav-link dropdown-toggle"
                                            href="#"
                                            id={`navbarDropdown${index}`}
                                            role="button"
                                            data-toggle="dropdown"
                                            aria-haspopup="true"
                                            aria-expanded="false"
                                        >
                                            {link.name}
                                        </a>
                                        <div
                                            className="dropdown-menu"
                                            aria-labelledby={`navbarDropdown${index}`}
                                        >
                                            {link.subLinks?.map((subLink, subIndex) => (
                                                <Link className="dropdown-item" to={subLink.to!} key={subIndex}>
                                                    {subLink.name}
                                                </Link>
                                            ))}
                                        </div>
                                    </li>
                                );
                            }
                        })}
                    </ul>
                    <ul className="navbar-nav ml-auto">
                        <li className="nav-item dropdown">
                            <a
                                className="nav-link dropdown-toggle"
                                href="#"
                                id="navbarUserDropdown"
                                role="button"
                                data-toggle="dropdown"
                                aria-haspopup="true"
                                aria-expanded="false"
                            >
                                <i className="bi bi-person"></i> {employee ? employee.name : 'Loading...'}
                            </a>
                            <div
                                className="dropdown-menu"
                                aria-labelledby="navbarUserDropdown"
                            >
                                <Link className="dropdown-item" to="/profile">Option 1</Link>
                                <Link className="dropdown-item" to="/settings">Option 2</Link>
                                <div className="dropdown-divider"></div>
                                <Link className="dropdown-item" to="/logout">Option 3</Link>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    );
};

export default NavigationBar;
