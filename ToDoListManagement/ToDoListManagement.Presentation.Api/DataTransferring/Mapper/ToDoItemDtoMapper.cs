using AutoMapper;
using ToDoListManagement.Domain.Dtos.ToDoItemDtos;
using ToDoListManagement.Domain.ToDoItems;
using ToDoListManagement.Presentation.Contracts.ToDoItems.Requests;
using ToDoListManagement.Presentation.Contracts.ToDoItems.Responses;

namespace ToDoListManagement.Presentation.Api.DataTransferring.Mapper
{
    public class ToDoItemDtoMapper : Profile
    {
        public ToDoItemDtoMapper()
        {
            CreateMap<ToDoItem, ToDoItemDto>().ReverseMap();
            CreateMap<ToDoItem, ToDoItemInsertResponse>().ReverseMap();
            CreateMap<ToDoItem, ToDoItemGetResponse>().ReverseMap();
            CreateMap<ToDoItemUpdateRequest, ToDoItemDto>().ReverseMap();
            CreateMap<ToDoItemInsertRequest, ToDoItem>().ReverseMap();
        }
    }
}
