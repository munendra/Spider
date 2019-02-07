using System;
using Spider.Application.Environments.Commands.Model;

namespace Spider.Application.Environments.Commands.UpdateEnvironment
{
    public class UpdateEnvironmentDto : EnvironmentDto
    {
        public Guid Id { get; set; }
    }
}