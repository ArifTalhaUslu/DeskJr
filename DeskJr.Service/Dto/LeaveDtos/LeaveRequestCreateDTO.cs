﻿namespace DeskJr.Service.Dto.LeaveDtos

{
    public class LeaveRequestCreateDTO
    {
        public Guid RequestingEmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid LeaveTypeId { get; set; }
        public string RequestComments { get; set; }
        public Guid? ApprovedById { get; set; }
    }
}