using AutoMapper;
using CrudAPI.Models;
using CrudAPI.DTOs;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<MyUser, MyUserDTO>();
        CreateMap<Product, ProductDTO>();

        // Add other mappings as needed
    }
}
