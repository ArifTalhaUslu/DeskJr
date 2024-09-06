import Button from "./Button";
import Input from "./Input";
import leaveService from "../../services/LeaveService";
import { useEffect, useState } from 'react';
import { showErrorToast } from "../../utils/toastHelper";
interface testProps {
  modalId: string;
  selectedItemId?: string;
  selectedLeave?: any;
  setSelectedLeave?: (item: any) => void;
  title?: string;
  message?: string;
  context?: JSX.Element;
  onConfirm?: (item: any) => void;
  onClose?: () => void;
}

function ConfirmModal({
  title,
  message,
  context,
  onClose,
  onConfirm,
  selectedItemId,
  setSelectedLeave,
  modalId,
}: testProps) {
  useEffect(() => {
    if (selectedItemId) {
      leaveService
        .getLeaveById(selectedItemId)
        .then((data) => {
          setSelectedLeave(data);
        })
        .catch((err) => {
          showErrorToast(err);
        });
    }
  }, [selectedItemId]);

  return (
    <div
      id={modalId}
      className="modal fade"
      role="dialog"
      data-backdrop="static"
    >
      <div className="modal-dialog" role="document">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">{title}</h5>
            <button
              type="button"
              className="close"
              id={`form-closed-${modalId}`}
              data-dismiss="modal"
              hidden
            ></button>
            <button
              type="button"
              className="close"
              onClick={() => {
                const close_button = document.getElementById(`form-closed-${modalId}`);
                close_button?.click();
                onClose();
              }}
            >
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <form onSubmit={(e) => onConfirm(e)}>
            <div className="modal-body">
              <Input type="hidden" name="id" value={selectedItemId} />
              <p>{message}</p>
              {context && context}
            </div>

            <div className="modal-footer">
              <Button
                type="button"
                className="btn btn-secondary"
                text="Cancel"
                onClick={() => {
                  const close_button = document.getElementById(`form-closed-${modalId}`);
                  close_button?.click();
                }}
              />
              <Button type="submit" className="btn btn-danger" text="Okey" />
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}

export default ConfirmModal;
