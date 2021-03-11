
using Volo.Abp.Application.Dtos;

namespace Sample.Demo.Incident
{
    public class GetIncidentListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }

    public class GetIncidentDetailListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }

    public class GetReviewDetailListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
