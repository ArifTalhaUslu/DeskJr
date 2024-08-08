import { useEffect, useState, CSSProperties } from "react";
import Button from "./Button";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

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
  customElementOfActions?:(item: any) => JSX.Element;
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
  hideActions = 'false',
  customElementOfActions,
}: DataTableProps) {
  const [records, setRecords] = useState<any>([]);
  const [columns, setColumns] = useState<string[]>([]);
  const [searchQueries, setSearchQueries] = useState<{ [key: string]: string }>(
    {}
  );
  const [showSearch, setShowSearch] = useState<{ [key: string]: boolean }>({});

  useEffect(() => {
    const firstItem = items.length > 0 ? items[0] : {};
    const newColumns = Object.keys(firstItem).filter(
      (column) => !hiddenColumns.includes(column)
    );
    setColumns([...newColumns]);

    if (hideActions && hideActions === 'false') {
      setColumns([...newColumns, "Actions"]);
    }

    const filteredItems = items.filter((item) =>
      newColumns.every((column) => {
        const searchQuery = searchQueries[column] || "";
        const value =
          item[column] != null ? item[column].toString().toLowerCase() : "";
        return (
          (!searchQuery || value.includes(searchQuery.toLowerCase()))
        );
      })
    );

    const newRecords = filteredItems.map((record: any, index: any) => (
      <tr key={index}>
        {newColumns.map((column) => (
          <td
            className="text-center"
            key={column}
            style={{
              padding: column === "Actions" ? "10px 20px" : "10px",
              verticalAlign: column === "Actions" ? "top" : "middle",
            }}
          >
            {renderColumn
              ? renderColumn(column, record[column])
              : record[column]}
          </td>
        ))}
        {
          hideActions && hideActions.toString() === 'false' &&
          <td className="text-center" style={{
            padding: "10px 20px",
            verticalAlign: "top",
          }}>
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
        }
      </tr>
    ));
    if (newRecords && newRecords.length > 0) {
      setRecords([...newRecords]);
    }
    else {
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
    searchQueries,
  ]);

  const handleSearchChange = (column: string, value: string) => {
    setSearchQueries((prev) => ({
      ...prev,
      [column]: value,
    }));
  };

  const toggleSearchVisibility = (column: string) => {
    setShowSearch((prev) => ({
      ...prev,
      [column]: !prev[column],
    }));
  };

  // Inline CSS Styles
  const tableStyle: CSSProperties = {
    width: "100%",
    borderCollapse: "collapse",
  };

  const headerStyle: CSSProperties = {
    textAlign: "center",
    padding: "10px",
    backgroundColor: "#f2f2f2",
    borderBottom: "2px solid #ddd",
  };

  const headerCellStyle: CSSProperties = {
    textAlign: "center",
    padding: "15px",
    verticalAlign: "top", // Ensure vertical alignment of the header cell
  };

  // Boşluk eklemek için stil
  const filterContainerStyle: CSSProperties = {
    display: "flex",
    flexDirection: "column",
    alignItems: "flex-start",
  };

  const filterWrapperStyle: CSSProperties = {
    display: "flex",
    alignItems: "center",
    gap: "10px",
  };

  const searchButtonStyle: CSSProperties = {
    marginLeft: "10px",
    cursor: "pointer",
    backgroundColor: "#007bff",
    color: "#fff",
    border: "none",
    padding: "0px 10px",
    borderRadius: "4px",
  };

  return (
    <div>
      <table
        className={tableClassName ? tableClassName : "table table-bordered table-hover"}
        style={tableStyle}
      >
        <thead>
          <tr>
            {columns.map(
              (column) =>
                !hiddenColumns.includes(column) && (
                  <th
                    key={column}
                    style={headerCellStyle}
                    className="text-center"
                  >
                    <div
                      style={{
                        ...headerStyle,
                        verticalAlign: column === "Actions" ? "top" : "middle",
                      }}
                    >
                      {columnNames[column] || column}
                      {column !== "Actions" && (<></>
                        // <button
                        //   style={searchButtonStyle}
                        //   onClick={() => toggleSearchVisibility(column)}
                        // >
                        //   showSearch[column] ? "−" : "+"
                        // </button>
                      )}
                    </div>
                    {column !== "Actions" && showSearch[column] && (
                      <div style={filterWrapperStyle}>
                        <input
                          type="text"
                          placeholder={`Search ${column}`}
                          value={searchQueries[column] || ""}
                          onChange={(e) =>
                            handleSearchChange(column, e.target.value)
                          }
                          className="form-control"
                        />
                      </div>
                    )}
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
