import { useEffect, useState } from "react";
import teamService from "../../../services/TeamService";
import Input from "../../CommonComponents/Input";
import Button from "../../CommonComponents/Button";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import employeeService from "../../../services/EmployeeService";
import { Roles } from "../../../types/Roles";

const TeamEditForm: any = (props: any) => {
  const [managers, setManagers] = useState([]);
  const [allTeams, setAllTeams] = useState<any[]>([]);


  useEffect(() => {
    if (props.selectedItemId) {
      teamService.getTeamById(props.selectedItemId)
        .then((data) => {
          props.setSelectedTeam(data);
        })
        .catch((err) => {
          showErrorToast(err);
        });
    }

    employeeService.getAllEmployee().then((data) => {
      const managers = data.filter((employee: any) => (employee.employeeRole !== Roles.Employee));
      setManagers(managers);
    });
    teamService.getAllTeam().then((data) => {
      setAllTeams(data);
    });
  }, [props.selectedItemId]);

    
  const handleChange = (e: any) => {
    const { name, value } = e.target;
    props.setSelectedTeam((prevState: any) => ({
        ...prevState,
        [name]: value ,
        managerId: name === 'manager' ? value  : prevState.managerId,  
        upTeamId: name === 'upTeam' ? value : prevState.upTeamId,  
    }))
    ;
};

  const handleSubmit = (e: any) => {
    e.preventDefault();
    teamService
      .addOrUpdateTeam({
        ...props.selectedTeam,
        managerId: props.selectedTeam.managerId || null,
        upTeamId: props.selectedTeam.upTeamId || null,
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

  return (
    <>
      <div
        className="modal fade"
        id="teamAddModal"
        role="dialog"
        data-backdrop="static"
      >
        <div className="modal-dialog" role="document">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title">{props.modalModeName} Team</h5>
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
                  value={props.selectedTeam && props.selectedTeam.id}
                />
                
                <div className="form-group">
                  <label className="col-form-label">Name:</label>
                  <Input
                    type="text"
                    name="name"
                    value={props.selectedTeam && props.selectedTeam.name}
                    onChange={(e: any) => handleChange(e)}
                    required
                  />
                  <label className="col-form-label">Manager:</label>
                  <select
                    name="manager"
                    className="form-control"
                    value={props.selectedTeam?.managerId || ""}
                    onChange={(e: any) => handleChange(e)}
                  >
                    <option value=""></option>
                    {managers.map((manager: any) => (
                      <option key={manager.id} value={manager.id}>
                        {manager.name}
                      </option>))}
                  </select>
                  <label className="col-form-label">UpTeam:</label>
                  <select
                    name="upTeam"
                    className="form-control"
                    value={props.selectedTeam && props.selectedTeam.upTeamId}
                    onChange={(e: any) => handleChange(e)}
                  >
                    <option value=""></option>
                    {allTeams.map((team:any) => (
                      <option key={team.id} value={team.id}>
                        {team.name}
                      </option>))}
                  </select>
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
export default TeamEditForm;
