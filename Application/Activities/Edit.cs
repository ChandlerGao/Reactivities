using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Domain;
using Persistence;
using AutoMapper;

namespace Application.Activities
{
     public class Edit
     {
          public class Command : IRequest
          {
               public Activity Activity { get; set; }
          }

          public class Handler : IRequestHandler<Command>
          {
               private readonly DataContext _context;
               private readonly IMapper _mapper;

               public Handler(DataContext context, IMapper mapper)
               {
                    this._mapper = mapper;
                    this._context = context;
               }

               public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
               {
                    var activity = await this._context.Activities.FindAsync(request.Activity.Id);
                    //也可以在这里一个属性一个属性赋值，但可扩展性不行
                    //activity.Title = request.Activity.Title ?? activity.Title; 
                    this._mapper.Map(request.Activity, activity);
                    await this._context.SaveChangesAsync();
                    return Unit.Value;
               }
          }
     }
}