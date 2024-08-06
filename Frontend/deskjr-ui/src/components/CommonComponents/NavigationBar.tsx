import React from "react";
import { Link, useNavigate } from "react-router-dom";
import { Employee } from "../../types/employee";
import Button from "./Button";
import Cookies from "js-cookie";

interface NavigationBarProps {
    currentUser: Employee;
    setCurrentUser: any;
    brand: {
        name: string;
        to?: string;
    };
    links: {
        name: string;
        to?: string;
        isDropDown?: boolean;
        visible?: boolean;
        subLinks?: { name: string; to: string; visible?: boolean }[];
    }[];
}

const NavigationBar: React.FC<NavigationBarProps> = ({
    brand,
    links,
    currentUser,
    setCurrentUser,
}) => {
    const navigate = useNavigate();

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
                <div
                    className="collapse navbar-collapse"
                    id="navbarNavDropdown"
                >
                    <ul className="navbar-nav mr-auto">
                        {links.map((link, index) => {
                            if (!link.visible) return null;
                            if (!link.isDropDown) {
                                return (
                                    <li className="nav-item" key={index}>
                                        <Link
                                            className="nav-link"
                                            to={link.to!}
                                        >
                                            {link.name}
                                        </Link>
                                    </li>
                                );
                            } else {
                                return (
                                    <li
                                        className="nav-item dropdown"
                                        key={index}
                                    >
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
                                            {link.subLinks?.map(
                                                (subLink, subIndex) => {
                                                    if (!subLink.visible)
                                                        return null;
                                                    return (
                                                        <Link
                                                            className="dropdown-item"
                                                            to={subLink.to!}
                                                            key={subIndex}
                                                        >
                                                            {subLink.name}
                                                        </Link>
                                                    );
                                                }
                                            )}
                                        </div>
                                    </li>
                                );
                            }
                        })}
                    </ul>
                    <ul className="navbar-nav ml-auto">
                        <li className="nav-item dropdown ">
                            <a
                                className="nav-link dropdown-toggle"
                                href="#"
                                id="navbarUserDropdown"
                                role="button"
                                data-toggle="dropdown"
                                aria-haspopup="true"
                                aria-expanded="false"
                                style={{
                                    fontWeight: "bold",
                                    textTransform: "capitalize",
                                }}
                            >
                                <i className="bi bi-person "></i>{" "}
                                {currentUser
                                    ? currentUser.name
                                    : "Unauthorized"}
                            </a>
                            <div
                                className="dropdown-menu dropdown-menu-right"
                                aria-labelledby="navbarUserDropdown"
                            >
                                <Link className="dropdown-item " to="/myInfo">
                                    Profile
                                </Link>
                                <Link className="dropdown-item" to="/changePassword">
                                    Change Password
                                </Link>
                                <div className="dropdown-divider"></div>
                                <Button
                                    text={"Logout"}
                                    type="button"
                                    className="dropdown-item btn btn-danger"
                                    style={{ color: "red", fontWeight: "bold" }}
                                    onClick={() => {
                                        setCurrentUser(null);
                                        Cookies.remove("id");
                                        Cookies.remove("token");
                                        navigate("/login");
                                    }}
                                />
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    );
};

export default NavigationBar;
