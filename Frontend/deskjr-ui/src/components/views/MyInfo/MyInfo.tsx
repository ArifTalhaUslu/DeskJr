import React from 'react';
import { User } from '../../../types/user';
import { Roles } from '../../../types/Roles';

interface MyInfoProps {
    currentUser: User;
}

const MyInfo: React.FC<MyInfoProps> = ({ currentUser }) => {

    return (
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
                                        <td>{currentUser.employeeTitle.titleName || "N/A"}</td>
                                    </tr>
                                    <tr>
                                        <th>Team:</th>
                                        <td>{currentUser.team.name || "N/A"}</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default MyInfo;
