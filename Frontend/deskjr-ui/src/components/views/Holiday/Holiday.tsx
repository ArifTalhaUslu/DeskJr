import { useEffect, useState } from "react";
import holidayService from "../../../services/HolidayService";
import Card from "../../CommonComponents/Card";
import Board from "../../CommonComponents/Board";
import { formatDate } from "date-fns";
import ConfirmDelete from "../../CommonComponents/ConfirmDelete";
import HolidayEditForm from "./HolidayEditForm";

const Holiday: any = () => {
  const [items, setItems] = useState([]);
  const [selectedItemId, setSelectedItemId] = useState("");
  const [selectedHoliday, setSelectedHoliday] = useState("");
  const [modalModeName, setModalModeName] = useState("");
  const [modalDataTarget] = useState("holidayAddModal");
  const [isTrigger, setIsTrigger] = useState(false);
  const [formToBeClosed, setFormToBeClosed] = useState("");

  useEffect(() => {
    getList();
  }, [isTrigger]);

  const getList = async () => {
    holidayService.getAllHoliday().then((data) => {
      setItems(data);
    });
  };

  const onConfirmDelete = async (e: any) => {
    e.preventDefault();

    if (selectedItemId) {
      holidayService
        .deleteHoliday(selectedItemId)
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

  const handleEdit = (holiday: any) => {
    setSelectedItemId(holiday.id);
    setModalModeName("Update");
    setFormToBeClosed("form-close");
  };

  const handleDelete = (holiday: any) => {
    setSelectedItemId(holiday.id);
    setFormToBeClosed("delete-form-closed");
  };

  const isEditable = (item: any) => true;

  const isDeletable = (item: any) => true;

  const renderColumn = (column: string, value: any) => {
    if (column === "startDate") {
      return formatDate(new Date(value), "dd/MM/yyyy");
    } else if (column === "endDate") {
      return formatDate(new Date(value), "dd/MM/yyyy");
    }
    return value;
  };

  const onModalClose = () => {
    setSelectedItemId("");
    setSelectedHoliday("");
    setModalModeName("");
    setIsTrigger(false);
    const close_button = document.getElementById(formToBeClosed);
    close_button?.click();
    setFormToBeClosed("");
    //window.location.reload(); //gecici cozum
  };

  const columnNames = {
    name: "Holiday Name",
    startDate: "Start Date",
    endDate: "End Date",
  };

  return (
    <>
      <Card title={"Holidays"}>
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

      <HolidayEditForm
        selectedItemId={selectedItemId}
        modalModeName={modalModeName}
        selectedHoliday={selectedHoliday}
        setSelectedHoliday={setSelectedHoliday}
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

export default Holiday;
