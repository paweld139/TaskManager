using AutoMapper;
using PDCore.Utils;
using TaskManager.BLL.Entities.Basic;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Entities.DTO;
using TaskManager.DAL.Entities;
using TaskManager.DAL.Proxies;

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

            CreateMap<Ticket, TicketDTOProxy>()
                .ReverseMap();

            CreateMap<Ticket, TicketBasic>()
                .ReverseMap();

            CreateMap<Ticket, TicketDetailsProxy>()
                .ReverseMap();


            CreateMap<Comment, CommentDTO>()
                .ReverseMap();

            CreateMap<Comment, CommentBasic>()
                .ReverseMap();


            CreateMap<Contrahent, ContrahentBrief>()
                .ReverseMap();


            CreateMap<Employee, EmployeeDTO>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(e => string.Concat(e.FirstName, " ", e.LastName)))
                .ReverseMap();

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

            //CreateMap<Ticket, TicketModel>()
            //    .ReverseMap();

            //CreateMap<Ticket, TicketDetails>()
            //    .ReverseMap()
            //    .ForMember(t => t.Contrahent, opt => opt.Ignore())
            //    .ForMember(t => t.Operator, opt => opt.Ignore())
            //    .ForMember(t => t.Representative, opt => opt.Ignore())
            //    .ForMember(t => t.Type, opt => opt.Ignore())
            //    .ForMember(t => t.Priority, opt => opt.Ignore())
            //    .ForMember(t => t.Status, opt => opt.Ignore());

            //.ForMember(t => t.RowVersion, opt => opt.Ignore());
        }
    }
}
