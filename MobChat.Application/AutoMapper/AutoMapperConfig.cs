using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.Application.AutoMapper
{
    class AutoMapperConfig: Profile
    {
        public static MapperConfiguration RegisterMappings()
        {
            var config = new MapperConfiguration(mapper =>
            {
                mapper.AddProfile<DomainToViewModelMappingUser>();
                mapper.AddProfile<ViewModelToDomainMappingUser>();               
                mapper.AddProfile<DomainToViewModelMappingTextMessage>();
                mapper.AddProfile<ViewModelToDomainMappingTextMessage>();
                mapper.AddProfile<DomainToViewModelMappingChat>();
                mapper.AddProfile<ViewModelToDomainMappingChat>();
            });
            return config;
        }
    }
}
