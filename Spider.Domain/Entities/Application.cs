using System;
using System.Collections.Generic;

namespace Spider.Domain.Entities
{
    public class Application : BaseEntity<Guid>
    {
        public Application()
        {
            Environments = new HashSet<Environment>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Environment> Environments { get; }

        public byte[] Rowversion { get; set; }
    }
}