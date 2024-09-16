import { useEffect, useState } from "react";
import employeeService from "../../../services/EmployeeService";
import Card from "../../CommonComponents/Card";
import Board from "../../CommonComponents/Board";
import EmployeeEditForm from "./EmployeeEditForm";
import { formatDate } from "date-fns";
import ConfirmDelete from "../../CommonComponents/ConfirmDelete";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import { Roles } from "../../../types/Roles";
import ImageUpload from "../../CommonComponents/ImageUpload";

const Employee: any = (props: any) => {
  const [items, setItems] = useState([]);
  const [selectedItemId, setSelectedItemId] = useState("");
  const [selectedEmployee, setSelectedEmployee] = useState("");
  const [modalModeName, setModalModeName] = useState("");
  const [modalDataTarget] = useState("employeeAddModal");
  const [isTrigger, setIsTrigger] = useState(false);
  const [formToBeClosed, setFormToBeClosed] = useState("");
  const [imageBase64, setImageBase64] = useState<string | null>(null);
  useEffect(() => {
    getList();
  }, [isTrigger]);

  const getList = async () => {
    employeeService
      .getAllEmployee()
      .then((data) => {
        setItems(data);
      })
      .catch((err) => {
        showErrorToast(err);
      });
  };

  const onConfirmDelete = async (e: any) => {
    e.preventDefault();

    if (selectedItemId) {
      employeeService
        .deleteEmployee(selectedItemId)
        .then(() => {
          showSuccessToast("Successful!");
          setIsTrigger(true);
        })
        .catch((err) => {
          showErrorToast(err);
        });
    }
    onModalClose();
  };

  const handleEdit = (employee: any) => {
    setSelectedItemId(employee.id);
    setModalModeName("Update");
    setFormToBeClosed("form-close");
  };

  const handleDelete = (employee: any) => {
    setSelectedItemId(employee.id);
    setFormToBeClosed("delete-form-closed");
  };
  const handleImageUpload = (imageBase64: string) => {
    setImageBase64(imageBase64);
    props.setSelectedEmployee((prev: any) => ({
      ...prev,
      base64Image: imageBase64,
    }));
  };

  const isEditable = (item: any) => true;

  const isDeletable = (item: any) =>
    props.currentUser.employeeRole === Roles.Admin;

  const renderColumn = (column: string, value: any) => {
    if (column === "employeeRole") {
      return value === Roles.Employee
        ? "Employee"
        : value === Roles.Manager
        ? "Manager"
        : value === Roles.Admin
        ? "Admin"
        : value;
    } else if (column === "gender") {
      return value === 0 ? "Male" : value === 1 ? "Female" : value;
    } else if (column === "dayOfBirth") {
      return formatDate(new Date(value), "dd/MM/yyyy");
    } else if (column === "employeeTitle") {
      return value && value.titleName;
    } else if (column === "team") {
      return value && value.name;
    } else if (column === "base64Image") {
      return (
        <img
          src={value}
          alt="Profile"
          style={{ width: "50px", height: "50px", borderRadius: "50%" }}
        />
      );
    }

    return value;
  };

  const onModalClose = () => {
    setSelectedItemId("");
    setSelectedEmployee("");
    setModalModeName("");
    setImageBase64(null);
    setIsTrigger(false);
    const close_button = document.getElementById(formToBeClosed);
    close_button?.click();
    setFormToBeClosed("");
  };

  const columnNames = {
    name: "Employee Name",
    dayOfBirth: "BirthDay",
    employeeRole: "Employee Role",
    gender: "Gender",
    employeeTitle: "Title Name",
    team: "Team Name",
    email: "E-mail",
    base64Image: "Profile Picture",
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
          hiddenColumns={["id", "password", "teamId", "employeeTitleId"]}
          renderColumn={renderColumn}
          columnNames={columnNames}
          hasNewRecordButton={props.currentUser.employeeRole === Roles.Admin}
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
        currentUser={props.currentUser}
        handleImageUpload={handleImageUpload}
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
