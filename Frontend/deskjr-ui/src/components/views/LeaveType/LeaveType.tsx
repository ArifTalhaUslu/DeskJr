import { useEffect, useState } from "react";
import leaveTypeService from "../../../services/LeaveTypeService";
import Card from "../../CommonComponents/Card";
import Board from "../../CommonComponents/Board";
import LeaveTypeEditForm from "./LeaveTypeEditForm";
import ConfirmDelete from "../../CommonComponents/ConfirmDelete";

const LeaveType: any = () => {
  const [items, setItems] = useState([]);
  const [selectedItemId, setSelectedItemId] = useState("");
  const [selectedLeaveType, setSelectedLeaveType] = useState("");
  const [modalModeName, setModalModeName] = useState("");
  const [modalDataTarget] = useState("leaveTypeAddModal");
  const [isTrigger, setIsTrigger] = useState(false);
  const [isDelete, setIsDelete] = useState(false);
  const [isEdit, setIsEdit] = useState(false);
  const [formToBeClosed, setFormToBeClosed] = useState("");

  useEffect(() => {
    getList();
  }, [isTrigger]);

  const getList = async () => {
    leaveTypeService.getAllLeaveType().then((data) => {
      setItems(data);
    });
  };

  const onConfirmDelete = async (e: any) => {
    e.preventDefault();

    if (selectedItemId) {
      leaveTypeService
        .deleteLeaveType(selectedItemId)
        .then(() => {
          alert("Success");
          setIsTrigger(true);
        })
        .catch((err: any) => {
          console.log(err);
        });
    }

    onModalClose();
  };

  const handleEdit = (leaveType: any) => {
    setSelectedItemId(leaveType.id);
    setModalModeName("Update");
    setIsEdit(true);
    setFormToBeClosed("form-close");
  };

  const handleDelete = (leaveType: any) => {
    setSelectedItemId(leaveType.id);
    setIsDelete(true);
    setFormToBeClosed("delete-form-closed");
  };

  const isEditable = (item: any) => true;

  const isDeletable = (item: any) => true;

  const renderColumn = (column: string, value: any) => {
    return value;
  };

  const onModalClose = () => {
    setSelectedItemId("");
    setSelectedLeaveType("");
    setModalModeName("");
    setIsTrigger(false);
    const close_button = document.getElementById(formToBeClosed);
    close_button?.click();
    setFormToBeClosed("");
  };

  const columnNames = {
    name: "Leave Type Name",
  };

  return (
    <>
      <Card title={"Leave Type List"}>
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

      <LeaveTypeEditForm
        selectedItemId={selectedItemId}
        modalModeName={modalModeName}
        selectedLeaveType={selectedLeaveType}
        setSelectedLeaveType={setSelectedLeaveType}
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

export default LeaveType;
