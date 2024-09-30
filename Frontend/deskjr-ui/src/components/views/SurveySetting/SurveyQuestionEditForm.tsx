import React, { useEffect, useState } from "react";
import surveyQuestionService from "../../../services/SurveyQuestionService";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import Button from "../../CommonComponents/Button";
import Input from "../../CommonComponents/Input";

const SurveyQuestionEditForm = ({
    selectedSurveyQuestionItemId,
    selectedItemId,
    modalModeName,
    onClose,
    getList
}) => {
    const [surveyQuestion, setSurveyQuestion] = useState({
        id: "",
        text: "",
        surveyId: selectedItemId
    });

    useEffect(() => {
        if (selectedSurveyQuestionItemId && modalModeName === "Update") {
            surveyQuestionService
                .getSurveyQuestionById(selectedSurveyQuestionItemId)
                .then((data) => setSurveyQuestion(data))
                .catch((err) => showErrorToast(err));
        } else if (modalModeName === "Add") {
            setSurveyQuestion({
                id: "",
                text: "",
                surveyId: selectedItemId
            });
        }
    }, [selectedSurveyQuestionItemId, modalModeName, selectedItemId]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setSurveyQuestion((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const { id, ...dataToSend } = surveyQuestion;

        try {
            if (modalModeName === "Add") {
                await surveyQuestionService.addOrUpdateSurveyQuestion(dataToSend);
            } else {
                await surveyQuestionService.addOrUpdateSurveyQuestion(surveyQuestion);
            }

            showSuccessToast("Successful!");
            onClose();
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
                        <h5 className="modal-title">{modalModeName} Survey Question</h5>
                        <button type="button" className="close" onClick={onClose}>
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <form onSubmit={handleSubmit}>
                        <div className="modal-body">
                            <Input
                                type="hidden"
                                name="id"
                                value={surveyQuestion.id}
                            />
                            <div className="form-group">
                                <label className="col-form-label">Text:</label>
                                <Input
                                    type="text"
                                    name="text"
                                    value={surveyQuestion.text}
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

export default SurveyQuestionEditForm;