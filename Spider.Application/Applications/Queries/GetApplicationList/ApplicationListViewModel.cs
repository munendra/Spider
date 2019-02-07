using System.Collections.Generic;

namespace Spider.Application.Applications.Queries.GetApplicationList
{
    public class ApplicationListViewModel
    {
        public ApplicationListViewModel()
        {
            Applications= new List<ApplicationLookUpModel>();
        }
        public IEnumerable<ApplicationLookUpModel> Applications { get; set; }
    }
}