import React, { useState, useEffect } from "react";
import Button from "../../CommonComponents/Button";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import { Setting } from "../../../types/advancedSetting";
import AdvancedSettingService from "../../../services/AdvancedSettingService";
import NumberInput from "../../CommonComponents/NumberInput";

type AdvancedSettingEditFormProps = {
  settings: Setting[];
  onSubmit: (updatedSettings: Setting[]) => void;
};

const AdvancedSettingEditForm: any = ({ settings }) => {
  const [updatedSettings, setUpdatedSettings] = useState(settings);

  const handleValueChange = (id: any, newValue: any) => {
    setUpdatedSettings((prevSettings) =>
      prevSettings.map((setting) =>
        setting.id === id ? { ...setting, value: newValue } : setting
      )
    );
  };

  const handleSubmit = async (e: any) => {
    e.preventDefault();
    console.log(updatedSettings);

    try {
      await AdvancedSettingService.updateMultipleSettings(updatedSettings);
    } catch (error) {}
  };

  useEffect(() => {
    AdvancedSettingService.getAllSetting()
      .then((data) => {
        setUpdatedSettings(data);
      })
      .catch((err) => {
        showErrorToast(err);
      });
  }, []);

  return (
    <div className="card">
      <div className="card-body">
        {updatedSettings.map((setting) => (
          <NumberInput
            id={setting.id}
            label={setting.key}
            value={setting.value}
            key={setting.id}
            onValueChange={handleValueChange}
          />
        ))}
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
