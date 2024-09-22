import React, { useEffect, useState } from "react";
import surveyService from "../../../services/SurveyService";
import Card from "../../CommonComponents/Card";
import Board from "../../CommonComponents/Board";
import ConfirmDelete from "../../CommonComponents/ConfirmDelete";
import SurveyEditForm from "./SurveyEditForm";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import Button from "../../CommonComponents/Button";
import SurveyQuestionForm from "./SurveyQuestionForm";

const Survey = () => {
    const [items, setItems] = useState([]);
    const [selectedItemId, setSelectedItemId] = useState("");
    const [modalMode, setModalMode] = useState("");
    const [isModalOpen, setIsModalOpen] = useState(false);

    useEffect(() => {
        getList();
    }, []);

    const getList = async () => {
        try {
            const data = await surveyService.getAllSurvey();
            setItems(data);
        } catch (err) {
            showErrorToast(err);
        }
    };

    const onConfirmDelete = async (e) => {
        if (selectedItemId) {
            try {
                await surveyService.deleteSurvey(selectedItemId);
                showSuccessToast("Successful!");
                getList();
            } catch (err) {
                showErrorToast(err);
            }
        }
        closeModal();
    };

    const handleEdit = (survey) => {
        setSelectedItemId(survey.id);
        setModalMode("Update");
        setIsModalOpen(true);
    };

    const handleDelete = (survey) => {
        setSelectedItemId(survey.id);
        setModalMode("Delete");
        setIsModalOpen(true);
    };

    const closeModal = () => {
        setSelectedItemId("");
        setModalMode("");
        setIsModalOpen(false);
    };

    const customColumnOfActions = (item) => (
        <div className="text-center">
            <Button
                text="Questions"
                className="btn btn-info m-1 p-2"
                onClick={() => {
                    setSelectedItemId(item.id);
                    setModalMode("Questions");
                    setIsModalOpen(true);
                }}
            />
        </div>
    );

    return (
        <>
            <Card title={"Surveys"}>
                <Board
                    items={items}
                    onEdit={handleEdit}
                    onDelete={handleDelete}
                    isEditable={() => true}
                    isDeletable={() => true}
                    hiddenColumns={["id"]}
                    renderColumn={(column, value) => value}
                    customColumnOfActions={customColumnOfActions}
                    columnNames={{
                        name: "Survey Name",
                        customColumnName: "Manage Survey Questions"
                    }}
                    hasNewRecordButton={true}
                    newRecordButtonOnClick={() => {
                        setModalMode("Add");
                        setIsModalOpen(true);
                    }}
                />
            </Card>

            {isModalOpen && (
                <>
                    {modalMode === "Add" || modalMode === "Update" ? (
                        <SurveyEditForm
                            selectedItemId={selectedItemId}
                            modalModeName={modalMode}
                            onClose={closeModal}
                            getList={getList}
                        />
                    ) : modalMode === "Delete" ? (
                        <ConfirmDelete
                            onConfirm={onConfirmDelete}
                            selectedItemId={selectedItemId}
                            onClose={closeModal}
                        />
                    ) : modalMode === "Questions" ? (
                        <SurveyQuestionForm
                            selectedItemId={selectedItemId}
                            onClose={closeModal}
                        />
                    ) : null}
                </>
            )}
        </>
    );
};

export default Survey;