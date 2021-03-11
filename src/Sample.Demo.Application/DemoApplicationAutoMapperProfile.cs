using AutoMapper;
using Sample.Demo.Incident;

namespace Sample.Demo
{
    public class DemoApplicationAutoMapperProfile : Profile
    {
        public DemoApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<IncidentMaster, IncidentDto>();
            CreateMap<CreateIncidentDto, IncidentMaster>();
            CreateMap<UpdateIncidentDto, IncidentMaster>();

            CreateMap<IncidentDetail, IncidentDetailDto>();
            CreateMap<CreateIncidentDetailDto, IncidentDetail>();
            CreateMap<UpdateIncidentDetailDto, IncidentDetail>();

            CreateMap<ReviewDetail, ReviewDetailDto>();
            CreateMap<CreateReviewDetailDto, ReviewDetail>();
            CreateMap<UpdateReviewDetailDto, ReviewDetail>();
        }
    }
}
