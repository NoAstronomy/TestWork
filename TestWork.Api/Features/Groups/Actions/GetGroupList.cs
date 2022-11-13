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
using TestWork.Api.Features.Groups.ViewModels;
using TestWork.Db;

namespace TestWork.Api.Features.Groups.Actions
{
    public class GetGroupList
    {
        public class Query : IRequest<GroupViewModel[]>
        {
        }

        public class Handler : IRequestHandler<Query, GroupViewModel[]>
        {

            private readonly TestWorkDbContext _dbContext;
            private readonly IMapper _mapper;
            public Handler(TestWorkDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<GroupViewModel[]> Handle(Query request, CancellationToken cancellationToken = default) => 
                await _dbContext.Groups
                    .AsNoTracking()
                    .ProjectTo<GroupViewModel>(_mapper.ConfigurationProvider)
                    .OrderBy(x => x.Name)
                    .ToArrayAsync(cancellationToken);
        }
    }
}
