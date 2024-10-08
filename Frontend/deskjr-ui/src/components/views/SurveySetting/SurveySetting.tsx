// import React, { useEffect, useState } from "react";
// // import surveyService from "../../../services/SurveyService";
// import Card from "../../CommonComponents/Card";
// import Board from "../../CommonComponents/Board";
// import ConfirmDelete from "../../CommonComponents/ConfirmDelete";
// // import SurveyEditForm from "./SurveyEditForm";
// import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
// import Button from "../../CommonComponents/Button";
// // import SurveyQuestionForm from "./SurveyQuestionForm";
// import { formatDate } from "date-fns";
// // import SurveyResultDetails from "./SurveyResultDetails";

// const SurveySetting = (props: any) => {
//     const [items, setItems] = useState([]);
//     const [selectedItemId, setSelectedItemId] = useState("");
//     const [modalMode, setModalMode] = useState("");
//     const [isModalOpen, setIsModalOpen] = useState(false);


//     useEffect(() => {
//         getList();
//     }, []);

//     const getList = async () => {
//         try {
//             const data = await surveyService.getAllSurvey();
//             setItems(data);
//         } catch (err) {
//             showErrorToast(err);
//         }
//     }

//     const onConfirmDelete = async () => {
//         if (selectedItemId) {
//             try {
//                 await surveyService.deleteSurvey(selectedItemId);
//                 showSuccessToast("Successful!");
//                 getList();
//             } catch (err) {
//                 showErrorToast(err);
//             }
//         }
//         closeModal();
//     };

//     const handleEdit = (survey) => {
//         setSelectedItemId(survey.id);
//         setModalMode("Update");
//         setIsModalOpen(true);
//     };

//     const handleDelete = (survey) => {
//         setSelectedItemId(survey.id);
//         setModalMode("Delete");
//         setIsModalOpen(true);
//     };

//     const closeModal = () => {
//         setSelectedItemId("");
//         setModalMode("");
//         setIsModalOpen(false);
//     };

//     const customColumnManageQuestions = (item) => (
//         <div className="text-center">
//             <Button
//                 text="Questions"
//                 className="btn btn-info m-1 p-2"
//                 onClick={() => {
//                     setSelectedItemId(item.id);
//                     setModalMode("Questions");
//                     setIsModalOpen(true);
//                 }}
//             />
//         </div>
//     );

//     const customColumnSurveyDetails = (item) => (
//         <div className="text-center">
//             <Button
//                 text="Results"
//                 className="btn btn-secondary p-2"
//                 style={{ width: '100%', height: '100%' }}
//                 onClick={() => {
//                     setSelectedItemId(item.id);
//                     setModalMode("ResultDetails");
//                     setIsModalOpen(true);
//                 }}
//             />
//         </div>
//     );

//     const renderColumn = (column: string, value: any) => {
//         if (column === "endDate") {
//             return formatDate(new Date(value), "dd/MM/yyyy");
//         }
//         return value;
//     };

//     return (
//         <>
//             <Card title={"Surveys"}>
//                 <Board
//                     items={items}
//                     onEdit={handleEdit}
//                     onDelete={handleDelete}
//                     isEditable={() => true}
//                     isDeletable={() => true}
//                     hiddenColumns={["id", "surveyQuestions"]}
//                     renderColumn={renderColumn}
//                     customColumn={customColumnManageQuestions}
//                     isCustomColumnExist={"true"}
//                     columnNames={{
//                         name: "Survey Name",
//                         endDate: "End Date",
//                         customColumnName: "Manage Survey Questions"
//                     }}
//                     hasNewRecordButton={true}
//                     newRecordButtonOnClick={() => {
//                         setModalMode("Add");
//                         setIsModalOpen(true);
//                     }}
//                     customElementOfActions={customColumnSurveyDetails}
//                 />
//             </Card>

//             {isModalOpen && (
//                 <>
//                     {modalMode === "Add" || modalMode === "Update" ? (
//                         <SurveyEditForm
//                             selectedItemId={selectedItemId}
//                             modalModeName={modalMode}
//                             onClose={closeModal}
//                             getList={getList}
//                         />
//                     ) : modalMode === "Delete" ? (
//                         <ConfirmDelete
//                             onConfirm={onConfirmDelete}
//                             selectedItemId={selectedItemId}
//                             onClose={closeModal}
//                         />
//                     ) : modalMode === "Questions" ? (
//                         <SurveyQuestionForm
//                             selectedItemId={selectedItemId}
//                             onClose={closeModal}
//                         />
//                     ) : modalMode === "ResultDetails" ? (
//                         <SurveyResultDetails
//                             selectedItemId={selectedItemId}
//                             onClose={closeModal}
//                         />
//                     ) : null}
//                 </>
//             )}
//         </>
//     );
// };

// export default SurveySetting;
