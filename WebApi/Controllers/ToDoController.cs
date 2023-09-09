using Application.Commands;
using Application.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateToDoCommand command)
        {
            var taskId = await _mediator.Send(command);
            return Ok(taskId);
        }

        [HttpGet]
        public async Task<ActionResult<List<Task>>> GetAll()
        {
            var query = new GetToDoListQuery();
            var tasks = await _mediator.Send(query);
            return Ok(tasks);
        }
    }

}
