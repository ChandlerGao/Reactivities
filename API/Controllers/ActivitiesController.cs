using System.Net.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using MediatR;
using Application.Activities;

namespace API.Controllers
{
     public class ActivitiesController : BaseApiController
     {
          [HttpGet]//api/activities
          public async Task<IActionResult> GetActivities()
          {
               return this.HandleResult(await this.Mediator.Send(new List.Query()));
          }

          [HttpGet("{id}")]//api/activities/id
          public async Task<IActionResult> GetActivity(Guid id)
          {
               return this.HandleResult(await this.Mediator.Send(new Details.Query { Id = id }));
          }

          [HttpPost]
          public async Task<IActionResult> CreateActivity(Activity activity)
          {
               return this.HandleResult(await this.Mediator.Send(new Create.Command() { Activity = activity }));
          }

          [HttpPut("{id}")]
          public async Task<IActionResult> EditActivity(Guid id, Activity activity)
          {
               activity.Id = id;
               return this.HandleResult(await this.Mediator.Send(new Edit.Command() { Activity = activity }));
          }

          [HttpDelete("{id}")]
          public async Task<IActionResult> DeleteActivity(Guid id)
          {
               return this.HandleResult(await this.Mediator.Send(new Delete.Command() { Id = id }));
          }
     }
}