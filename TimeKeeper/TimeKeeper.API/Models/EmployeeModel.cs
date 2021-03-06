﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeKeeper.API.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        [RegularExpression(@"^(\p{L}+)$")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        [RegularExpression(@"^(\p{L}+)$")]
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        [FileExtensions(Extensions = "jpg,", ErrorMessage = "File with jpg format is required")]
        public string Image { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is in wrong format")]
        public string Email { get; set; }
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Phone is in wrong format")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Salary is required")]
        [Range(0,100000,ErrorMessage = "Salary must be between 0 and 100000")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "Birth date is required")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Begin date is required")]
        [DataType(DataType.Date)]
        public DateTime BeginDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "Employee status is required")]
        public int Status { get; set; }

        public BaseModel<string> Position { get; set; }
    }
}