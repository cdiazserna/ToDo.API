using AutoMapper;
using ToDo.API.DAL.Entities;
using ToDo.API.Models;

namespace ToDo.API.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TodoItemDTO, Todo>().ReverseMap();
            CreateMap<Todo, TodoItemResponse>().ReverseMap();
        }
    }
}
