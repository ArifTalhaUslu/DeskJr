import Button from "./Button";

interface TopContentProps {
  hasNewRecordButton?: boolean;
  newRecordButtonOnClick?: () => void;
}

function TopContents(props: TopContentProps) {
  return (
    <>
      {props.hasNewRecordButton && (
        <div className="text-right">
          <Button
            className={"btn btn-success mb-2 mr-5"}
            text={"Add New Employee"}
          />
        </div>
      )}
    </>
  );
}

export default TopContents;
