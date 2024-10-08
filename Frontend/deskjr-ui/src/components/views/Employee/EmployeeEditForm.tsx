import Button from "../../CommonComponents/Button";
import Input from "../../CommonComponents/Input";
import { useEffect, useState } from "react";
import employeeService from "../../../services/EmployeeService";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import teamService from "../../../services/TeamService";
import employeeTitleService from "../../../services/EmployeeTitleService";
import { Roles } from "../../../types/Roles";
import ImageUpload from "../../CommonComponents/ImageUpload";
const EmployeeEditForm: any = (props: any) => {
  const [teams, setTeams] = useState([]);
  const [titles, setTitles] = useState([]);
  const [imageBase64, setImageBase64] = useState(null);

  const handleImageUpload = (base64Image: string) => {
    setImageBase64(base64Image);
  };
  const [genderOptions] = useState([
    { value: "" },
    { value: 1, label: "Female" },
    { value: 0, label: "Male" },
  ]);

  const [roleOptions] = useState([
    { value: "" },
    { value: Roles.Employee, label: Roles[Roles.Employee] },
    { value: Roles.Manager, label: Roles[Roles.Manager] },
    { value: Roles.Admin, label: Roles[Roles.Admin] },
  ]);

  useEffect(() => {
    if (props.selectedItemId) {
      employeeService
        .getEmployeeById(props.selectedItemId)
        .then((data) => {
          props.setSelectedEmployee(data);
          setImageBase64(data.base64Image);
        })
        .catch((err) => {
          showErrorToast(err);
        });
    } else if (props.selectedEmployee && props.selectedEmployee.base64Image) {
      setImageBase64(props.selectedEmployee.base64Image);
    }
    teamService.getAllTeam().then((data) => {
      setTeams(data);
    });
    employeeTitleService.getAllEmployeeTitle().then((data) => {
      setTitles(data);
    });
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
        teamId: name === "team" ? value : prev.teamId,
        employeeTitleId: name === "title" ? value : prev.employeeTitleId,
      }));
    }
  };

  const handleSubmit = (e: any) => {
    e.preventDefault();
    employeeService
      .addOrUpdateEmployee({
        ...props.selectedEmployee,
        base64Image: imageBase64,
        teamId: props.selectedEmployee.teamId,
        employeeTitleId: props.selectedEmployee.employeeTitleId || null,
        employeeRole: props.selectedEmployee.employeeRole,
      })
      .then(() => {
        showSuccessToast("Successful!");
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
                  const close_button = document.getElementById("form-close");
                  close_button?.click();
                  props.onClose();
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

                  <label className="col-form-label">Role:</label>
                  <select
                    required
                    name="employeeRole"
                    className="form-control"
                    value={
                      props.selectedEmployee &&
                      props.selectedEmployee.employeeRole
                    }
                    disabled={props.currentUser.employeeRole !== Roles.Admin}
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
                  <label className="col-form-label">Title:</label>
                  <select
                    name="title"
                    className="form-control"
                    value={props.selectedEmployee?.employeeTitleId || ""}
                    disabled={props.currentUser.employeeRole !== Roles.Admin}
                    onChange={(e: any) => handleChange(e)}
                  >
                    <option value=""></option>
                    {titles.map((title: any) => (
                      <option key={title.id} value={title.id}>
                        {title.titleName}
                      </option>
                    ))}
                  </select>

                  <label className="col-form-label">Team:</label>
                  <select
                    name="team"
                    className="form-control"
                    value={props.selectedEmployee?.teamId || ""}
                    onChange={(e: any) => handleChange(e)}
                    disabled={props.currentUser.employeeRole !== Roles.Admin}
                    required
                  >
                    <option value=""></option>
                    {teams.map((team: any) => (
                      <option key={team.id} value={team.id}>
                        {team.name}
                      </option>
                    ))}
                  </select>

                  <label className="col-form-label">E-mail:</label>
                  <Input
                    type="email"
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
                  <label className="col-form-label">Upload Image:</label>
                  <ImageUpload
                    onUpload={handleImageUpload}
                    imageBase64={imageBase64}
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
