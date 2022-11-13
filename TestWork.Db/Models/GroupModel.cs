using System;

namespace TestWork.Db.Models
{
    public class GroupModel
    {
        public GroupModel(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreatedAtUtc = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public DateTime CreatedAtUtc { get; private set; }
    }
}