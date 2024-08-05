import { useEffect, useState } from "react";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import leaveService from "../../../services/LeaveService";
import Card from "../../CommonComponents/Card";
import Board from "../../CommonComponents/Board";
import ConfirmDelete from "../../CommonComponents/ConfirmDelete";
import Cookies from "js-cookie";
import LeaveEditForm from "./LeaveEditForm";
import { formatDate } from "date-fns";
import { statusOfLeave } from "../../../types/statusOfLeave";

const Leave: any = (props: any) => {
  const id = Cookies.get("id");
  
  const [items, setItems] = useState([]);
  const [requestingEmployeeId] = useState(id);
  const [selectedItemId, setSelectedItemId] = useState("");
  const [selectedLeave, setSelectedLeave] = useState("");
  const [modalModeName, setModalModeName] = useState("");
  const [modalDataTarget] = useState("leaveAddModal");
  const [isTrigger, setIsTrigger] = useState(false);
  const [formToBeClosed, setFormToBeClosed] = useState("");
  

  useEffect(() => {
    getList();
  }, [isTrigger]);

  const getList = async () => {
    leaveService.getLeavesByEmployeeId(id).then((data) => {
      setItems(data);
    })
      .catch((err) => {
        showErrorToast(err);
      });
  };

  const onConfirmDelete = async (e: any) => {
    e.preventDefault();

    if (selectedItemId) {
      leaveService
        .deleteLeave(selectedItemId)
        .then(() => {
          showSuccessToast('Successful!');
          setIsTrigger(true);
        })
        .catch((err) => {
          showErrorToast(err);
        });
    }
    onModalClose();
  };

  const handleEdit = (leave: any) => {
    setSelectedItemId(leave.id);
    setModalModeName("Update");
    setFormToBeClosed("form-close");
  };

  const handleDelete = (leave: any) => {
    setSelectedItemId(leave.id);
    setFormToBeClosed("delete-form-closed");
  };

  const isEditable = (item: any) => true;

  const isDeletable = (item: any) => true;

  const onModalClose = () => {
    setSelectedItemId("");
    setSelectedLeave("");
    setModalModeName("");
    setIsTrigger(false);
    const close_button = document.getElementById(formToBeClosed);
    close_button?.click();
    setFormToBeClosed("");
  };

  const renderColumn = (column: string, value: any) => {
    if (column === "statusOfLeave") {
      return value === statusOfLeave.Pending
        ? "Pending"
        : value === statusOfLeave.Approved
        ? "Approved"
        : value === statusOfLeave.Cancelled
        ? "Cancelled"
        : value;
    } else if (column === "startDate") {
      return formatDate(new Date(value), "dd/MM/yyyy");
    } else if (column === "endDate") {
      return formatDate(new Date(value), "dd/MM/yyyy");
    }
    
    return value;
  };

  const columnNames = {

  };

  return (
    <>
      <Card title={"Leave List"}>
        <Board
          items={items}
          onEdit={handleEdit}
          onDelete={handleDelete}
          isEditable={isEditable}
          isDeletable={isDeletable}
          hiddenColumns={["id"]}
          renderColumn={renderColumn}
          columnNames={columnNames}
          hasNewRecordButton={true}
          newRecordButtonOnClick={() => {
            setModalModeName("Add");
            setFormToBeClosed("form-close");
            setIsTrigger(true);
          }}
          newRecordModalDataTarget={modalDataTarget}
        />
      </Card>

      <LeaveEditForm
        selectedItemId={selectedItemId}
        modalModeName={modalModeName}
        selectedLeave={selectedLeave}
        setSelectedLeave={setSelectedLeave}
        getList={getList}
        onClose={onModalClose}
      />

      <ConfirmDelete
        onConfirm={(e) => onConfirmDelete(e)}
        selectedItemId={selectedItemId}
        onClose={onModalClose}
      />
    </>
  );
};

export default Leave;
