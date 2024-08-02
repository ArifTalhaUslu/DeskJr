import { useEffect, useState } from "react";
import leaveTypeService from "../../../services/LeaveTypeService";
import Button from "../../CommonComponents/Button";
import Input from "../../CommonComponents/Input";
import { showSuccessToast } from "../../../utils/toastHelper";

const LeaveTypeEditForm: any = (props: any) => {
  useEffect(() => {
    if (props.selectedItemId) {
      leaveTypeService.getLeaveTypeById(props.selectedItemId).then((data) => {
        props.setSelectedLeaveType(data);
      });
    }
  }, [props.selectedItemId]);

  const handleChange = (e: any) => {
    const { name, value } = e.target;
    props.setSelectedLeaveType((prev: any) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = (e: any) => {
    e.preventDefault();
    leaveTypeService
      .addOrUpdateLeaveType({
        ...props.selectedLeaveType,
      })
      .then(() => {
        showSuccessToast("Success");
        props.getList();
        props.onClose();
      })
      .catch((err: any) => {
        console.log(err);
      });
  };

  return (
    <>
      <div
        className="modal fade"
        id="leaveTypeAddModal"
        role="dialog"
        data-backdrop="static"
      >
        <div className="modal-dialog" role="document">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title">{props.modalModeName} Leave Type</h5>
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
                  value={props.selectedLeaveType?.id || ""}
                />
                <div className="form-group">
                  <label className="col-form-label">Leave Type Name:</label>
                  <Input
                    type="text"
                    name="name"
                    value={props.selectedLeaveType?.name || ""}
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

export default LeaveTypeEditForm;
