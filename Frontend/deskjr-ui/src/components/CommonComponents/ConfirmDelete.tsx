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
    <div className="modal fade show d-block" role="dialog" style={{ backgroundColor: "rgba(0,0,0,0.5)" }}>
      <div className="modal-dialog" role="document">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">Confirm Delete</h5>
            <button
              type="button"
              className="close"
              onClick={onClose}
            >
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <form onSubmit={(e) => { e.preventDefault(); onConfirm(e); }}>
            <div className="modal-body">
              <p>Are you sure you want to delete this item?</p>
            </div>
            <Input type="hidden" name="id" value={selectedItemId} />
            <div className="modal-footer">
              <Button
                type="button"
                className="btn btn-secondary"
                text="Cancel"
                onClick={onClose}
              />
              <Button
                type="submit"
                className="btn btn-danger"
                text="Delete"
              />
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}

export default ConfirmDelete;
