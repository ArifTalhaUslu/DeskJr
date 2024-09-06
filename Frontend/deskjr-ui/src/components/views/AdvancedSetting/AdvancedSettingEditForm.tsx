import React, { useState, useEffect } from "react";
import Button from "../../CommonComponents/Button";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import AdvancedSettingService from "../../../services/AdvancedSettingService";
import NumberInput from "../../CommonComponents/NumberInput";
import { Setting } from "../../../types/advancedSetting";
import teamService from "../../../services/TeamService";

const AdvancedSettingEditForm: any = () => {
  const [accruedDay, setAccruedDay] = useState<string>("0");
  const [humanResourcesTeam, setHumanResourcesTeam] = useState<string>("");
  const [teams, setTeams] = useState([]);

  const handleSubmit = async (e: any) => {
    e.preventDefault();

    if (!humanResourcesTeam) {
      showErrorToast("Please select a Human Resources team");
      return;
    }

    const updatedSettings = [
      { key: "Accrued Day", value: accruedDay },
      { key: "Human Resources Team", value: humanResourcesTeam },
    ];

    try {
      const currentSettings = await AdvancedSettingService.getAllSetting();

      currentSettings.map(async (setting: any) => {
        await AdvancedSettingService.deleteSetting(setting.id);
      });
      await AdvancedSettingService.updateMultipleSettings(updatedSettings);
      showSuccessToast("Settings updated successfully");
    } catch (error) {
      showErrorToast(error);
    }
  };

  useEffect(() => {
    teamService.getAllTeam().then((data) => {
      setTeams(data);
    });
    AdvancedSettingService.getAllSetting()
      .then((data) => {
        const accruedDaySetting = data.find(
          (item: Setting) => item.key === "Accrued Day"
        );
        if (accruedDaySetting) setAccruedDay(accruedDaySetting.value);
        const hrTeamSetting = data.find(
          (item: Setting) => item.key === "Human Resources Team"
        );
        if (hrTeamSetting) setHumanResourcesTeam(hrTeamSetting.value);
      })
      .catch((err) => {
        showErrorToast(err);
      });
  }, []);

  return (
    <div className="card">
      <div className="card-body">
        <NumberInput
          key="Accrued Day"
          label="Accrued Day"
          value={accruedDay}
          onChange={(newValue: string) => setAccruedDay(newValue)}
        />
        <div key="Human Resources team" className="form-group">
          <label htmlFor="Human Resources team">Human Resources team</label>
          <select
            name="team"
            className="form-control"
            value={humanResourcesTeam || ""}
            onChange={(e: any) => setHumanResourcesTeam(e.target.value)}
            required
          >
            <option value=""></option>
            {teams.map((team: any) => (
              <option key={team.id} value={team.id}>
                {team.name}
              </option>
            ))}
          </select>
        </div>

        <Button
          type="submit"
          className="btn btn-primary mt-2"
          text="Submit"
          onClick={handleSubmit}
        />
      </div>
    </div>
  );
};

export default AdvancedSettingEditForm;
