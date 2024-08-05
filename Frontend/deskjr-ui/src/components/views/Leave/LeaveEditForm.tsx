import { useEffect, useState } from "react";
import leaveService from "../../../services/LeaveService";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import Button from "../../CommonComponents/Button";
import Input from "../../CommonComponents/Input";

const LeaveEditForm: any = (props: any) => {
    useEffect(() => {
        if (props.selectedItemId) {
            leaveService.getLeaveById(props.selectedItemId).then((data) => {
                props.setSelectedLeave(data);
            })
                .catch((err) => {
                    showErrorToast(err);
                });
        }
    }, [props.selectedItemId]);

    const handleChange = (e: any) => {
        const { name, value } = e.target;
        props.setSelectedLeave((prevState: any) => ({
            ...prevState,
            [name]: value,
        }));
    };

    const handleSubmit = (e: any) => {
        e.preventDefault();
        leaveService
            .createLeave({
                ...props.selectedLeave,
            })
            .then(() => {
                showSuccessToast('Successful!');
                props.getList();
                props.onClose();
            })
            .catch((err) => {
                showErrorToast(err);
            });
    };

    const formatDate = (date: string | Date | undefined): string => {
        if (!date) return "";
        const dateObj = typeof date === "string" ? new Date(date) : date;
        const year = dateObj.getFullYear();
        const month = (dateObj.getMonth() + 1).toString().padStart(2, "0");
        const day = dateObj.getDate().toString().padStart(2, "0");
        return `${year}-${month}-${day}`;
    };

    return (
        <>
            <div
                className="modal fade"
                id="leaveAddModal"
                role="dialog"
                data-backdrop="static"
            >
                <div className="modal-dialog" role="document">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title">{props.modalModeName} Leave</h5>
                            <button
                                type="button"
                                className="close"
                                id="form-close"
                                data-dismiss="modal"
                                hidden
                            ></button>
                            <button
                                type="button"
                                className="close"
                                onClick={() => {
                                    props.onClose();
                                    const close_button = document.getElementById("form-close");
                                    close_button?.click();
                                }}
                            >
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <form onSubmit={(e) => handleSubmit(e)}>
                            <div className="modal-body">
                                <Input
                                    type="hidden"
                                    name="Id"
                                    value={props.selectedLeave && props.selectedLeave.id}
                                />
                                <div className="form-group">
                                    <label className="col-form-label">Start Date:</label>
                                    <Input
                                        type="date"
                                        name="startDate"
                                        value={props.selectedLeave &&
                                            formatDate(props.selectedLeave.startDate)
                                        }
                                        onChange={(e: any) => handleChange(e)}
                                        required
                                    />
                                    <label className="col-form-label">End Date:</label>
                                    <Input
                                        type="date"
                                        name="endDate"
                                        value={props.selectedLeave &&
                                            formatDate(props.selectedLeave.endDate)
                                        }
                                        onChange={(e: any) => handleChange(e)}
                                        required
                                    />
                                    {/* <label className="col-form-label">Leave Type:</label>
                                    <select
                                        name="leaveTypeId"
                                        className="form-control"
                                        value={props.selectedLeave?.leaveTypeId || ""}
                                        onChange={(e: any) => handleChange(e)}
                                        //required
                                      >
                                        <option value=""></option>
                                        {leaveTypes.map((leaveType: any) => (
                                          <option key={leaveType.id} value={leaveType.id}>{leaveType.name}</option>
                                        ))}
                                    </select> */}
                                    <label className="col-form-label">Leave Comments:</label>
                                    <textarea
                                        className="form-control"
                                        name="requestComments"
                                        value={props.selectedLeave?.requestComments || ""}
                                        onChange={(e: any) => handleChange(e)}
                                        required
                                    />
                                </div>
                            </div>
                            <div className="modal-footer">
                                <Button
                                    type="submit"
                                    className="btn btn-primary"
                                    text="Submit"
                                />
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </>
    );

};

export default LeaveEditForm;