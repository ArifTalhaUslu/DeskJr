import "./App.css";
import Login from "./components/views/Login";
import NavigationBar from "./components/CommonComponents/NavigationBar";
import Home from "./components/views/Home";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import React from "react";
import MyInfo from "./components/views/MyInfo";
import Contacts from "./components/views/Contacts";
import Employee from "./components/views/Employee/Employee";
import Leaves from "./components/views/Leave";
import PendingLeaveRequests from "./components/views/PendingLeaveRequest";
import Holidays from "./components/views/Holiday";

import Team from "./components/views/Team";
import EmployeeTitle from "./components/views/EmployeeTitle/EmployeeTitle";
import LeaveType from "./components/views/LeaveType/LeaveType";

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
        { name: "Titles", to: "/employeeTitles" },
        { name: "Teams", to: "/teams" },
      ],
    },
  ],
};

const App: React.FC = () => {
  const { brand, links } = navigation;

  return (
    <Router>
      <div className="App">
        <NavigationBar brand={brand} links={links} />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/login" element={<Login />} />
          <Route path="/myInfo" element={<MyInfo />} />
          <Route path="/contacts" element={<Contacts />} />
          <Route path="/employees" element={<Employee />} />
          <Route path="/leaves" element={<Leaves />} />
          <Route
            path="/pendingLeaveRequests"
            element={<PendingLeaveRequests />}
          />
          <Route path="/holidays" element={<Holidays />} />
          <Route path="/leaveTypes" element={<LeaveType />} />
          <Route path="/employeeTitles" element={<EmployeeTitle />} />
          <Route path="/teams" element={<Team />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;
