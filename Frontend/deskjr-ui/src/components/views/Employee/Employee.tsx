import { useEffect, useState } from "react";
import employeeService from "../../../services/EmployeeService";
import Card from "../../CommonComponents/Card";
import Board from "../../CommonComponents/Board";
import EmployeeEditForm from "./EmployeeEditForm";
import { formatDate } from "date-fns";
import ConfirmDelete from "../../CommonComponents/ConfirmDelete";

const Employee: any = () => {
  const [items, setItems] = useState([]);
  const [selectedItemId, setSelectedItemId] = useState("");
  const [selectedEmployee, setSelectedEmployee] = useState("");
  const [modalModeName, setModalModeName] = useState("");
  const [modalDataTarget] = useState("employeeAddModal");

  const [isTrigger, setIsTrigger] = useState(false);
  const [isDelete, setIsDelete] = useState(false);
  const [isEdit, setIsEdit] = useState(false);
  const [formToBeClosed, setFormToBeClosed] = useState("");

  useEffect(() => {
    getList();
  }, [isTrigger]);


  const getList = async () => {
    employeeService.getAllEmployee().then((data) => {
      setItems(data);
    });
  };

  const onConfirmDelete = async (e: any) => {
    e.preventDefault();

    if (selectedItemId) {
      employeeService
        .deleteEmployee(selectedItemId)
        .then(() => {
          alert("Success");
          setIsTrigger(true);
        })
        .catch((err: any) => {
          console.log(err);
        });
    }

    // const close_button = document.getElementById("confirm-delete-close");
    // close_button?.click();
    onModalClose();
  };

  const handleEdit = (employee: any) => {
    setSelectedItemId(employee.id);
    setModalModeName("Update");
    setIsEdit(true);
    setFormToBeClosed("form-close");
  };

  const handleDelete = (employee: any) => {
    setSelectedItemId(employee.id);
    setIsDelete(true);
    setFormToBeClosed("delete-form-closed");
  };

  const isEditable = (item: any) => true;

  const isDeletable = (item: any) => true;

  const renderColumn = (column: string, value: any) => {
    if (column === "employeeRole") {
      return value === 2
        ? "Employee"
        : value === 1
        ? "Manager"
        : value === 0
        ? "Admin"
        : value;
    } else if (column === "gender") {
      return value === 0 ? "Male" : value === 1 ? "Female" : value;
    } else if (column === "dayOfBirth") {
      return formatDate(new Date(value), "dd/MM/yyyy");
    }
    return value;
  };

  const onModalClose = () => {
    setSelectedItemId("");
    setSelectedEmployee("");
    setModalModeName("");
    setIsTrigger(false);
    const close_button = document.getElementById(formToBeClosed);
    close_button?.click();
    setFormToBeClosed("");
    //window.location.reload(); //gecici cozum
  };

  const columnNames = {
    name: "Employee Name",
    dayOfBirth: "BirthDay",
    employeeRole: "Employee Role",
    gender: "Gender",
    titleId: "Title Name",
    teamId: "Team Name",
    email: "E-mail",
  };

  return (
    <>
      <Card title={"Employee List"}>
        <Board
          items={items}
          onEdit={handleEdit}
          onDelete={handleDelete}
          isEditable={isEditable}
          isDeletable={isDeletable}
          hiddenColumns={["id","password"]}
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

      <EmployeeEditForm
        selectedItemId={selectedItemId}
        modalModeName={modalModeName}
        selectedEmployee={selectedEmployee}
        setSelectedEmployee={setSelectedEmployee}
        getList={getList}
        onClose={onModalClose}
      />
      <ConfirmDelete
        onConfirm={(e) => onConfirmDelete(e)}
        selectedItemId={selectedItemId}
        onClose={onModalClose}
        setShouldRefresh={setShouldRefresh}
      />
      <ConfirmDelete
        onConfirm={(e) => onConfirmDelete(e)}
        selectedItemId={selectedItemId}
        onClose={onModalClose}
      />
    </>
  );
};

export default Employee;
