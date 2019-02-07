using System.Collections.Generic;
using Spider.Application.Environments.Queries.Models;

namespace Spider.Application.Environments.Queries.GetApplicationEnvironments
{
    public class EnvironmentListViewModel
    {
        public EnvironmentListViewModel()
        {
            Environments = new HashSet<EnvironmentLookUpModel>();
        }

        public IEnumerable<EnvironmentLookUpModel> Environments { get; set; }
    }
}