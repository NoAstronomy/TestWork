using System;

namespace TestWork.Db.Models
{
    public class EmployeeGroupModel
    {
        private EmployeeGroupModel()
        {
        }

        public EmployeeGroupModel(EmployeeModel employee, GroupModel group)
        {
            SetEmployee(employee);
            SetGroup(group);
        }

        public Guid EmployeeId { get; private set; }

        public EmployeeModel Employee { get; private set; }

        private void SetEmployee(EmployeeModel employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            EmployeeId = employee.Id;
            Employee = employee;
        }

        private void SetGroup(GroupModel group)
        {
            if (group is null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            GroupId = group.Id;
            Group = group;
        }

        public Guid GroupId { get; private set; }

        public GroupModel Group { get; private set; }
    }
}