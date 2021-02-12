using AutoMapper;
using LogProxy.Application.Interfaces.Mapping;
using LogProxy.Application.Providers.Airtable.Models;
using System;

namespace LogProxy.Application.CQRS.Messages.Models
{
    public class MessagesViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime? ReceivedAt { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<MessagesResponse.Record, MessagesViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => c.Fields.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(c => c.Fields.Summary))
                .ForMember(x => x.Text, opt => opt.MapFrom(c => c.Fields.Message))
                .ForMember(x => x.ReceivedAt, opt => opt.MapFrom(c => c.Fields.ReceivedAt));
        }
    }
}
