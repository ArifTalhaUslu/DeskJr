import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { GetEmployees} from "../../store/actions/employeeActions";
import Card from "../CommonComponents/Card";
import Board from "../CommonComponents/Board";
import { AppState } from "../../store";

const Employee: React.FC = () => {
  const dispatch = useDispatch();
  const { data: employees, loading, error } = useSelector((state: AppState) => state.employee);

  useEffect(() => {
    dispatch(GetEmployees());
  }, [dispatch]);



  const isEditable = (employee: any) => {
    return true; // Add your custom logic for editable condition
  };

  const isDeletable = (employee: any) => {
    return true; // Add your custom logic for deletable condition
  };

  const renderColumn = (column: string, value: any) => {
    if (typeof value === "boolean") {
      return value ? <b>Yes</b> : <b>No</b>;
    }
    if (column === "age") {
      return (
        value && (
          <i>
            <b>
              <u>{value}</u>
            </b>
          </i>
        )
      );
    }
    return value;
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <Card title="Employee List">
      <Board
        items={employees}
        isEditable={isEditable}
        isDeletable={isDeletable}
        hiddenColumns={["id"]}
        renderColumn={renderColumn}
        hasNewRecordButton={true}
        newRecordButtonOnClick={() => { /* Add new record logic here */ }}
      />
    </Card>
  );
};

export default Employee;
