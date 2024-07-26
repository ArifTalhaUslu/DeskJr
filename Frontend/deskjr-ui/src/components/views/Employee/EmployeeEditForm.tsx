import Button from "../../CommonComponents/Button";
import Input from "../../CommonComponents/Input";
import { useEffect, useState } from "react";
import employeeService from "../../../services/EmployeeService";

const EmployeeEditForm: any = (props: any) => {
  const [genderOptions] = useState([
    { value: 1, label: "Female" },
    { value: 0, label: "Male" },
  ]);

  const [roleOptions] = useState([
    { value: 0, label: "Admin" },
    { value: 1, label: "Manager" },
    { value: 2, label: "Employee" },
  ]);

  useEffect(() => {
    if (props.selectedItemId) {
      employeeService.getEmployeeById(props.selectedItemId).then((data) => {
        props.setSelectedEmployee(data);
      });
    }
  }, [props.selectedItemId]);

  const handleChange = (e: any) => {
    const { name, value } = e.target;
    if (name === "gender" || name === "employeeRole") {
      props.setSelectedEmployee((prev: any) => ({
        ...prev,
        [name]: parseInt(value, 10),
      }));
    } else {
      props.setSelectedEmployee((prev: any) => ({
        ...prev,
        [name]: value,
      }));
    }
  };

  const handleSubmit = (e: any) => {
    e.preventDefault();
    employeeService
      .addOrUpdateEmployee({
        ...props.selectedEmployee,
      })
      .then(() => {
        alert("success");
      }).catch((err:any) => {
        console.log(err);
      })
      .finally(() => {
        props.onClose();
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
      <div className="modal fade" id="employeeAddModal" role="dialog">
        <div className="modal-dialog" role="document">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title">{props.modalModeName} Employee</h5>
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
                  value={props.selectedEmployee && props.selectedEmployee.id}
                />
                <div className="form-group">
                  <label className="col-form-label">Name:</label>
                  <Input
                    type="text"
                    name="name"
                    value={
                      props.selectedEmployee && props.selectedEmployee.name
                    }
                    onChange={(e: any) => handleChange(e)}
                    required
                  />
                  <label className="col-form-label">BirthDay:</label>
                  <Input
                    type="date"
                    name="dayOfBirth"
                    value={
                      props.selectedEmployee &&
                      formatDate(props.selectedEmployee.dayOfBirth)
                    }
                    onChange={(e: any) => handleChange(e)}
                    required
                  />

                  <label className="col-form-label">Role:</label>
                  <select
                    name="employeeRole"
                    className="form-control"
                    value={
                      props.selectedEmployee &&
                      props.selectedEmployee.employeeRole
                    }
                    onChange={(e: any) => handleChange(e)}
                    required
                  >
                    {roleOptions.map((option: any) => (
                      <option key={option.value} value={option.value}>
                        {option.label}
                      </option>
                    ))}
                  </select>

                  <label className="col-form-label">Gender:</label>
                  <select
                    name="gender"
                    className="form-control"
                    value={
                      props.selectedEmployee && props.selectedEmployee.gender
                    }
                    onChange={(e: any) => handleChange(e)}
                    required
                  >
                    {genderOptions.map((option: any) => (
                      <option key={option.value} value={option.value}>
                        {option.label}
                      </option>
                    ))}
                  </select>
                  {props.modalModeName == "Add" ? (
                    <></>
                  ) : (
                    <>
                      {/* <label className="col-form-label">Title:</label>
                <Input
                  type="text"
                  name="titleId"
                  value={
                    props.selectedEmployee && props.selectedEmployee.titleId
                  }
                  onChange={(e: any) => handleChange(e)}
                /> */}
                    </>
                  )}

                  {props.modalModeName == "Add" ? (
                    <></>
                  ) : (
                    <>
                      {/* <label className="col-form-label">Team:</label>
                <Input
                  type="text"
                  name="teamId"
                  value={
                    props.selectedEmployee && props.selectedEmployee.teamId
                  }
                  onChange={(e: any) => handleChange(e)} */}
                    </>
                  )}

                  <label className="col-form-label">E-mail:</label>
                  <Input
                    type="text"
                    name="email"
                    value={
                      props.selectedEmployee && props.selectedEmployee.email
                    }
                    onChange={(e: any) => handleChange(e)}
                    required
                  />
                  <label className="col-form-label">Password:</label>
                  <Input
                    type="password"
                    name="password"
                    value={
                      props.selectedEmployee && props.selectedEmployee.password
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

export default EmployeeEditForm;
