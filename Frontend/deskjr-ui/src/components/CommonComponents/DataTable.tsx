import { useEffect, useState } from "react";
import Button from "./Button";

interface DataTableProps {
  items: any[];
  tableClassName?: string;
  onEdit?: (item: any) => void;
  onDelete?: (item: any) => void;
  isEditable?: (item: any) => boolean;
  isDeletable?: (item: any) => boolean;
  hiddenColumns?: string[];
  renderColumn?: (column: string, value: any) => JSX.Element | string;
  dataTarget?: string;
  columnNames?: { [key: string]: string };
  hideActions?: string;
  customElementOfActions?: (item: any) => JSX.Element;
  customColumn?: (item: any) => JSX.Element;
  isCustomColumnExist?: string;
}

function DataTable({
  items,
  tableClassName,
  onEdit,
  onDelete,
  isEditable,
  isDeletable,
  hiddenColumns = [],
  renderColumn,
  dataTarget,
  columnNames = {},
  hideActions = "false",
  customElementOfActions,
  customColumn,
  isCustomColumnExist = "false"

}: DataTableProps) {
  const [records, setRecords] = useState<any>([]);
  const [columns, setColumns] = useState<string[]>([]);

  useEffect(() => {
    const firstItem = items.length > 0 ? items[0] : {};
    const newColumns = Object.keys(firstItem).filter(
      (column) => !hiddenColumns.includes(column)
    );
    setColumns([...newColumns]);

    if (hideActions && hideActions === "false" && isCustomColumnExist && isCustomColumnExist === "false") {
      setColumns([...newColumns, "Actions"]);
    }
    else if (hideActions && hideActions === "false" && isCustomColumnExist && isCustomColumnExist === "true") {
      setColumns([...newColumns, customColumn && columnNames["customColumnName"], "Actions"]);
    }
    else if (hideActions && hideActions === "true" && isCustomColumnExist && isCustomColumnExist === "true") {
      setColumns([...newColumns, customColumn && columnNames["customColumnName"]]);
    }
    else {
      setColumns([...newColumns])
    }



    const newRecords = items.map((record: any, index: any) => (
      <tr key={index}>
        {newColumns.map((column) => (
          <td className="text-center align-middle" key={column}>
            {renderColumn
              ? renderColumn(column, record[column])
              : record[column]}
          </td>
        ))}
        {customColumn && <td>{customColumn(record)}</td>}
        {hideActions && hideActions.toString() === "false" && (
          <td className="text-center align-top">
            {isEditable && isEditable(record) && onEdit && (
              <Button
                text="Edit"
                className={"btn btn-warning mr-2"}
                onClick={() => onEdit(record)}
                isModalTrigger={true}
                dataTarget={dataTarget}
              ></Button>
            )}
            {isDeletable && isDeletable(record) && onDelete && (
              <Button
                text="Delete"
                className={"btn btn-danger"}
                onClick={() => onDelete(record)}
                isModalTrigger={true}
                dataTarget={"delete-confirm"}
              ></Button>
            )}
            {customElementOfActions && customElementOfActions(record)}
          </td>
        )}
      </tr>
    ));

    if (newRecords && newRecords.length > 0) {
      setRecords([...newRecords]);
    } else {
      setRecords([]);
      setColumns([]);
    }
  }, [
    items,
    onEdit,
    onDelete,
    isEditable,
    isDeletable,
    hiddenColumns,
    renderColumn,
  ]);

  return (
    <div>
      <table
        className={
          tableClassName ? tableClassName : "table table-bordered table-hover"
        }
      >
        <thead>
          <tr>
            {columns.map(
              (column) =>
                !hiddenColumns.includes(column) && (
                  <th
                    key={column}
                    className="text-center align-middle bg-light"
                  >
                    <div>{columnNames[column] || column}</div>
                  </th>
                )
            )}
          </tr>
        </thead>
        <tbody>
          {records && records.length > 0 ? (
            records
          ) : (
            <tr>
              <td className="text-center" colSpan={columns.length}>
                No Records Found
              </td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
}

export default DataTable;
