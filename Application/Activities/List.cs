using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
     public class List
     {
          public class Query : IRequest<Result<List<ActivityDto>>>
          {

          }

          public class Handler : IRequestHandler<Query, Result<List<ActivityDto>>>
          {
               private readonly DataContext _context;
               private readonly IMapper _mapper;

               public Handler(DataContext context, IMapper mapper)
               {
                    this._mapper = mapper;
                    this._context = context;
               }

               public async Task<Result<List<ActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
               {
                    // //Method 1: Eager loading
                    // var activities = await _context.Activities
                    // .Include(x => x.Attendees)
                    // .ThenInclude(y => y.AppUser)
                    // .ToListAsync(cancellationToken);
                    // var activitiesToReturn = this._mapper.Map<List<ActivityDto>>(activities);
                    // return Result<List<ActivityDto>>.Success(activitiesToReturn);

                    //Method 2: 
                    var activities = await _context.Activities                    
                         .ProjectTo<ActivityDto>(this._mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);                    
                    return Result<List<ActivityDto>>.Success(activities);
               }
          }
     }
}