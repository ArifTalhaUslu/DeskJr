import React, { useEffect, useState } from "react";
import surveyService from "../../../services/SurveyService";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import Button from "../../CommonComponents/Button";
import Input from "../../CommonComponents/Input";

const SurveyEditForm = ({ selectedItemId, modalModeName, onClose, getList }) => {
    const [selectedSurvey, setSelectedSurvey] = useState({
        id: "",
        name: "",
    });

    useEffect(() => {
        if (modalModeName === "Update" && selectedItemId) {
            surveyService
                .getSurveyById(selectedItemId)
                .then((data) => setSelectedSurvey(data))
                .catch((err) => showErrorToast(err));
        } else if (modalModeName === "Add") {
            setSelectedSurvey({
                id: "",
                name: "",
            });
        }
    }, [selectedItemId, modalModeName]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setSelectedSurvey((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const surveyData = modalModeName === "Add" ? { name: selectedSurvey.name } : selectedSurvey;
            await surveyService.addOrUpdateSurvey(surveyData);
            showSuccessToast("Successful!");
            getList();
            onClose();
        } catch (err) {
            showErrorToast(err);
        }
    };

    return (
        <div className="modal fade show d-block" role="dialog" style={{ backgroundColor: "rgba(0,0,0,0.5)" }}>
            <div className="modal-dialog" role="document">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title">{modalModeName} Survey</h5>
                        <button type="button" className="close" onClick={onClose}>
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <form onSubmit={handleSubmit}>
                        <div className="modal-body">
                            {modalModeName === "Update" && (
                                <Input
                                    type="hidden"
                                    name="id"
                                    value={selectedSurvey.id}
                                />
                            )}
                            <div className="form-group">
                                <label className="col-form-label">Name:</label>
                                <Input
                                    type="text"
                                    name="name"
                                    value={selectedSurvey.name}
                                    onChange={handleChange}
                                    required
                                />
                            </div>
                        </div>
                        <div className="modal-footer">
                            <Button type="submit" className="btn btn-primary" text="Submit" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default SurveyEditForm;
