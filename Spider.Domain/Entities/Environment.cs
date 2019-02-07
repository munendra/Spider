using System;
using System.Collections.Generic;

namespace Spider.Domain.Entities
{
    public class Environment : BaseEntity<Guid>
    {
        public Environment()
        {
            Configurations = new HashSet<Configuration>();
            IsActive = true;
        }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }


        public bool IsActive { get; set; }

        public Application Application { get; set; }

        public Guid ApplicationId { get; set; }

        public ICollection<Configuration> Configurations { get; }

        public byte[] Rowversion { get; set; }
    }
}