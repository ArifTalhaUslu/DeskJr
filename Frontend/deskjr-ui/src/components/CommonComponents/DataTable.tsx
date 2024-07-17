import { useEffect, useState } from "react";

interface DataTableProps {
  items: any[];
  tableClassName?: string;
  onEdit?: (item: any) => void;
  onDelete?: (item: any) => void;
  isEditable?: boolean;
  isDeletable?: boolean;
  hiddenColumns?: string[];
  renderColumn?: (column: string, value: any) => JSX.Element | string;
}

function DataTable({ items, tableClassName, onEdit, onDelete, isEditable, isDeletable, hiddenColumns = [], renderColumn }: DataTableProps) {
  const [records, setRecords] = useState<any>([]);
  const [columns, setColumns] = useState<string[]>([]);

  useEffect(() => {
    if (items && items.length > 0) {
      const firstItem = items[0];
      const newColumns = Object.keys(firstItem).filter(column => !hiddenColumns.includes(column));
      setColumns([...newColumns, "Actions"]);

      const newRecords = items.map((record: any, index: any) => (
        <tr key={index}>
          {newColumns.map((column) => (
            <td className="text-center" key={column}>
              {renderColumn ? renderColumn(column, record[column]) : record[column]}
            </td>
          ))}
          <td className="text-center">
            {isEditable && onEdit && <button onClick={() => onEdit(record)}>Edit</button>}
            {isDeletable && onDelete && <button onClick={() => onDelete(record)}>Delete</button>}
          </td>
        </tr>
      ));
      setRecords([...newRecords]);
    } else {
      setRecords([]);
      setColumns([]);
    }
  }, [items, onEdit, onDelete, isEditable, isDeletable, hiddenColumns, renderColumn]);

  return (
    <table className={tableClassName ? tableClassName : "table table-bordered"}>
      <thead>
        <tr>
          {columns.map((column) => (
            <th className="text-center" key={column}>{column}</th>
          ))}
        </tr>
      </thead>
      <tbody>
        {
          records && records.length > 0
            ? records
            : (
              <tr>
                <td className="text-center" colSpan={columns.length}>No Records Found</td>
              </tr>
            )
        }
      </tbody>
    </table>
  );
}

export default DataTable;