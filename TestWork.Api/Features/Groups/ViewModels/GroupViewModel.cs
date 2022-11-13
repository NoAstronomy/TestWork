using AutoMapper;
using System;
using TestWork.Db.Models;

namespace TestWork.Api.Features.Groups.ViewModels
{
    public class GroupViewModel
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }
    }

    public class GroupViewModelProfiler : Profile
    {
        public GroupViewModelProfiler()
        {
            CreateMap<GroupModel, GroupViewModel>();
        }
    }
}