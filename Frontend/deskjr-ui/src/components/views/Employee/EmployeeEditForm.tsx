import Button from "../../CommonComponents/Button";
import Input from "../../CommonComponents/Input";
import { useEffect, useState } from "react";
import employeeService from "../../../services/EmployeeService";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import teamService from "../../../services/TeamService";
import employeeTitleService from "../../../services/EmployeeTitleService";
const EmployeeEditForm: any = (props: any) => {
  const [teams, setTeams] = useState([]);
  const [titles, setTitles] = useState([]);

  const [genderOptions] = useState([
    { value: "" },
    { value: 1, label: "Female" },
    { value: 0, label: "Male" },
  ]);

  const [roleOptions] = useState([ //types ile yap
    { value: "" },
    { value: 2, label: "Employee" },
    { value: 1, label: "Manager" },
    { value: 0, label: "Admin" },
  ]);

  useEffect(() => {
    if (props.selectedItemId) {
      employeeService.getEmployeeById(props.selectedItemId).then((data) => {
        props.setSelectedEmployee(data);
      })
        .catch((err) => {
          showErrorToast(err);
        });
    }

    teamService.getAllTeam().then((data) => {
      setTeams(data);
    })
    employeeTitleService.getAllEmployeeTitle().then((data) => {
      setTitles(data);
    })

  }, [props.selectedItemId]);

  const handleChange = (e: any) => {
    const { name, value } = e.target;
    if (name === "gender" || name === "employeeRole") {
      props.setSelectedEmployee((prev: any) => ({
        ...prev,
        [name]: parseInt(value, 10),
      }));
    }
    else {
      props.setSelectedEmployee((prev: any) => ({
        ...prev,
        [name]: value,
        teamId: name === 'team' ? value : prev.teamId,
        employeeTitleId: name === 'title' ? value : prev.employeeTitleId,
      }));
    }
  };

  const handleSubmit = (e: any) => {
    e.preventDefault();
    employeeService
      .addOrUpdateEmployee({
        ...props.selectedEmployee,
        teamId: props.selectedEmployee.teamId,
        employeeTitleId: props.selectedEmployee.employeeTitleId || null
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
        id="employeeAddModal"
        role="dialog"
        data-backdrop="static"
      >
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
                    required
                    name="employeeRole"
                    className="form-control"
                    value={
                      props.selectedEmployee &&
                      props.selectedEmployee.employeeRole
                    }
                    onChange={(e: any) => handleChange(e)}
                  >
                    {roleOptions.map((option: any) => (
                      <option
                        key={option.value}
                        value={option.value}
                        hidden={option.value === ""}
                      >
                        {option.label}
                      </option>
                    ))}
                  </select>

                  <label className="col-form-label">Gender:</label>
                  <select
                    required
                    name="gender"
                    className="form-control"
                    value={
                      props.selectedEmployee && props.selectedEmployee.gender
                    }
                    onChange={(e: any) => handleChange(e)}
                  >
                    {genderOptions.map((option: any) => (
                      <option
                        key={option.value}
                        value={option.value}
                        hidden={option.value === ""}
                      >
                        {option.label}
                      </option>
                    ))}
                  </select>
                  <label className="col-form-label">Title:</label>
                  <select
                    name="title"
                    className="form-control"
                    value={props.selectedEmployee?.employeeTitleId || ""}
                    onChange={(e: any) => handleChange(e)}
                  >
                    <option value=""></option>
                    {titles.map((title: any) => (
                      <option key={title.id} value={title.id}>{title.titleName}</option>
                    ))}
                  </select>

                  <label className="col-form-label">Team:</label>
                  <select
                    name="team"
                    className="form-control"
                    value={props.selectedEmployee?.teamId || ""}
                    onChange={(e: any) => handleChange(e)}
                    required
                  >
                    <option value=""></option>
                    {teams.map((team: any) => (
                      <option key={team.id} value={team.id}>{team.name}</option>
                    ))}
                  </select>

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
                  {props.modalModeName === "Add" ? (
                    <>
                      <label className="col-form-label">Password:</label>
                      <Input
                        type="password"
                        name="password"
                        value={
                          props.selectedEmployee &&
                          props.selectedEmployee.password
                        }
                        onChange={(e: any) => handleChange(e)}
                        required
                      />
                    </>
                  ) : (
                    <></>
                  )}
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
