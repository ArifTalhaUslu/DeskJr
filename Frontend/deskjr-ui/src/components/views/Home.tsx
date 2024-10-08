import { useState, useEffect } from "react";
import EmployeeService from "../../services/EmployeeService";
import { showErrorToast } from "../../utils/toastHelper";
import Board from "../CommonComponents/Board";
import { formatDate } from "date-fns";
import HolidayService from "../../services/HolidayService";
import Card from "../CommonComponents/Card";
import LeaveService from "../../services/LeaveService";

const Home = (props: any) => {
  const [holidays, setHolidays] = useState<any[]>([]);
  const [upcomingBirthdays, setUpcomingBirthdays] = useState<any[]>([]);
  const [recentValidLeaves, setRecentValidLeaves] = useState<any[]>([]);
  const [userLeaveInfo, setUserLeaveInfo] = useState<any>({});

  const fetchRecentValidLeaves = () => {
    LeaveService.getRecentValidLeaves()
      .then((data) => {
        setRecentValidLeaves(data);
      })
      .catch((err) => {
        showErrorToast(err);
      });
  };

  const fetchAllHolidays = () => {
    HolidayService.getUpComingHoliday()
      .then((data) => {
        setHolidays(data);
      })
      .catch((err) => {
        showErrorToast(err);
      });
  };

  const fetchUpcomingBirthdays = () => {
    EmployeeService.getUpcomingBirthdays()
      .then((data) => {
        setUpcomingBirthdays(data);
      })
      .catch((err) => {
        showErrorToast(err);
      });
  };

  const fetchLeaveInfo = async (userId: any) => {
    await EmployeeService.employeeLeavesInfo(userId)
      .then((data: any) => {
        setUserLeaveInfo(data);
      })
      .catch((err: any) => {
        showErrorToast(err);
      });
  }

  useEffect(() => {
    fetchAllHolidays();
    fetchRecentValidLeaves();
    fetchUpcomingBirthdays();
    fetchLeaveInfo(props.currentUser.id)
  }, []);

  const columnNamesEmployeeLeaves = {
    requestingEmployee: "Employee Name",
    startDate: "Start Date",
    endDate: "End Date",
  };

  const columnNamesHoliday = {
    name: "Holiday Name",
    startDate: "Start Date",
    endDate: "End Date",
  };
  const columnNamesBirthday = {
    name: "Name",
    dayOfBirth: "Birthday",
  };

  const renderColumnLeave = (column: string, value: any) => {
    if (column === "name") {
      return value;
    } else if (column === "startDate" || column === "endDate") {
      return formatDate(new Date(value), "dd/MM/yyyy");
    } else if (column === "requestingEmployee") {
      return value.name;
    }
    return null;
  };
  const renderColumnHoliday = (column: string, value: any) => {
    if (column === "startDate") {
      return formatDate(new Date(value), "dd/MM/yyyy");
    } else if (column === "endDate") {
      return formatDate(new Date(value), "dd/MM/yyyy");
    }
    return value;
  };
  const renderColumnEmployee = (column: string, value: any) => {
    if (column === "employeeRole") {
      return value === 2
        ? "Employee"
        : value === 1
          ? "Manager"
          : value === 0
            ? "Admin"
            : value;
    } else if (column === "gender") {
      return value === 0 ? "Male" : value === 1 ? "Female" : value;
    } else if (column === "dayOfBirth") {
      return formatDate(new Date(value), "dd/MM/yyyy");
    } else if (column === "employeeTitle") {
      return value && value.titleName;
    } else if (column === "team") {
      return value && value.name;
    } else if (column === "base64Image") {
      return (
        <img
          src={value}
          alt="Profile"
          style={{ width: "50px", height: "50px", borderRadius: "50%" }}
        />
      );
    }
    return value;
  };

  return (
    <div className="container">
      <div className="row">
        <div className="col-12">
          <div className="p-3 my-3 bg-primary text-white card text-center">
            <div className="text-center">
              <img
                src={props.currentUser?.base64Image}
                alt="Profile"
                style={{ width: "130px", height: "130px", borderRadius: "50%" }} />
            </div>
            <h1 className="mt-4" style={{ textTransform: "capitalize" }}>
              {props.currentUser?.name}{" "}
              <span className="text-sm">
                {" "}
                ({props.currentUser?.team?.name}){" "}
              </span>
            </h1>
            <h2>{props.currentUser?.employeeTitle?.titleName}</h2>
          </div>
        </div>

        <div className="col-12">
          <div className="card-group">
            <div className="card text-white m-1" style={{ backgroundColor: "#81AC5B" }}>
              <div className="card-body">
                <h5 className="card-title">Deserved Day</h5>
                <h4 className="card-text">{userLeaveInfo.deservedDay}</h4>
              </div>
            </div>
            <div className="card text-white m-1" style={{ backgroundColor: "#BB3242" }} >
              <div className="card-body">
                <h5 className="card-title">Used Day</h5>
                <h4 className="card-text">{userLeaveInfo.usedDay}</h4>
              </div>
            </div>
            <div className="card text-white m-1" style={{ backgroundColor: "#F8CD4F" }}>
              <div className="card-body">
                <h5 className="card-title">Remaning Day</h5>
                <h4 className="card-text">{userLeaveInfo.remainingDay}</h4>
              </div>
            </div>
          </div>
        </div>

        <div className="col-sm-6">
          <Card title="Employees On Leaves">
            <Board
              items={recentValidLeaves}
              isEditable={() => {
                return false;
              }}
              isDeletable={() => {
                return false;
              }}
              hiddenColumns={[
                "id",
                "password",
                "teamId",
                "employeeRole",
                "gender",
                "employeeTitle",
                "team",
                "email",
                "leaveTypeId",
                "requestingEmployeeId",
                "leaveType",
                "requestComments",
                "statusOfLeave",
                "approvedById",
                "approvedBy",
              ]}
              renderColumn={renderColumnLeave}
              columnNames={columnNamesEmployeeLeaves}
              hideActions={"true"}
            />
          </Card>
        </div>
        <div className="col-sm-6">
          <Card title="Public Holidays">
            <Board
              items={holidays}
              isEditable={() => {
                return false;
              }}
              isDeletable={() => {
                return false;
              }}
              hiddenColumns={["id"]}
              renderColumn={renderColumnHoliday}
              columnNames={columnNamesHoliday}
              hideActions={"true"}
            />
          </Card>
        </div>
        <div className="col-md-12">
          <Card title="Upcoming Birthdays🥳">
            <Board
              items={upcomingBirthdays}
              isEditable={() => {
                return false;
              }}
              isDeletable={() => {
                return false;
              }}
              hiddenColumns={[
                "id",
                "password",
                "teamId",
                "employeeTitleId",
                "employeeRole",
                "gender",
                "employeeTitle",
                "team",
                "email",
                "hireDate"
                "base64Image",
              ]}
              renderColumn={renderColumnEmployee}
              columnNames={columnNamesBirthday}
              hideActions={"true"}
            />
          </Card>
        </div>
      </div>
    </div >
  );
};

export default Home;
