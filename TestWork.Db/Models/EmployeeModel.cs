using System;
using System.Collections.Generic;

namespace TestWork.Db.Models
{
    public class EmployeeModel
    {
        public EmployeeModel(string fullName, string email)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Email = email;
            CreatedAtUtc = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }

        public string FullName { get; private set; }

        public string Email { get; private set; }

        public DateTime CreatedAtUtc { get; private set; }

        public ISet<EmployeeGroupModel> Groups { get; private set; } = new HashSet<EmployeeGroupModel>();
    }
}