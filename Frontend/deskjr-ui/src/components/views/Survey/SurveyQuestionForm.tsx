import React, { useEffect, useState } from "react";
import surveyQuestionService from "../../../services/SurveyQuestionService";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import Card from "../../CommonComponents/Card";
import Board from "../../CommonComponents/Board";
import SurveyQuestionEditForm from "./SurveyQuestionEditForm";
import SurveyQuestionOptionsForm from "./SurveyQuestionOptionsForm";
import ConfirmDelete from "../../CommonComponents/ConfirmDelete";
import Button from "../../CommonComponents/Button";

const SurveyQuestionForm = ({ selectedItemId, onClose }) => {
    const [items, setItems] = useState([]);
    const [selectedQuestionId, setSelectedQuestionId] = useState("");
    const [modalMode, setModalMode] = useState("");
    const [isModalOpen, setIsModalOpen] = useState(false);

    useEffect(() => {
        if (selectedItemId) {
            getList(selectedItemId);
        }
    }, [selectedItemId]);

    const getList = async (surveyId) => {
        try {
            const data = await surveyQuestionService.getSurveyQuestionsBySurveyId(surveyId);
            setItems(data);
        } catch (err) {
            showErrorToast(err);
        }
    };

    const handleEdit = (question) => {
        setSelectedQuestionId(question.id);
        setModalMode("Update");
        setIsModalOpen(true);
    };

    const handleDelete = (question) => {
        setSelectedQuestionId(question.id);
        setModalMode("Delete");
        setIsModalOpen(true);
    };

    const onConfirmDelete = async () => {
        if (selectedQuestionId) {
            try {
                await surveyQuestionService.deleteSurveyQuestion(selectedQuestionId);
                showSuccessToast("Successful!");
                getList(selectedItemId);
            } catch (err) {
                showErrorToast(err);
            }
        }
        closeModal();
    };

    const closeModal = () => {
        setSelectedQuestionId("");
        setModalMode("");
        setIsModalOpen(false);
    };

    const customColumnOfActions = (item) => (
        <div className="text-center">
            <Button
                text="Question Options"
                className="btn btn-info m-1 p-2"
                onClick={() => {
                    setSelectedQuestionId(item.id);
                    setModalMode("Options");
                    setIsModalOpen(true);
                }}
            />
        </div>
    );


    return (
        <div className="modal fade show d-block" id="questionFormModal" role="dialog" data-backdrop="static" style={{ backgroundColor: "rgba(0,0,0,0.5)" }}>
            <div className="modal-dialog modal-lg" role="document">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title">Manage Survey Questions</h5>
                        <button type="button" className="close" onClick={onClose}>
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div className="modal-body">
                        <Card title={"Survey Questions"}>
                            <Board
                                items={items}
                                onEdit={handleEdit}
                                onDelete={handleDelete}
                                isEditable={() => true}
                                isDeletable={() => true}
                                hiddenColumns={["id", "surveyId", "surveyQuestionOptions"]}
                                renderColumn={(column, value) => value}
                                customColumnOfActions={customColumnOfActions}
                                columnNames={{
                                    text: "Survey Question Text",
                                    customColumnName: "Manage Survey Question Options"
                                }}
                                hasNewRecordButton={true}
                                newRecordButtonOnClick={() => {
                                    setSelectedQuestionId("");
                                    setModalMode("Add");
                                    setIsModalOpen(true);
                                }}
                            />
                        </Card>
                    </div>
                </div>
            </div>

            {isModalOpen && (
                <>
                    {modalMode === "Add" || modalMode === "Update" ? (
                        <SurveyQuestionEditForm
                            selectedSurveyQuestionItemId={selectedQuestionId}
                            selectedItemId={selectedItemId}
                            modalModeName={modalMode}
                            onClose={closeModal}
                            getList={() => getList(selectedItemId)}
                        />
                    ) : modalMode === "Delete" ? (
                        <ConfirmDelete
                            onConfirm={onConfirmDelete}
                            selectedItemId={selectedQuestionId}
                            onClose={closeModal}
                        />
                    ) : modalMode === "Options" ? (
                        <SurveyQuestionOptionsForm
                            selectedQuestionId={selectedQuestionId}
                            onClose={closeModal}
                        />
                    ) : null}
                </>
            )}
        </div>
    );
};

export default SurveyQuestionForm;
