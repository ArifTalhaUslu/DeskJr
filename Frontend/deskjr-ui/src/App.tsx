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
import MyInfo from "./components/views/MyInfo/MyInfo";
import Employee from "./components/views/Employee/Employee";
import Leaves from "./components/views/Leave/Leave";
import LeaveApproval from "./components/views/LeaveApproval/LeaveApproval";

import Holidays from "./components/views/Holiday/Holiday";
import AdvancedSetting from "./components/views/AdvancedSetting/AdvancedSetting";

import "react-toastify/dist/ReactToastify.css";
import Team from "./components/views/Team/Team";
import EmployeeService from "./services/EmployeeService";
import { showErrorToast } from "./utils/toastHelper";
import Cookies from "js-cookie";
import { Roles } from "./types/Roles";

import EmployeeTitle from "./components/views/EmployeeTitle/EmployeeTitle";
import LeaveType from "./components/views/LeaveType/LeaveType";
import ChangePassword from "./components/views/ChangePassword/ChangePassword";

const App: React.FC = () => {
  const [currentUser, setCurrentUser] = useState<any>();
  const [loading, setLoading] = useState(true);

  const navigation = (currentUser: any) => {
    return {
      brand: { name: "� Desk Jr.", to: "/" },
      links: [
        {
          name: "My Info",
          to: "/myInfo",
          visible: currentUser !== null,
        },
        {
          name: "Employee List",
          to: "/employees",
          visible:
            currentUser?.employeeRole === Roles.Admin ||
            currentUser?.employeeRole === Roles.Manager,
        },

        {
          name: "Leave",
          visible: true,
          isDropDown: true,
          subLinks: [
            {
              name: "My Leaves",
              to: "/leaves",
              visible: currentUser?.employeeRole !== Roles.Admin,
            },
            {
              name: "Leave Approval",
              to: "/leaveApproval",
              visible:
                currentUser?.employeeRole === Roles.Admin ||
                currentUser?.employeeRole === Roles.Manager,
            },
          ],
        },
        {
          name: "Settings",
          visible: currentUser?.employeeRole === Roles.Admin,
          isDropDown: true,
          subLinks: [
            {
              name: "Holidays",
              to: "/holidays",
              visible: currentUser?.employeeRole === Roles.Admin,
            },
            {
              name: "Leave Types",
              to: "/leaveTypes",
              visible: currentUser?.employeeRole === Roles.Admin,
            },
            {
              name: "Titles",
              to: "/titles",
              visible: currentUser?.employeeRole === Roles.Admin,
            },
            {
              name: "Teams",
              to: "/teams",
              visible: currentUser?.employeeRole === Roles.Admin,
            },
            {
              name: "Advanced Setting",
              to: "/advancedSetting",
              visible: currentUser?.employeeRole === Roles.Admin,
            },
          ],
        },
      ],
    };
  };

  const { brand, links } = navigation(currentUser);

  const [idFromLocalStr] = useState(Cookies.get("id"));

  const fetchEmployee = (id: string) => {
    setLoading(true);
    EmployeeService.getEmployeeById(id)
      .then((data) => {
        setCurrentUser(data);
      })
      .catch((err) => {
        showErrorToast(err);
      })
      .finally(() => {
        setLoading(false);
      });
  };

  useEffect(() => {
    if (idFromLocalStr) {
      fetchEmployee(idFromLocalStr);
    } else {
      setLoading(false);
    }
  }, [idFromLocalStr]);

  if (loading) {
    return <div>.</div>;
  }

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
                <Route path="/" element={<Home currentUser={currentUser} />} />
                <Route
                  path="/myInfo"
                  element={<MyInfo currentUser={currentUser} />}
                />

                <Route
                  path="/leaves"
                  element={<Leaves currentUser={currentUser} />}
                />
                <Route
                  path="/leaveApproval"
                  element={<LeaveApproval currentUser={currentUser} />}
                />
                <Route path="/holidays" element={<Holidays />} />
                <Route path="/titles" element={<EmployeeTitle />} />
                <Route path="*" element={<>Not Found</>} />
                <Route path="/leaveTypes" element={<LeaveType />} />
                <Route
                  path="/changePassword"
                  element={<ChangePassword currentUser={currentUser} />}
                />
              </>
            )}
            {currentUser && currentUser.employeeRole === Roles.Admin && (
              <>
                <Route
                  path="/employees"
                  element={<Employee currentUser={currentUser} />}
                />
                <Route path="/teams" element={<Team />} />
                <Route path="advancedSetting" element={<AdvancedSetting />} />
              </>
            )}
            {currentUser && currentUser.employeeRole === Roles.Manager && (
              <>
                <Route
                  path="/employees"
                  element={<Employee currentUser={currentUser} />}
                />
              </>
            )}
            {!currentUser && (
              <>
                <Route
                  path="*"
                  element={<Login setCurrentUser={setCurrentUser} />}
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
