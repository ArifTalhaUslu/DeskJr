import Button from "../../CommonComponents/Button";
import Input from "../../CommonComponents/Input";
import { useEffect, useState } from "react";
import holidayService from "../../../services/HolidayService";

const HolidayEditForm: any = (props: any) => {
   
  useEffect(() => {
    if (props.selectedItemId) {
      holidayService.getHolidayById(props.selectedItemId).then((data) => {
        props.setSelectedHoliday(data);
      });
    }
  }, [props.selectedItemId]);

  const handleChange = (e: any) => {
    const { name, value } = e.target;
    props.setSelectedHoliday((prev: any) => ({
        ...prev,
        [name]: value,
      }));
    };

  const handleSubmit = (e: any) => {
    e.preventDefault();
    holidayService
      .addOrUpdateHoliday({
        ...props.selectedHoliday,
      })
      .then(() => {
        alert("success");
        props.getList();
        props.onClose();
      })
      .catch((err: any) => {
        console.log(err);
      });
  };

  const formatDate = (date: string | Date | undefined): string => {
    if (!date) return "";
    const dateObj = typeof date === "string" ? new Date(date) : date;
    const year = dateObj.getFullYear();
    const month = (dateObj.getMonth() + 1).toString().padStart(2, "0");
    const day = dateObj.getDate().toString().padStart(2, "0");
    return `${year}-${month}-${day}`;
  };

  return (
    <>
      <div
        className="modal fade"
        id="holidayAddModal"
        role="dialog"
        data-backdrop="static"
      >
        <div className="modal-dialog" role="document">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title">{props.modalModeName} Holiday</h5>
              <button
                type="button"
                className="close"
                id="form-close"
                data-dismiss="modal"
                hidden
              ></button>
              <button
                type="button"
                className="close"
                onClick={() => {
                  props.onClose();
                  const close_button = document.getElementById("form-close");
                  close_button?.click();
                }}
              >
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <form onSubmit={(e) => handleSubmit(e)}>
              <div className="modal-body">
                <Input
                  type="hidden"
                  name="Id"
                  value={props.selectedHoliday && props.selectedHoliday.id} 
                />
                <div className="form-group">
                  <label className="col-form-label">Name:</label>
                  <Input
                    type="text"
                    name="name"
                    value={
                      props.selectedHoliday && props.selectedHoliday.name 
                    }
                    onChange={(e: any) => handleChange(e)}
                    required
                  />
                  <label className="col-form-label">Start Date:</label>
                  <Input
                    type="date"
                    name="startDate"
                    value={
                      props.selectedHoliday && 
                      formatDate(props.selectedHoliday.startDate) 
                    }
                    onChange={(e: any) => handleChange(e)}
                    required
                  />
                  <label className="col-form-label">End Date:</label>
                  <Input
                    type="date"
                    name="endDate"
                    value={
                      props.selectedHoliday &&
                      formatDate(props.selectedHoliday.endDate) 
                    }
                    onChange={(e: any) => handleChange(e)}
                    required
                  />
                </div>
              </div>
              <div className="modal-footer">
                <Button
                  type="submit"
                  className="btn btn-primary"
                  text="Submit"
                />
              </div>
            </form>
          </div>
        </div>
      </div>
    </>
  );
};

export default HolidayEditForm;
