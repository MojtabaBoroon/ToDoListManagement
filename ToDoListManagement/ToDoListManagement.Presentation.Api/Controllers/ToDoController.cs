using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoListManagement.Application.Abstraction;
using ToDoListManagement.Application.ToDoItems.Commands;
using ToDoListManagement.Application.ToDoItems.Queries;
using ToDoListManagement.Domain.Dtos.ToDoItemDtos;
using ToDoListManagement.Domain.ToDoItems;
using ToDoListManagement.Presentation.Contracts.ToDoItems.Requests;
using ToDoListManagement.Presentation.Contracts.ToDoItems.Responses;

namespace ToDoListManagement.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : Controller
    {
        private readonly IMapper _toDoItemMapper;

        public ToDoController(IMapper toDoItemMapper)
        {
            _toDoItemMapper = toDoItemMapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDoItem>>> GetToDoItems(
            [FromServices] IQueryHandler<GetToDoItemsQuery, List<ToDoItem>> getToDoItemsQueryHandler)
        {
            var toDos = await getToDoItemsQueryHandler.HandleAsync(new GetToDoItemsQuery());
            var toDoItemGetResponses = _toDoItemMapper.Map<List<ToDoItemGetResponse>>(toDos);
            return Ok(toDoItemGetResponses);
        }

        [HttpGet("completed")]
        public async Task<ActionResult<List<ToDoItem>>> GetCompletedToDoItems(
        [FromServices] IQueryHandler<GetCompletedToDoItemsQuery, List<ToDoItem>> getToDoItemsQueryHandler)
        {
            var toDos = await getToDoItemsQueryHandler.HandleAsync(new GetCompletedToDoItemsQuery());
            var toDoItemGetResponses = _toDoItemMapper.Map<List<ToDoItemGetResponse>>(toDos);

            return Ok(toDoItemGetResponses);
        }

        [HttpGet("Deleted")]
        public async Task<ActionResult<List<ToDoItem>>> GetDeletedToDoItems(
        [FromServices] IQueryHandler<GetDeletedToDoItemsQuery, List<ToDoItem>> getToDoItemsQueryHandler)
        {
            var toDos = await getToDoItemsQueryHandler.HandleAsync(new GetDeletedToDoItemsQuery());
            var toDoItemGetResponses = _toDoItemMapper.Map<List<ToDoItemGetResponse>>(toDos);

            return Ok(toDoItemGetResponses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItemById(
            [FromServices] IQueryHandler<GetToDoItemByIdQuery, ToDoItem> getToDoItemByIdQueryHandler,
            Guid id)
        {
            if (!Guid.TryParse(id.ToString(), out _))
            {
                return BadRequest("Invalid GUID format for the 'Id' parameter.");
            }
            var getToDoItemByIdQuery = new GetToDoItemByIdQuery
            {
                Id = id
            };
            var todo = await getToDoItemByIdQueryHandler.HandleAsync(getToDoItemByIdQuery);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> GetEnrichedTransactions(
            [FromBody] ToDoItemInsertRequest request,
            [FromServices] ICommandHandler<InsertToDoItemCommand> insertToDoItemCommandHandler)
        {
            ToDoItem toDoItem = _toDoItemMapper.Map<ToDoItem>(request);
            toDoItem.CreatedAt = DateTime.Now;

            await insertToDoItemCommandHandler.HandleAsync(CreateInsertToDoItemCommand(toDoItem));

            var toDoItemInsertResponse = _toDoItemMapper.Map<ToDoItemInsertResponse>(toDoItem);
            return CreatedAtAction(nameof(GetToDoItemById), new { id = toDoItemInsertResponse.Id }, toDoItemInsertResponse); ;
        }

        private static InsertToDoItemCommand CreateInsertToDoItemCommand(ToDoItem toDoItem)
        {
            return new InsertToDoItemCommand
            {
                ToDoItem = toDoItem
            };
        }

        [HttpPut()]
        public async Task<IActionResult> Put(
            [FromBody] ToDoItemUpdateRequest request,
            [FromServices] ICommandHandler<UpdateToDoItemCommand> updateToDoItemCommandHandler)
        {
            try
            {
                await updateToDoItemCommandHandler.HandleAsync(CreateUpdateToDoItemCommand(request));

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            }

        private UpdateToDoItemCommand CreateUpdateToDoItemCommand(ToDoItemUpdateRequest request)
        {
            return new UpdateToDoItemCommand
            {
                Id = request.Id,
                ToDoItemDto = _toDoItemMapper.Map<ToDoItemDto>(request)
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            Guid id,
            [FromServices] ICommandHandler<DeleteToDoItemCommand> deleteToDoItemCommandHandler)
        {
            try
            {
                await deleteToDoItemCommandHandler.HandleAsync(CreateDeleteToDoItemCommand(id));

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        private DeleteToDoItemCommand CreateDeleteToDoItemCommand(Guid id)
        {
            return new DeleteToDoItemCommand
            {
                Id = id
            };
        }
    }
}
