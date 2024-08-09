import React, { useState } from "react";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import Input from "../../CommonComponents/Input";
import Button from "../../CommonComponents/Button";
import AccountService from "../../../services/AccountService";

const ChangePassword: React.FC<{ currentUser: any }> = ({ currentUser }) => {
    const [oldPassword, setOldPassword] = useState<string>("");
    const [newPassword, setNewPassword] = useState<string>("");
    const [confirmNewPassword, setConfirmNewPassword] = useState<string>("");

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        if (newPassword !== confirmNewPassword) {
            showErrorToast("New password and confirm password do not match");
            return;
        }

        AccountService.changePassword(
            {
                oldPassword,
                newPassword,
            },
            currentUser
        )
        .then(() => {
            showSuccessToast("Password changed successfully!");
        })
        .catch(() => {
            showErrorToast("Failed to change password. Please try again.");
        });
    };

    return (
        <div className="container mt-5">
            <div className="row">
                <div className="col-8 mx-auto">
                    <div className="card bg-primary text-white mb-4">
                        <div className="card-body">
                            <h1 className="card-title mb-3">Change Password</h1>
                        </div>
                    </div>
                </div>
                <div className="col-md-8 mx-auto">
                    <div className="card">
                        <div className="card-body">
                            <form onSubmit={handleSubmit}>
                                <div className="form-group">
                                    <label htmlFor="oldPassword">Old Password</label>
                                    <Input
                                        type="password"
                                        className="form-control"
                                        id="oldPassword"
                                        value={oldPassword}
                                        onChange={(e) => setOldPassword(e.target.value)}
                                        required
                                        placeholder="Enter your old password"
                                    />
                                </div>
                                <div className="form-group">
                                    <label htmlFor="newPassword">New Password</label>
                                    <Input
                                        type="password"
                                        className="form-control"
                                        id="newPassword"
                                        value={newPassword}
                                        onChange={(e) => setNewPassword(e.target.value)}
                                        required
                                        placeholder="Enter your new password"
                                    />
                                </div>
                                <div className="form-group">
                                    <label htmlFor="confirmNewPassword">Confirm New Password</label>
                                    <Input
                                        type="password"
                                        className="form-control"
                                        id="confirmNewPassword"
                                        value={confirmNewPassword}
                                        onChange={(e) => setConfirmNewPassword(e.target.value)}
                                        required
                                        placeholder="Confirm your new password"
                                    />
                                </div>
                                <Button type="submit" text="Change Password" className="btn btn-danger" />
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ChangePassword;
