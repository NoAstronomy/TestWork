using AutoMapper;
using System;
using TestWork.Db.Models;

namespace TestWork.Api.Features.Employees.ViewModels
{
    public class EmployeeViewModel
    {
        public Guid Id { get; private set; }

        public string FullName { get; private set; }

        public string Email { get; private set; }
    }

    public class EmployeeViewModelProfiler : Profile
    {
        public EmployeeViewModelProfiler()
        {
            CreateMap<EmployeeModel, EmployeeViewModel>();
        }
    }
}