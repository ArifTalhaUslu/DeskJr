import Button from "../../CommonComponents/Button";
import Input from "../../CommonComponents/Input";
import { useEffect, useState } from "react";
import employeeTitleService from "../../../services/EmployeeTitleService";

const EmployeeTitleEditForm: any = (props: any) => {
  useEffect(() => {
    if (props.selectedItemId) {
      employeeTitleService
        .getEmployeeTitleById(props.selectedItemId)
        .then((data) => {
          props.setSelectedEmployeeTitle(data);
        });
    }
  }, [props.selectedItemId]);

  const handleChange = (e: any) => {
    const { name, value } = e.target;
    props.setSelectedEmployeeTitle((prev: any) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = (e: any) => {
    e.preventDefault();
    employeeTitleService
      .addOrUpdateEmployeeTitle({
        ...props.selectedEmployeeTitle,
      })
      .then(() => {
        alert("Success");
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
        id="employeeTitleAddModal"
        role="dialog"
        data-backdrop="static"
      >
        <div className="modal-dialog" role="document">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title">
                {props.modalModeName} Employee Title
              </h5>
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
                  value={
                    props.selectedEmployeeTitle &&
                    props.selectedEmployeeTitle.id
                  }
                />
                <div className="form-group">
                  <label className="col-form-label">Title Name:</label>
                  <Input
                    type="text"
                    name="titleName"
                    value={
                      props.selectedEmployeeTitle &&
                      props.selectedEmployeeTitle.titleName
                    }
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

export default EmployeeTitleEditForm;
