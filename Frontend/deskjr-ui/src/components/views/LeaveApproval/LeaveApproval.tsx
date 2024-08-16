import { useEffect, useState } from "react";
import Board from "../../CommonComponents/Board";
import Card from "../../CommonComponents/Card";
import { showErrorToast, showSuccessToast } from "../../../utils/toastHelper";
import leaveService from "../../../services/LeaveService";
import Button from "../../CommonComponents/Button";
import { formatDate } from "date-fns";
import { status } from "../../../types/status";
import StatusIcon from "../../CommonComponents/StatusIcons/StatusIcon";

const LeaveApproval: any = (props: any) => {
  const [leaves, setLeaves] = useState([]);
  const [isTrigger, setIsTrigger] = useState(false);
  const [showPendings, setShowPendings] = useState(false);

  useEffect(() => {
    getPendingLeavesWithInclude();
  }, [isTrigger]);

  const getPendingLeavesWithInclude = async () => {
    await leaveService
      .getPendingLeavesForApproverEmployeeByEmployeeId(
        props.currentUser?.id,
        props.currentUser?.employeeRole
      )
      .then((data) => {
        setLeaves(data);
      })
      .catch((err) => {
        showErrorToast(err);
      });
  };

  const Approve = (leave: any) => {
    leaveService
      .updateLeaveStatus(leave.id, status.Approved, props.currentUser?.id)
      .then(() => {
        showSuccessToast("Successfully Confirmed!");
        setIsTrigger(!isTrigger);
      })
      .catch((err) => {
        showErrorToast(err);
      });
  };

  const Deny = (leave: any) => {
    leaveService
      .updateLeaveStatus(leave.id, status.Cancelled, props.currentUser?.id)
      .then(() => {
        showSuccessToast("Successfully Rejected!");
        setIsTrigger(!isTrigger);
      })
      .catch((err) => {
        showErrorToast(err);
      });
  };

  const customElementOfActions = (item: any) => (
    <>
      {item.statusOfLeave === status.Pending && (
        <div className="container">
          <div className="row">
            <Button
              text="Approve"
              className="btn btn-success m-1 p-2 w-100"
              onClick={() => Approve(item)}
            />
            <Button
              text="Deny"
              className="btn btn-danger m-1 p-2 w-100"
              onClick={() => Deny(item)}
            />
          </div>
        </div>
      )}
    </>
  );

  const getLeaves = async () => {
    await leaveService
      .getAllLeavesByManagerId(props.currentUser?.id)
      .then((data) => {
        setLeaves(data);
      })
      .catch((err) => {
        showErrorToast(err);
      });
  };

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

  const togglePendings = () => {
    setShowPendings(!showPendings);
  };

  const columnNames = {
    requestingEmployee: "Requesting Employee",
    startDate: "Start Date",
    endDate: "End Date",
    leaveType: "Leave Type",
    requestComments: "Request Comments",
    statusOfLeave: "Leave Status",
  };

  const topRightContent = (
    <>
      <Button
        text={showPendings ? "Show Pendings" : "Show All"}
        className={
          showPendings ? "btn btn-warning m-1 p-2" : "btn btn-success m-1 p-2"
        }
        onClick={() => {
          if (showPendings) {
            getPendingLeavesWithInclude();
          } else {
            getLeaves();
          }
          togglePendings();
        }}
      />
    </>
  );

  return (
    <>
      <Card title={"Leave Approval"}>
        <Board
          items={leaves}
          isEditable={() => {
            return false;
          }}
          isDeletable={() => {
            return false;
          }}
          hiddenColumns={[
            "id",
            "requestingEmployeeId",
            "leaveTypeId",
            "approvedBy",
            "approvedById",
          ]}
          renderColumn={renderColumn}
          columnNames={columnNames}
          customElementOfActions={customElementOfActions}
          rightTopContent={topRightContent}
        />
      </Card>
    </>
  );
};

export default LeaveApproval;
