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
          public async Task<ActionResult<List<Activity>>> GetActivities()
          {
               return await this.Mediator.Send(new List.Query());
          }

          [HttpGet("{id}")]//api/activities/id
          public async Task<ActionResult<Activity>> GetActivity(Guid id)
          {
               return await this.Mediator.Send(new Details.Query { Id = id });
          }

          [HttpPost]
          public async Task<IActionResult> CreateActivity(Activity activity)
          {
               return Ok(await this.Mediator.Send(new Create.Command() { Activity = activity }));
          }

          [HttpPut("{id}")]
          public async Task<IActionResult> EditActivity(Guid id, Activity activity)
          {
               activity.Id = id;
               return Ok(await this.Mediator.Send(new Edit.Command() { Activity = activity }));
          }


          [HttpDelete("{id}")]
          public async Task<IActionResult> DeleteActivity(Guid id)
          {
               return Ok(await this.Mediator.Send(new Delete.Command() { Id = id }));
          }
     }
}