using AutoMapper;
using FunTrip.DTOs;
using BusinessObject.Models;
using System.Linq;

namespace FunTrip.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();

            CreateMap<User, UserDTO>()
                .ForMember(des=> des.NumberOfOrders, act => act.MapFrom(src => src.Orders.Count()));    
            CreateMap<UserDTO, User>();

            CreateMap<Area, AreaDTO>()
                .ForMember(des => des.NumberOfGroups, act => act.MapFrom(src => src.AreaGroups.Count))
                .ForMember(des => des.DistrictName, act => act.MapFrom(src => src.District.District1));
            CreateMap<AreaDTO, Area>();

            CreateMap<Category, CategoryDTO>()
                .ForMember(des => des.NumberOfCars, act => act.MapFrom(src => src.Vehicles.Count));
            CreateMap<CategoryDTO, Category>();

            CreateMap<City, CityDTO>()
                .ForMember(des => des.NumberOfDistricts, act=> act.MapFrom(src=> src.DistrictOutsides.Count));
            CreateMap<CityDTO, City>();

            CreateMap<District, DistrictDTO>()
                .ForMember(des => des.NumberOfAreas, act => act.MapFrom(src => src.Areas.Count));
            CreateMap<DistrictDTO, District>();

            CreateMap<DistrictOutside, DistrictOutsideDTO>()
                .ForMember(des => des.CityName, act => act.MapFrom(src => src.City.City1));
            CreateMap<DistrictOutsideDTO, DistrictOutside>();

            CreateMap<Driver, DriverDTO>()
                .ForMember(des => des.GroupName, act => act.MapFrom(src => src.Group.GroupName))
                .ForMember(des => des.VehicleName, act => act.MapFrom(src => src.Vehicles.FirstOrDefault().VehicleName))
                .ForMember(des => des.Email, act => act.MapFrom(src => src.Account.Email))
                .ForMember(des => des.rate, act
                   => act.MapFrom(src => src.Bookings.Average(x=> x.Rate)));
            CreateMap<DriverDTO, Driver>();

            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<Group, GroupDTO>()
                .ForMember(des => des.NumberOfMembers, act => act.MapFrom(src => src.Drivers.Count))
                .ForMember(des => des.NumberOfAreas, act => act.MapFrom(src => src.AreaGroups.Count));
            CreateMap<GroupDTO, Group>();

            CreateMap<Booking, OrderDTO>()
                .ForMember(des => des.DriverName, act => act.MapFrom(src => src.Driver.FullName))
                .ForMember(des => des.EmployeeName, act => act.MapFrom(src => src.Employee.FullName))
                .ForMember(des => des.StartLoction, act => act.MapFrom(src => src.StartLocation.Area.ApartmentName))

                .ForMember(des => des.GroupName, act => act.MapFrom(src => src.Driver.Group.GroupName));
            CreateMap<OrderDTO, Booking>();

            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>(); 

            CreateMap<User, UserDTO>()
                .ForMember(des => des.Email, act => act.MapFrom(src => src.Account.Email))
                .ForMember(des => des.NumberOfOrders, act => act.MapFrom(src => src.Orders.Count));
            CreateMap<UserDTO, User>();

            CreateMap<Vehicle, VehicleDTO>()
                .ForMember(des => des.DriverName, act => act.MapFrom(src => src.Driver.FullName))
                .ForMember(des => des.CategoryName, act => act.MapFrom(src => src.Category.Category1));
            CreateMap<VehicleDTO, Vehicle>();   
        }
    }
}
