using AutoMapper;
using TaskListWebApplication.Models.DbModels;
using TaskListWebApplication.Models.Dto;

namespace TaskListWebApplication.Mapping;

public class MainMappingProfile : Profile
{
    public MainMappingProfile()
    {
        CreateMap<ProjectDb, ProjectDto>();
        CreateMap<SprintDb, SprintDto>();
        CreateMap<TaskDb, UserTaskDto>();
    }
}