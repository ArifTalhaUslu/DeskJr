import React, { useEffect, useState } from "react";
import surveyQuestionOptionService from "../../../services/SurveyQuestionOptionService";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import Card from "../../CommonComponents/Card";
import Board from "../../CommonComponents/Board";
import SurveyQuestionOptionsEditForm from "./SurveyQuestionOptionsEditForm";
import ConfirmDelete from "../../CommonComponents/ConfirmDelete";


const SurveyQuestionOptionsForm = ({ selectedQuestionId, onClose }) => {
    const [items, setItems] = useState([]);
    const [selectedQuestionOptionsId, setSelectedQuestionOptionsId] = useState("");
    const [modalMode, setModalMode] = useState("");
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [formToBeClosed, setFormToBeClosed] = useState("");

    useEffect(() => {
        if (selectedQuestionId) {
            getList(selectedQuestionId);
        }
    }, [selectedQuestionId]);

    const getList = async (selectedQuestionId) => {
        try {
            const data = await surveyQuestionOptionService.getSurveyQuestionOptionsBySurveyQuestionId(selectedQuestionId);
            setItems(data);
        } catch (err) {
            showErrorToast(err);
        }
    };

    const handleEdit = (questionOptions) => {
        setSelectedQuestionOptionsId(questionOptions.id);
        setModalMode("Update");
        setIsModalOpen(true);
    };

    const handleDelete = (questionOptions) => {
        setSelectedQuestionOptionsId(questionOptions.id);
        setModalMode("Delete");
        setIsModalOpen(true);
        setFormToBeClosed("delete-form-closed");
    };

    const onConfirmDelete = async (e) => {
        e.preventDefault();
        if (selectedQuestionOptionsId) {
            try {
                await surveyQuestionOptionService.deleteSurveyQuestionOptions(selectedQuestionOptionsId);
                showSuccessToast("Successful!");
                getList(selectedQuestionId);
            } catch (err) {
                showErrorToast(err);
            }
        }
        closeModal();
    };

    const closeModal = () => {
        setSelectedQuestionOptionsId("");
        setModalMode("");
        setIsModalOpen(false);
    };

    return (
        <div className="modal fade show d-block" id="questionFormModal" role="dialog" data-backdrop="static" style={{ backgroundColor: "rgba(0,0,0,0.5)" }}>
            <div className="modal-dialog modal-lg" role="document">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title">Manage Survey Question Options</h5>
                        <button type="button" className="close" onClick={onClose}>
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div className="modal-body">
                        <Card title={"Survey Question Options"}>
                            <Board
                                items={items}
                                onEdit={handleEdit}
                                onDelete={handleDelete}
                                isEditable={() => true}
                                isDeletable={() => true}
                                hiddenColumns={["id", "surveyQuestionId", "surveyQuestion"]}
                                renderColumn={(column, value) => value}
                                isCustomColumnExist={"false"}
                                columnNames={{
                                    text: "Survey Question Option Text",
                                }}
                                hasNewRecordButton={true}
                                newRecordButtonOnClick={() => {
                                    setSelectedQuestionOptionsId("");
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
                        <SurveyQuestionOptionsEditForm
                            selectedQuestionId={selectedQuestionId}
                            selectedQuestionOptionsId={selectedQuestionOptionsId}
                            modalModeName={modalMode}
                            onclose={closeModal}
                            getList={() => getList(selectedQuestionId)}
                        />
                    ) : modalMode === "Delete" ? (
                        <ConfirmDelete
                            onConfirm={onConfirmDelete}
                            selectedItemId={selectedQuestionId}
                            onClose={closeModal}
                        />
                    ) : null}
                </>
            )}
        </div>
    );
};

export default SurveyQuestionOptionsForm;
