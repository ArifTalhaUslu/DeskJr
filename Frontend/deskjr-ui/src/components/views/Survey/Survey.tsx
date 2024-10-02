import { useEffect, useState } from "react";
import surveyService from "../../../services/SurveyService";
import { showErrorToast } from "../../../utils/toastHelper";
import Button from "../../CommonComponents/Button";
import Card from "../../CommonComponents/Card";
import Board from "../../CommonComponents/Board";
import SurveyForm from "./SurveyForm";
import employeeAnswersService from "../../../services/EmployeeAnswersService";
import { formatDate } from "date-fns/format";

const Survey = (props: any) => {
    const [items, setItems] = useState([]);
    const [selectedItemId, setSelectedItemId] = useState("");
    const [selectedSurvey, setSelectedSurvey] = useState("");
    const [modalModeName, setModalModeName] = useState("");
    const [status, setStatus] = useState(false);
    const [isTrigger, setIsTrigger] = useState(false);
    const [modalDataTarget] = useState("survey-parcipation");
    const [formToBeClosed, setFormToBeClosed] = useState("");

    useEffect(() => {
        getList();
    }, [isTrigger]);

    const checked = async (userId: any, surveyId: any) => {
        try {
            const data = await employeeAnswersService.getEmployeeSurveyStatus(userId, surveyId);
            setStatus(data);
        } catch (err) {
            showErrorToast(err);
        }
    };

    const getList = async () => {
        try {
            const data = await surveyService.getAllSurvey();
            setItems(data);
        } catch (err) {
            showErrorToast(err);
        }
    };

    const customColumn = (item: any) => {
        const currentDate = new Date();
        const surveyEndDate = new Date(item.endDate);

        if (surveyEndDate < currentDate) {
            return null;
        }

        return (
            <div className="text-center">
                <Button
                    text="Join Survey"
                    className="btn btn-info m-1 p-2"
                    onClick={async () => {
                        await checked(props.currentUser?.id, item.id);
                        setSelectedItemId(item.id);
                        setModalModeName("Survey Participation");
                        setFormToBeClosed("form-close-survey");
                    }}
                    isModalTrigger={true}
                    dataTarget={modalDataTarget}
                />
            </div>
        );
    };

    const onModalClose = () => {
        setSelectedItemId("");
        setSelectedSurvey("");
        setModalModeName("");
        setIsTrigger(false);
        const close_button = document.getElementById(formToBeClosed);
        close_button?.click();
        setFormToBeClosed("");
    };

    const renderColumn = (column: string, value: any) => {
        if (column === "endDate") {
            return formatDate(new Date(value), "dd/MM/yyyy");
        }
        return value;
    };

    return (
        <>
            <Card title={"Surveys"}>
                <Board
                    items={items}
                    hiddenColumns={["id", "surveyQuestions"]}
                    renderColumn={renderColumn}
                    customColumn={customColumn}
                    hideActions="true"
                    isCustomColumnExist="true"
                    columnNames={{
                        name: "Survey Name",
                        endDate: "End Date"
                    }}
                />
            </Card>

            <SurveyForm
                selectedItemId={selectedItemId}
                modalModeName={modalModeName}
                onClose={onModalClose}
                currentUser={props.currentUser}
                status={status}
            />
        </>
    );
};

export default Survey;
