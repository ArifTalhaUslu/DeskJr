import React, { useEffect, useState } from 'react';
import { User } from '../../../types/user';
import { Roles } from '../../../types/Roles';
import Board from '../../CommonComponents/Board';
import employeeService from '../../../services/EmployeeService';
import { showErrorToast } from '../../../utils/toastHelper';
import { formatDate } from 'date-fns';
import Card from '../../CommonComponents/Card';

interface MyInfoProps {
    currentUser: User;
}

const MyInfo: React.FC<MyInfoProps> = ({ currentUser }) => {
    const [items, setItems] = useState([]);

    useEffect(() => {
        getList(currentUser.id);
    }, [currentUser]);

    const getList = async (userId: any) => {
        await employeeService.calculateLeaves(userId)
            .then((data: any) => {
                setItems(data);
            })
            .catch((err: any) => {
                showErrorToast(err);
            });
    }

    const renderColumn = (column: string, value: any) => {
        if (column === "endDate" || column === "startDate") {
            return formatDate(new Date(value), "dd/MM/yyyy");
        }
        return value;
    }

    return (
        <>
            <div className="container mt-4">
                <div className="row ">
                    <div className="col-12 mx-auto">
                        <div className="card bg-primary text-white mb-4">
                            <div className="card-body">
                                <h1 className="card-title">My Information</h1>
                            </div>
                        </div>
                    </div>
                    <div className="col-md-12 mx-auto">
                        <div className="card">
                            <div className="card-header">
                                <h2>Personal Information</h2>
                            </div>
                            <div className="card-body">
                                <table className="table table-striped">
                                    <tbody>
                                        <tr>
                                            <th>Name:</th>
                                            <td>{currentUser.name}</td>
                                        </tr>
                                        <tr>
                                            <th>Date of Birth:</th>
                                            <td>{new Date(currentUser.dayOfBirth).toLocaleDateString()}</td>
                                        </tr>
                                        <tr>
                                            <th>Email:</th>
                                            <td>{currentUser.email}</td>
                                        </tr>
                                        <tr>
                                            <th>Gender:</th>
                                            <td>{currentUser.gender === 0 ? "Male" : "Female"}</td>
                                        </tr>
                                        <tr>
                                            <th>Role:</th>
                                            <td>
                                                {currentUser.employeeRole === Roles.Employee
                                                    ? "Employee"
                                                    : currentUser.employeeRole === Roles.Manager
                                                        ? "Manager"
                                                        : currentUser.employeeRole === Roles.Admin
                                                            ? "Admin"
                                                            : "Unknown"}
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>Title:</th>
                                            <td>{currentUser.employeeTitle?.titleName || "N/A"}</td>
                                        </tr>
                                        <tr>
                                            <th>Team:</th>
                                            <td>{currentUser.team?.name || "N/A"}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <Card title={"Leave Progress Payments"}>
                    <Board
                        items={items}
                        renderColumn={renderColumn}
                        hiddenColumns={["userId"]}
                        hideActions='true'
                        columnNames={{
                            startDate: "Start Date",
                            endDate: "End Date",
                            dayUsed: "Used",
                            deservedDay: "Deserved",
                            transferDay: "Transfer",
                        }}
                    />
                </Card>
            </div>
        </>
    );
};

export default MyInfo;
