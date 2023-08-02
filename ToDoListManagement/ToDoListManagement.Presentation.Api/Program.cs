using Microsoft.EntityFrameworkCore;
using ToDoListManagement.Application.Abstraction;
using ToDoListManagement.Application.ToDoItems.Commands;
using ToDoListManagement.Application.ToDoItems.Queries;
using ToDoListManagement.Domain.ToDoItems;
using ToDoListManagement.Persistence;
using ToDoListManagement.Persistence.Abstractions;
using ToDoListManagement.Persistence.DataAccessContext;
using ToDoListManagement.Presentation.Api.DataTransferring.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IQueryHandler<GetCompletedToDoItemsQuery, List<ToDoItem>>, GetCompletedToDoItemsQueryHandler>();
builder.Services.AddTransient<IQueryHandler<GetDeletedToDoItemsQuery, List<ToDoItem>>, GetDeletedToDoItemsQueryHandler>();
builder.Services.AddTransient<IQueryHandler<GetToDoItemByIdQuery, ToDoItem>, GetToDoItemByIdQueryHandler>();
builder.Services.AddTransient<IQueryHandler<GetToDoItemsQuery, List<ToDoItem>>, GetToDoItemsQueryHandler>();
builder.Services.AddTransient<ICommandHandler<DeleteToDoItemCommand>, DeleteToDoItemCommandHandler>();
builder.Services.AddTransient<ICommandHandler<InsertToDoItemCommand>, InsertToDoItemCommandHandler>();
builder.Services.AddTransient<ICommandHandler<UpdateToDoItemCommand>, UpdateToDoItemCommandHandler>();
builder.Services.AddTransient<IToDoItemRepository, ToDoItemRepository>();
builder.Services.AddDbContext<ToDoDbContext>();

builder.Services.AddAutoMapper(typeof(ToDoItemDtoMapper));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
