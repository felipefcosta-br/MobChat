using AutoMapper;
using MobChat.Application.Models.ViewModels;
using MobChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.Application.AutoMapper
{
    class ViewModelToDomainMappingChat : Profile
    {
        public ViewModelToDomainMappingChat()
        {
            CreateMap<ChatViewModel, Chat>();
        }
    }
}
