import { useEffect, useState } from "react";
import Board from "../../CommonComponents/Board";
import Card from "../../CommonComponents/Card";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import leaveService from "../../../services/LeaveService";
import Button from "../../CommonComponents/Button";
import { formatDate } from "date-fns";
import { status } from "../../../types/status";
import StatusIcon from "../../CommonComponents/StatusIcons/StatusIcon";

const PendingLeaveRequest: any = (props: any) => {
  const [pendingLeaves, setPendingLeaves] = useState([]);
  const [isTrigger, setIsTrigger] = useState(false);

  useEffect(() => {
    getList();
  }, [isTrigger]);

  const getList = async () => {
    await leaveService
      .getPendingLeavesForApproverEmployeeByEmployeeId(props.currentUser?.id, props.currentUser?.employeeRole)
      .then((data) => {
        setPendingLeaves(data);
      })
      .catch((err) => {
        showErrorToast(err);
      });
  }

  const Approve = (leave: any) => {
    leaveService.updateLeaveStatus(leave.id, status.Approved, props.currentUser?.id)
      .then(() => {
        showSuccessToast('Successfully Confirmed!');
        setIsTrigger(!isTrigger);
      }).catch((err) => {
        showErrorToast(err);
      });
  }

  const Deny = (leave: any) => {
    leaveService.updateLeaveStatus(leave.id, status.Cancelled, props.currentUser?.id)
      .then(() => {
        showSuccessToast('Successfully Rejected!');
        setIsTrigger(!isTrigger);
      }).catch((err) => {
        showErrorToast(err);
      });
  }

  const customElementOfActions = (item: any) => (
    <>
      <div className="container">
        <div className="row">
          <Button text="Approve" className="btn btn-success m-1 p-2 w-100" onClick={() => Approve(item)} />
          <Button text="Deny" className="btn btn-danger m-1 p-2 w-100" onClick={() => Deny(item)} />
        </div>
      </div>
    </>
  )


  const renderColumn = (column: string, value: any) => {
    if (column === "statusOfLeave") {
      return value || value == 0 ? <StatusIcon status={value} /> : value;
    } else if (column === "requestingEmployee") {
      return value && value.name;
    } else if (column === "startDate") {
      return formatDate(new Date(value), "dd/MM/yyyy");
    } else if (column === "endDate") {
      return formatDate(new Date(value), "dd/MM/yyyy");
    } else if (column === "leaveType") {
      return value && value.name;
    }
    return value;
  };

  const columnNames = {
    requestingEmployee: "Requesting Employee",
    startDate: "Start Date",
    endDate: "End Date",
    leaveType: "Leave Type",
    requestComments: "Request Comments",
    statusOfLeave: "Leave Status",
  };

  return (
    <>
      <Card title={"Pending Leave Request"}>
        <Board
          items={pendingLeaves}
          isEditable={() => { return false }}
          isDeletable={() => { return false }}
          hiddenColumns={["id", "requestingEmployeeId", "leaveTypeId", "approvedBy", "approvedById"]}
          renderColumn={renderColumn}
          columnNames={columnNames}
          customElementOfActions={customElementOfActions}
        />

      </Card>
    </>
  );
};

export default PendingLeaveRequest;
