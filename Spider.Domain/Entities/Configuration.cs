using System;

namespace Spider.Domain.Entities
{
    public class Configuration : BaseEntity<Guid>
    {
        public Configuration()
        {
            IsActive = true;
        }

        public string Key { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public Guid EnvironmentId { get; set; }

        public Environment Environment { get; set; }

        public byte[] RowVersion { get; set; }
    }
}