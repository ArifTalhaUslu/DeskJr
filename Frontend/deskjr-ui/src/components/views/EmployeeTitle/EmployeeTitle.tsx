import { useEffect, useState } from "react";
import employeeTitleService from "../../../services/EmployeeTitleService";
import Card from "../../CommonComponents/Card";
import Board from "../../CommonComponents/Board";
import EmployeeTitleEditForm from "./EmployeeTitleEditForm";
import ConfirmDelete from "../../CommonComponents/ConfirmDelete";

const EmployeeTitle: any = () => {
  const [items, setItems] = useState([]);
  const [selectedItemId, setSelectedItemId] = useState("");
  const [selectedEmployeeTitle, setSelectedEmployeeTitle] = useState("");
  const [modalModeName, setModalModeName] = useState("");
  const [modalDataTarget] = useState("employeeTitleAddModal");
  const [isTrigger, setIsTrigger] = useState(false);
  const [isDelete, setIsDelete] = useState(false);
  const [isEdit, setIsEdit] = useState(false);
  const [formToBeClosed, setFormToBeClosed] = useState("");

  useEffect(() => {
    getList();
  }, [isTrigger]);

  const getList = async () => {
    employeeTitleService.getAllEmployeeTitle().then((data) => {
      setItems(data);
    });
  };

  const onConfirmDelete = async (e: any) => {
    e.preventDefault();

    if (selectedItemId) {
      employeeTitleService
        .deleteEmployeeTitle(selectedItemId)
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

  const handleEdit = (employeeTitle: any) => {
    setSelectedItemId(employeeTitle.id);
    setModalModeName("Update");
    setIsEdit(true);
    setFormToBeClosed("form-close");
  };

  const handleDelete = (employeeTitle: any) => {
    setSelectedItemId(employeeTitle.id);
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
    setSelectedEmployeeTitle("");
    setModalModeName("");
    setIsTrigger(false);
    const close_button = document.getElementById(formToBeClosed);
    close_button?.click();
    setFormToBeClosed("");
  };

  const columnNames = {
    titleName: "Title Name",
  };

  return (
    <>
      <Card title={"Employee Title List"}>
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

      <EmployeeTitleEditForm
        selectedItemId={selectedItemId}
        modalModeName={modalModeName}
        selectedEmployeeTitle={selectedEmployeeTitle}
        setSelectedEmployeeTitle={setSelectedEmployeeTitle}
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

export default EmployeeTitle;
