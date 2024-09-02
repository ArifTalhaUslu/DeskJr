import React, { useState, useEffect } from "react";
import AdvancedSettingEditForm from "./AdvancedSettingEditForm";
import { showErrorToast } from "../../../utils/toastHelper";
import { Setting, settings } from "../../../types/advancedSetting";

const AdvancedSetting: React.FC = () => {
  const [settingsData, setSettingsData] = useState<Setting[]>(settings);
  const handleSettingsUpdate = (updatedSettings: Setting[]) => {
    setSettingsData(updatedSettings);
  };

  return (
    <>
      <div className="container mt-5">
        <div className="row">
          <div className="col-10 mx-auto">
            <div className="card bg-primary text-white mb-4">
              <div className="card-body">
                <h1 className="card-title mb-3">Advanced Setting</h1>
              </div>
            </div>
            <AdvancedSettingEditForm
              settings={settingsData}
              onSubmit={handleSettingsUpdate}
            />
          </div>
        </div>
      </div>
    </>
  );
};

export default AdvancedSetting;
