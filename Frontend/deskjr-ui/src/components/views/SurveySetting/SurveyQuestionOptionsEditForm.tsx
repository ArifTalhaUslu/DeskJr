import React, { useEffect, useState } from "react";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import Button from "../../CommonComponents/Button";
import Input from "../../CommonComponents/Input";
import surveyQuestionOptionService from "../../../services/SurveyQuestionOptionService";

const SurveyQuestionOptionsEditForm = ({
    selectedQuestionId,
    selectedQuestionOptionsId,
    modalModeName,
    onclose,
    getList,
}) => {
    const [surveyQuestionOption, setSurveyQuestionOption] = useState({
        id: "",
        text: "",
        surveyQuestionId: selectedQuestionId
    });

    useEffect(() => {
        if (selectedQuestionOptionsId && modalModeName === "Update") {
            surveyQuestionOptionService
                .getSurveyQuestionsOptionsById(selectedQuestionOptionsId)
                .then((data) => setSurveyQuestionOption(data))
                .catch((err) => showErrorToast(err));
        } else if (modalModeName === "Add") {
            setSurveyQuestionOption({
                id: "",
                text: "",
                surveyQuestionId: selectedQuestionId
            });
        }
    }, [selectedQuestionOptionsId, modalModeName, selectedQuestionId]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setSurveyQuestionOption((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const { id, ...dataToSend } = surveyQuestionOption;

        try {
            if (modalModeName === "Add") {
                await surveyQuestionOptionService.addSurveyQuestionOptions(dataToSend);
            } else {
                await surveyQuestionOptionService.updateSurveyQuestionOptions(surveyQuestionOption);
            }

            showSuccessToast("Successful!");
            onclose();
            getList();
        } catch (err) {
            showErrorToast(err);
        }
    };

    return (
        <div className="modal fade show d-block" role="dialog" style={{ backgroundColor: "rgba(0,0,0,0.5)" }}>
            <div className="modal-dialog" role="document">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title">{modalModeName} Survey Question Options</h5>
                        <button type="button" className="close" onClick={onclose}>
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <form onSubmit={handleSubmit}>
                        <div className="modal-body">
                            <Input
                                type="hidden"
                                name="id"
                                value={surveyQuestionOption.id}
                            />
                            <div className="form-group">
                                <label className="col-form-label">Text:</label>
                                <Input
                                    type="text"
                                    name="text"
                                    value={surveyQuestionOption.text}
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

export default SurveyQuestionOptionsEditForm;