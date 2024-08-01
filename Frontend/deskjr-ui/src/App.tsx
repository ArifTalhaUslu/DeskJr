import Login from "./components/views/Login";
import NavigationBar from "./components/CommonComponents/NavigationBar";
import Home from "./components/views/Home";
import {
    BrowserRouter as Router,
    Route,
    Routes,
    redirect,
} from "react-router-dom";
import React, { useEffect, useMemo, useState } from "react";
import MyInfo from "./components/views/MyInfo";
import Contacts from "./components/views/Contacts";
import Employee from "./components/views/Employee/Employee";
import Leaves from "./components/views/Leave";
import PendingLeaveRequests from "./components/views/PendingLeaveRequest";
import Holidays from "./components/views/Holiday/Holiday";
import LeaveTypes from "./components/views/LeaveType";
import Title from "./components/views/Title";
import "react-toastify/dist/ReactToastify.css";
import Team from "./components/views/Team/Team";
import EmployeeService from "./services/EmployeeService";
import { showErrorToast } from "./utils/toastHelper";

const App: React.FC = () => {
    const [currentUser, setCurrentUser] = useState<any>();

    const navigation = (currentUser: any) => {
        return {
            brand: { name: "�Desk", to: "/" },
            links: [
                {
                    name: "My Info",
                    to: "/myInfo",
                    visible: currentUser !== null,
                },
                {
                    name: "Contacts",
                    to: "/contacts",
                    visible: currentUser !== null,
                },
                {
                    name: "Employee List",
                    to: "/employees",
                    visible: currentUser.employeeRole === 0,
                },
                {
                    name: "Leave",
                    visible: currentUser !== null,
                    isDropDown: true,
                    subLinks: [
                        {
                            name: "My Leaves",
                            to: "/leaves",
                            visible: currentUser !== null,
                        },
                        {
                            name: "Pending Leave Requests",
                            to: "/pendingLeaveRequests",
                            visible: currentUser !== null,
                        },
                    ],
                },
                {
                    name: "Settings",
                    visible: currentUser !== null,
                    isDropDown: true,
                    subLinks: [
                        {
                            name: "Holidays",
                            to: "/holidays",
                            visible: currentUser !== null,
                        },
                        {
                            name: "Leave Types",
                            to: "/leaveTypes",
                            visible: currentUser !== null,
                        },
                        {
                            name: "Titles",
                            to: "/titles",
                            visible: currentUser !== null,
                        },
                        {
                            name: "Teams",
                            to: "/teams",
                            visible: currentUser !== null,
                        },
                    ],
                },
            ],
        };
    };

    const getNavigation = navigation(currentUser);
    const { brand, links } = getNavigation;

    const [idFromLocalStr] = useState(localStorage.getItem("id"));

    const fetchEmployee = (id: string) => {
        EmployeeService.getEmployeeById(id)
            .then((data) => {
                setCurrentUser(data);
            })
            .catch((err) => {
                showErrorToast(err);
            });
    };

    useEffect(() => {
        idFromLocalStr && fetchEmployee(idFromLocalStr);
    }, []);

    useEffect(() => {
        if (currentUser && currentUser.id) {
            redirect("/");
        } else {
            redirect("/login");
        }
    }, [currentUser]);

    return (
        <>
            <Router>
                <div className="App">
                    {currentUser && currentUser.id && (
                        <NavigationBar
                            brand={brand}
                            links={links}
                            currentUser={currentUser}
                            setCurrentUser={setCurrentUser}
                        />
                    )}
                    <Routes>
                        {currentUser && currentUser.id && (
                            <>
                                <Route
                                    path="/"
                                    element={<Home currentUser={currentUser} />}
                                />
                                <Route path="/myInfo" element={<MyInfo />} />
                                <Route
                                    path="/contacts"
                                    element={<Contacts />}
                                />

                                <Route path="/leaves" element={<Leaves />} />
                                <Route
                                    path="/pendingLeaveRequests"
                                    element={<PendingLeaveRequests />}
                                />
                                <Route
                                    path="/holidays"
                                    element={<Holidays />}
                                />
                                <Route
                                    path="/leaveTypes"
                                    element={<LeaveTypes />}
                                />
                                <Route path="/titles" element={<Title />} />
                                <Route path="*" element={<>Not Found</>} />
                            </>
                        )}
                        {currentUser && currentUser.employeeRole === 0 && (
                            <>
                                <Route
                                    path="/employees"
                                    element={<Employee />}
                                />
                                <Route path="/teams" element={<Team />} />
                            </>
                        )}
                        {!currentUser && (
                            <>
                                <Route
                                    path="*"
                                    element={
                                        <Login
                                            setCurrentUser={setCurrentUser}
                                        />
                                    }
                                />
                            </>
                        )}
                    </Routes>
                </div>
            </Router>
        </>
    );
};

export default App;
