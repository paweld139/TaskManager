using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DAL
{
    public class TaskManagerMappingProfile : Profile
    {
        public TaskManagerMappingProfile()
        {
            //CreateMap<Camp, CampModel>()
            //    .ForMember(c => c.Venue, opt => opt.MapFrom(m => m.Location.VenueName))
            //    .ReverseMap();

            //CreateMap<Talk, TalkModel>()
            //    .ReverseMap()
            //    .ForMember(t => t.Speaker, opt => opt.Ignore())
            //    .ForMember(t => t.Camp, opt => opt.Ignore());

            //CreateMap<Speaker, SpeakerModel>()
            //    .ReverseMap();
        }
    }
}
