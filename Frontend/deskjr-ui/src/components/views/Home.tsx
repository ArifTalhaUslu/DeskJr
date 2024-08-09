import { useState, useEffect } from "react";
import EmployeeService from "../../services/EmployeeService";
import { showErrorToast } from "../../utils/toastHelper";
import Board from "../CommonComponents/Board";
import { formatDate } from "date-fns";
import HolidayService from "../../services/HolidayService";
import Card from "../CommonComponents/Card";

const Home = (props: any) => {
    const [employees, setEmployees] = useState<any[]>([]);
    const [holidays, setHolidays] = useState<any[]>([]);

    const fetchAllEmployees = () => {
        EmployeeService.getAllEmployee()
            .then((data) => {
                setEmployees(data);
            })
            .catch((err) => {
                showErrorToast(err);
            });
    };

    const fetchAllHolidays = () => {
        HolidayService.getAllHoliday()
            .then((data) => {
                setHolidays(data);
            })
            .catch((err) => {
                showErrorToast(err);
            });
    };

    useEffect(() => {
        fetchAllHolidays();
        fetchAllEmployees();
    }, []);


    const columnNamesEmployee = {
        name: "Employee Name",
        dayOfBirth: "BirthDay"
    };

    const columnNamesHoliday = {
        name: "Holiday Name",
        startDate: "Start Date",
        endDate: "End Date",
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
        }
        return value;
    };
    const renderColumnHoliday = (column: string, value: any) => {
        if (column === "startDate") {
            return formatDate(new Date(value), "dd/MM/yyyy");
        } else if (column === "endDate") {
            return formatDate(new Date(value), "dd/MM/yyyy");
        }
        return value;
    };

    return (
        <div className="container">
            <div className="row">
                <div className="col-12">
                    <div className="p-3 my-3 bg-primary text-white card">
                        <h1 className="mt-4" style={{textTransform: 'capitalize'}}>
                            <h1> {props.currentUser?.name} <span className="text-sm"> ({props.currentUser?.team?.name} Team) </span> </h1>
                            <h2>{props.currentUser?.employeeTitle?.titleName}</h2>
                        </h1>
                    </div>
                </div>
                <div className="col-md-6">
                    <Card title="Employees On Vacation">
                        <Board
                            items={employees}
                            isEditable={() => { return false; }}
                            isDeletable={() => { return false; }}
                            hiddenColumns={["id", "password", "teamId", "employeeTitleId", "employeeRole", "gender", "employeeTitle", "team", "email"]}
                            renderColumn={renderColumnEmployee}
                            columnNames={columnNamesEmployee}
                            hideActions={'true'}
                        />
                    </Card>
                </div>
                <div className="col-md-6">
                    <Card title="Incomming Holidays">
                        <Board
                            items={holidays}
                            isEditable={() => { return false; }}
                            isDeletable={() => { return false; }}
                            hiddenColumns={["id"]}
                            renderColumn={renderColumnHoliday}
                            columnNames={columnNamesHoliday}
                            hideActions={'true'}
                        />
                    </Card>
                </div>
            </div>
        </div>
    );
};

export default Home;
