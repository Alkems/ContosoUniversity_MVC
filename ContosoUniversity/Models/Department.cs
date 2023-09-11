﻿using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ContosoUniversity.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        [StringLength(50, MinimumLength = 2)]

        public int Budget { get; set; }

        public string Name { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        public int? InstructorID { get; set; }
        [Timestamp]
        public byte RowVersion { get; set; }
        public Instructor Administrator { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
