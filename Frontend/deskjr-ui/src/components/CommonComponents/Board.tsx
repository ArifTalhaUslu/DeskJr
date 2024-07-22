import TopContents from './TopContents';
import DataTable from './DataTable';

interface BoardProps {
    hasDataTable?: boolean;
    items?: any[] | null;
    onEdit?: (item: any) => void;
    onDelete?: (item: any) => void;
    isEditable?: (item: any) => boolean;
    isDeletable?: (item: any) => boolean;
    hiddenColumns?: string[];
    renderColumn?: (column: string, value: any) => JSX.Element | string;
    hasNewRecordButton?: boolean;
    newRecordButtonOnClick?: () => void;
  }

  function Board({hasDataTable = true, items, onEdit, onDelete, isEditable, isDeletable, hiddenColumns, renderColumn, hasNewRecordButton, newRecordButtonOnClick}: BoardProps) 
  {
    const renderedItems = items ?? [];

    return (
        <>
        <TopContents
        hasNewRecordButton={hasNewRecordButton}
        newRecordButtonOnClick={newRecordButtonOnClick}/>
        {hasDataTable ? (
            <DataTable
            items={renderedItems}
            onEdit={onEdit}
            onDelete={onDelete}
            isEditable={isEditable}
            isDeletable={isDeletable}
            hiddenColumns={hiddenColumns}
            renderColumn={renderColumn}
          />
        ):[]}
        </>
    )
  }

  export default Board;