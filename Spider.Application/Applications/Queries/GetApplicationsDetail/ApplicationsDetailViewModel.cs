using Spider.Application.Applications.Queries.GetApplicationList;

namespace Spider.Application.Applications.Queries.GetApplicationsDetail
{
    public class ApplicationsDetailViewModel
    {
        public ApplicationsDetailViewModel()
        {
            TotalEnvironment = 0;
        }

        public int TotalEnvironment { get; set; }

        public ApplicationLookUpModel Application { get; set; }
    }
}