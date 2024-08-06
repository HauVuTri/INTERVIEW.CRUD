using System;
using System.Collections.Generic;

namespace INTERVIEW.CRUD.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Position { get; set; }
        public decimal? Salary { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
