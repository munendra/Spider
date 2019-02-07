using System;

namespace Spider.Application.Environments.Commands.Model
{
    public class EnvironmentDto
    {
        public Guid ApplicationId { get; set; }
        public string Name { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}