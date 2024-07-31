import Login from "./components/views/Login";
import NavigationBar from "./components/CommonComponents/NavigationBar";
import Home from "./components/views/Home";
import {
    BrowserRouter as Router,
    Route,
    Routes,
    redirect,
} from "react-router-dom";
import React, { useEffect, useState } from "react";
import MyInfo from "./components/views/MyInfo";
import Contacts from "./components/views/Contacts";
import Employee from "./components/views/Employee/Employee";
import Leaves from "./components/views/Leave";
import PendingLeaveRequests from "./components/views/PendingLeaveRequest";
import Holidays from "./components/views/Holiday/Holiday";
import LeaveTypes from "./components/views/LeaveType";
import Title from "./components/views/Title";
import Team from "./components/views/Team";
import { Provider } from 'react-redux'
import store from "./store/store";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import Team from "./components/views/Team/Team";
import EmployeeService from "./services/EmployeeService";

const navigation = {
    brand: { name: "�Desk", to: "/" },
    links: [
        { name: "My Info", to: "/myInfo" },
        { name: "Contacts", to: "/contacts" },
        { name: "Employee List", to: "/employees" },
        {
            name: "Leave",
            isDropDown: true,
            subLinks: [
                { name: "My Leaves", to: "/leaves" },
                { name: "Pending Leave Requests", to: "/pendingLeaveRequests" },
            ],
        },
        {
            name: "Settings",
            isDropDown: true,
            subLinks: [
                { name: "Holidays", to: "/holidays" },
                { name: "Leave Types", to: "/leaveTypes" },
                { name: "Titles", to: "/titles" },
                { name: "Teams", to: "/teams" },
            ],
        },
    ],
};

const App: React.FC = () => {
    const { brand, links } = navigation;
    const [currentUser, setCurrentUser] = useState<any>();

    const fetchEmployee = (id: string) => {
        EmployeeService.getEmployeeById(id)
            .then((data) => {
                setCurrentUser(data);
            })
            .catch((error) => {
                console.error("Error fetching data: ", error);
            });
    };

    useEffect(() => {
        fetchEmployee(localStorage.getItem("id"));
    }, []);

    useEffect(() => {
        if (currentUser && currentUser.id) {
            redirect("/");
        } else {
            redirect("/login");
        }
    }, [currentUser]);

    return (
        <Router>
            <Provider store={store}>
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
                            <Route path="/contacts" element={<Contacts />} />
                            <Route path="/employees" element={<Employee />} />
                            <Route path="/leaves" element={<Leaves />} />
                            <Route
                                path="/pendingLeaveRequests"
                                element={<PendingLeaveRequests />}
                            />
                            <Route path="/holidays" element={<Holidays />} />
                            <Route
                                path="/leaveTypes"
                                element={<LeaveTypes />}
                            />
                            <Route path="/titles" element={<Title />} />
                            <Route path="/teams" element={<Team />} />
                            <Route path="*" element={<>Not Found</> } />
                        </>
                    )}
                    {!currentUser && (
                        <>
                            <Route path="*" element={<Login setCurrentUser={setCurrentUser} /> } />
                        </>
                    )}
                </Routes>
                <ToastContainer />
                </div>
            </Provider>
        </Router>
    );
};

export default App;
