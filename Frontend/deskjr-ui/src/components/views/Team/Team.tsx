import { useEffect, useState } from "react";
import teamService from "../../../services/TeamService";
import Card from "../../CommonComponents/Card";
import Board from "../../CommonComponents/Board";
import TeamEditForm from "./TeamEditForm";
import ConfirmDelete from "../../CommonComponents/ConfirmDelete";

const Team: any = () => {
  const [items, setItems] = useState([]);
  const [selectedItemId, setSelectedItemId] = useState("");
  const [selectedTeam, setSelectedTeam] = useState("");
  const [modalModeName, setModalModeName] = useState("");
  const [modalDataTarget] = useState("teamAddModal");
  const [isTrigger, setIsTrigger] = useState(false);
  const [formToBeClosed, setFormToBeClosed] = useState("");

  useEffect(() => {
    getList();
  }, [isTrigger]);

  const getList = async () => {
    teamService.getAllTeam().then((data) => {
      setItems(data);
    });
  };

  const onConfirmDelete = async (e: any) => {
    e.preventDefault();

    if (selectedItemId) {
      teamService
        .deleteTeam(selectedItemId)
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

  const handleEdit = (team: any) => {
    setSelectedItemId(team.id);
    setModalModeName("Update");
    //setIsEdit(true);
    setFormToBeClosed("form-close");
  };

  const handleDelete = (team: any) => {
    setSelectedItemId(team.id);
    //setIsDelete(true);
    setFormToBeClosed("delete-form-closed");
  };

  const isEditable = (item: any) => true;

  const isDeletable = (item: any) => true;

  const onModalClose = () => {
    setSelectedItemId("");
    setSelectedTeam("");
    setModalModeName("");
    setIsTrigger(false);
    const close_button = document.getElementById(formToBeClosed);
    close_button?.click();
    setFormToBeClosed("");
  };
  
  const renderColumn = (column: string, value: any) => {
    if (column === "manager") {
      return value && value.name;
    } 
    return value;
  };

  const columnNames = {
    name: "Team Name",
    manager: "Manager Name",
  };

  return (
    <>
      <Card title={"Team List"}>
        <Board
          items={items}
          onEdit={handleEdit}
          onDelete={handleDelete}
          isEditable={isEditable}
          isDeletable={isDeletable}
          hiddenColumns={["id","managerId"]}
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

      <TeamEditForm
        selectedItemId={selectedItemId}
        modalModeName={modalModeName}
        selectedTeam={selectedTeam}
        setSelectedTeam={setSelectedTeam}
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

export default Team;
