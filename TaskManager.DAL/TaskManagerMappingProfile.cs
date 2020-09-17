using AutoMapper;
using PDCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Entities;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Entities.Details;
using TaskManager.BLL.Entities.DTO;

namespace TaskManager.DAL
{
    public class TaskManagerMappingProfile : Profile
    {
        public TaskManagerMappingProfile()
        {
            CreateMap<Dictionary, DictionaryBrief>()
                .ReverseMap();

            CreateMap<Ticket, TicketBrief>()
                .ReverseMap();

            //CreateMap<Ticket, TicketModel>()
            //    .ReverseMap();

            CreateMap<Ticket, TicketDetails>()
                .ReverseMap()
                .ForMember(t => t.Contrahent, opt => opt.Ignore())
                .ForMember(t => t.Operator, opt => opt.Ignore())
                .ForMember(t => t.Representative, opt => opt.Ignore())
                .ForMember(t => t.Type, opt => opt.Ignore())
                .ForMember(t => t.Priority, opt => opt.Ignore())
                .ForMember(t => t.Status, opt => opt.Ignore());

            CreateMap<Ticket, TicketDTO>()
                .ReverseMap();
            //.ForMember(t => t.RowVersion, opt => opt.Ignore());

            //ForAllMaps((typeMap, map) =>
            //{
            //    if (typeMap.SourceType is IModificationHistory && typeMap.DestinationType is IModificationHistory destinationType)
            //    {
            //        map.ForMember("RowVersion", opt => opt.Ignore());
            //    }
            //});

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
