using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DeskJr.Entity.Types;

namespace DeskJr.Entity.Models
{
    public class Leave : BaseEntity
    {
        public Guid RequestingEmployeeId { get; set; }
        public Employee RequestingEmployee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("LeaveTypeId")]
        public Guid LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }
        //public DateTime DateRequested { get; set; }
        public string RequestComments { get; set; }
        //public DateTime DateActioned { get; set; }
        public  EnumStatusOfLeave StatusOfLeave { get; set; }
        [ForeignKey("ApprovedById")]
        public Guid? ApprovedById { get; set; }
        public Employee? ApprovedBy { get; set; }
        public string? ConfirmDescription { get; set; } 
    }
}