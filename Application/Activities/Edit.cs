using System.Net;

using MediatR;
using Domain;
using Persistence;
using AutoMapper;
using FluentValidation;
using Application.Core;

namespace Application.Activities
{
     public class Edit
     {
          public class Command : IRequest<Result<Unit>>
          {
               public Activity Activity { get; set; }
          }
          public class CommandValidator : AbstractValidator<Command>
          {

               public CommandValidator()
               {
                    RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
               }
          }
          public class Handler : IRequestHandler<Command, Result<Unit>>
          {
               private readonly DataContext _context;
               private readonly IMapper _mapper;

               public Handler(DataContext context, IMapper mapper)
               {
                    this._mapper = mapper;
                    this._context = context;
               }

               public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
               {
                    var activity = await this._context.Activities.FindAsync(request.Activity.Id);
                    if (activity == null) return null;
                    //也可以在这里一个属性一个属性赋值，但可扩展性不行
                    //activity.Title = request.Activity.Title ?? activity.Title; 
                    this._mapper.Map(request.Activity, activity);
                    var result = await this._context.SaveChangesAsync() > 0;
                    System.Diagnostics.Debug.WriteLine($"result={result}");
                    if (!result) return Result<Unit>.Failure("Failed to update activity");
                    return Result<Unit>.Success(Unit.Value);
               }
          }
     }
}