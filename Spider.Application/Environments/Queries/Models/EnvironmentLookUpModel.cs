using System;

namespace Spider.Application.Environments.Queries.Models
{
    public class EnvironmentLookUpModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}