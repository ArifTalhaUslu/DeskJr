import Button from "./Button";
import Input from "./Input";

interface ConfirmDeleteProps {
  onConfirm: (item: any) => void;
  onClose: () => void;
  selectedItemId: string;
}

function ConfirmDelete({
  onConfirm,
  selectedItemId,
  onClose,
}: ConfirmDeleteProps) {
  return (
    <div id="delete-confirm" className="modal fade" role="dialog">
      <div className="modal-dialog" role="document">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">Confirm Delete</h5>
            <button
              type="button"
              className="close"
              id="delete-form-closed"
              data-dismiss="modal"
              hidden
            ></button>
            <button
              type="button"
              className="close"
              onClick={() => {
                const close_button =document.getElementById("delete-form-closed");
                close_button?.click();
                onClose();
              }}
            >
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <form onSubmit={(e) => onConfirm(e)}>
            <div className="modal-body">
              <p>Are you sure you want to delete this item?</p>
            </div>
            <Input type="hidden" name="id" value={selectedItemId} />
            <div className="modal-footer">
              <Button
                type="button"
                className="btn btn-secondary"
                text="Cancel"
                onClick={() => {
                  const close_button =
                    document.getElementById("delete-form-closed");
                  close_button?.click();
                }}
              />
              <Button
                type="submit"
                className="btn btn-danger"
                text="Delete"
                onClick={onConfirm}
              />
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}

export default ConfirmDelete;
