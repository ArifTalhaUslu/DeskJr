import Button from "./Button";

interface TopContentProps {
  hasNewRecordButton?: boolean;
  newRecordButtonOnClick?: () => void;
  dataTarget?:string;
}

function TopContents(props: TopContentProps) {
  return (
    <>
      {props.hasNewRecordButton && (
        <div className="text-right">
          <Button
            className={"btn btn-success mb-2 mr-5"}
            text={"Add New"}
            isModalTrigger={true}
            dataTarget={props.dataTarget}
            onClick={props.newRecordButtonOnClick}
          />
        </div>
      )}
    </>
  );
}

export default TopContents;
