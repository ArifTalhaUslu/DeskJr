import React, { useEffect, useState } from "react";
import Button from "../../CommonComponents/Button";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import surveyService from "../../../services/SurveyService";
import { RadioButton } from 'primereact/radiobutton';
import employeeAnswersService from "../../../services/EmployeeAnswersService";

const SurveyForm = (props: any) => {
    const [items, setItems] = useState([]);
    const [selectedAnswers, setSelectedAnswers] = useState<{ [key: string]: string | null }>({});
    const [survey, setSurvey] = useState({
        id: props.selectedItemId,
        name: "",
    });

    useEffect(() => {
        getList(props.selectedItemId);
    }, [props.selectedItemId]);

    useEffect(() => {
        if (items.length > 0) {
            const surveyData = items[0];
            setSurvey({
                id: surveyData.id,
                name: surveyData.name
            });
        }
    }, [items]);

    const getList = async (surveyId: any) => {
        if (props.selectedItemId)
            try {
                const data = await surveyService.getSurveyAllElements(surveyId);
                setItems(data);
            } catch (err) {
                showErrorToast(err);
            }
    };

    const onChangeValue = (questionId: string, optionId: string) => {
        setSelectedAnswers(prevState => ({
            ...prevState,
            [questionId]: prevState[questionId] === optionId ? null : optionId
        }));
    };

    const handleSubmit = (e: any) => {
        e.preventDefault();
        const payload = Object.entries(selectedAnswers).map(([questionId, optionId]) => ({
            userId: props.currentUser?.id,
            optionId
        }));

        employeeAnswersService
            .addRangeEmployeeOptions(payload)
            .then(() => {
                showSuccessToast("Success");
                props.onClose();
                setSelectedAnswers({});
                props.setStatus(true);
            })
            .catch((err: any) => {
                showErrorToast(err);
            });
    };

    return (
        <>
            <div
                className="modal fade"
                id="survey-parcipation"
                role="dialog"
                data-backdrop="static"
            >
                <div className="modal-dialog" role="document">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title">{props.modalModeName}</h5>
                            <button
                                type="button"
                                className="close"
                                id="form-close-survey"
                                data-dismiss="modal"
                                hidden
                            ></button>
                            <button
                                type="button"
                                className="close"
                                onClick={() => {
                                    props.onClose();
                                    const close_button = document.getElementById("form-close-survey");
                                    close_button?.click();
                                }}
                            >
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <form onSubmit={handleSubmit}>
                            <div className="modal-body">
                                <div className="form-group">
                                    <div>
                                        {props.status ? (
                                            <h4>You have participated in this survey before</h4>
                                        ) : (
                                            items.map((survey) => (
                                                <div key={survey.id}>
                                                    <h2>{survey.name}</h2>
                                                    <ul>
                                                        {survey.surveyQuestions.map((question) => (
                                                            <li key={question.id}>
                                                                <strong>{question.text}</strong>
                                                                <ul>
                                                                    {question.surveyQuestionOptions.map((option) => (
                                                                        <div key={option.id}>
                                                                            <RadioButton
                                                                                inputId={option.id}
                                                                                name={`question-${question.id}`}
                                                                                value={option.id}
                                                                                onChange={() => onChangeValue(question.id, option.id)}
                                                                                checked={selectedAnswers[question.id] === option.id}
                                                                            />
                                                                            {option.text}
                                                                        </div>
                                                                    ))}
                                                                </ul>
                                                            </li>
                                                        ))}
                                                    </ul>
                                                </div>
                                            ))
                                        )}
                                    </div>
                                </div>
                            </div>
                            <div className="modal-footer">
                                {!props.status && (
                                    <Button
                                        type="submit"
                                        className="btn btn-primary"
                                        text="Submit"
                                    />
                                )}
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </>
    );
};

export default SurveyForm;
