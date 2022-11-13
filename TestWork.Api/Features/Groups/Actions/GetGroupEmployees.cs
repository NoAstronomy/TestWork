using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestWork.Api.Features.Employees.ViewModels;
using TestWork.Db;

namespace TestWork.Api.Features.Groups.Actions
{
    public class GetGroupEmployees
    {
        public class Query : IRequest<EmployeeViewModel[]>
        {
            [FromRoute(Name = "groupId")]
            [Required]
            public Guid GroupId { get; set; }
        }

        public class Handler : IRequestHandler<Query, EmployeeViewModel[]>
        {

            private readonly TestWorkDbContext _dbContext;
            private readonly IMapper _mapper;
            public Handler(TestWorkDbContext dbContext, IMapper mapper)
            {
                 _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<EmployeeViewModel[]> Handle(Query request, CancellationToken cancellationToken = default) => 
                await _dbContext.Employees
                    .AsNoTracking()
                    .Where(x => x.Groups
                        .Select(y => y.GroupId)
                        .Contains(request.GroupId))
                    .ProjectTo<EmployeeViewModel>(_mapper.ConfigurationProvider)
                    .OrderBy(x => x.FullName)
                    .ToArrayAsync(cancellationToken);
        }
    }
}