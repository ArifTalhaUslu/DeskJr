import { useEffect, useState } from "react";
import teamService from "../../../services/TeamService";
import Card from "../../CommonComponents/Card";
import Board from "../../CommonComponents/Board";
import TeamEditForm from "./TeamEditForm";
import ConfirmDelete from "../../CommonComponents/ConfirmDelete";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";

const Team: any = () => {
  const [items, setItems] = useState([]);
  const [selectedItemId, setSelectedItemId] = useState("");
  const [selectedTeam, setSelectedTeam] = useState("");
  const [modalModeName, setModalModeName] = useState("");
  const [modalDataTarget] = useState("teamAddModal");
  const [isTrigger, setIsTrigger] = useState(false);
  const [formToBeClosed, setFormToBeClosed] = useState("");
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);

  useEffect(() => {
    getList();
  }, [isTrigger]);

  const getList = async () => {
    teamService.getAllTeam().then((data) => {
      setItems(data);
    })
      .catch((err) => {
        showErrorToast(err);
      });
  };

  const onConfirmDelete = async (e: any) => {
    e.preventDefault();

    if (selectedItemId) {
      teamService
        .deleteTeam(selectedItemId)
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

  const handleEdit = (team: any) => {
    setSelectedItemId(team.id);
    setSelectedTeam(team);
    setModalModeName("Update");
    setFormToBeClosed("form-close");
  };

  const handleDelete = (team: any) => {
    setSelectedItemId(team.id);
    setFormToBeClosed("delete-form-closed");
    setIsDeleteModalOpen(true);
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
    setIsDeleteModalOpen(false);
  };

  const renderColumn = (column: string, value: any) => {
    if (column === "manager") {
      return value && value.name;
    } else if (column === "upTeamId") {
      if (value) {
        const upTeam = items.find((team) => team.id === value);
        console.log("UpTeam:", upTeam);
        return upTeam ? upTeam.name : "No Up Team";
      }
      return "No Up Team";
    }
    return value;
  };

  const columnNames = {
    name: "Team Name",
    manager: "Manager Name",
    upTeamId: "Up Team",
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
          hiddenColumns={["id", "managerId"]}
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

      {isDeleteModalOpen && (
        <ConfirmDelete
          onConfirm={(e) => onConfirmDelete(e)}
          selectedItemId={selectedItemId}
          onClose={onModalClose}
        />
      )}
    </>
  );
};

export default Team;
