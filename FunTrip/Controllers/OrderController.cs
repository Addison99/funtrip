using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.IRepository;
using DataAccess.Repository;
using BusinessObject.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FunTrip.DTOs;
using AutoMapper;
using System;

namespace FunTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderRepository orderRepository;
        IMapper mapper;
        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }
        [HttpGet("{id}")]
        public OrderDTO get(int id)
        {
            return mapper.Map<OrderDTO>(orderRepository.Get(id));
        }
        [HttpDelete("{id}")]
        public void delete(int id)
        {
            Order order = orderRepository.Get(id);
            order.Status = "Active";
        }
        private void AddToDic(Dictionary<int,Order> dic, IEnumerable<Order> orders)
        {
            foreach (Order order in orders)
                if (!dic.ContainsKey(order.Id)) dic.Add(order.Id, order);
        }
        [HttpGet]
        public IEnumerable<OrderDTO> search(DateTime? starttime,DateTime? endtime,string? DriverName,
            string? EmployeeName, string? UserName, string? GroupName, string? DistrictOutsideName)
        {
            Dictionary<int,Order> dic = new Dictionary<int,Order>();
            if (starttime!=null && endtime != null)
            {
                var orders = orderRepository.GetList(x => x.StartTime <= starttime && x.EndTime >= endtime);
                AddToDic(dic, orders);
            }
            if (DriverName != null)
            {
                var orders = orderRepository.GetList(x => x.Driver.FullName.Contains(DriverName));
                AddToDic(dic, orders);  
            }
            if (EmployeeName != null)
            {
                var orders = orderRepository.GetList(x => x.Employee.FullName.Contains(EmployeeName));
                AddToDic(dic, orders);
            }
            if (UserName != null)
            {
                var orders = orderRepository.GetList(x => x.User.FullName.Contains(EmployeeName));
                AddToDic(dic, orders);
            }
            if (GroupName != null)
            {
                var orders = orderRepository.GetList(x => x.StartLocation.Group.GroupName.Contains(GroupName));
                AddToDic(dic, orders);
            }
            if (DistrictOutsideName != null)
            {
                var orders = orderRepository.GetList(x => x.EndLocation.District.Contains(DistrictOutsideName));
                AddToDic(dic, orders);
            }
            return dic.Values.Select(x => mapper.Map<OrderDTO>(x));
        }
        [HttpPost("")]
        public string Create([FromBody] OrderDTO dto)
        {
            Order order = mapper.Map<Order>(dto);
            order.Status = "Pending";
            orderRepository.Create(order);
            return "OK";
        }
        [HttpPut("update")]
        public string update([FromBody] OrderDTO dto)
        {
            Order order = orderRepository.Get(dto.Id);
            order.Address = dto.Address;
            order.Feedback = dto.Feedback;
            order.Rate = dto.Rate;
            order.Cost = dto.Cost;
            order.EndLocationId = dto.EndLocationId;
            order.VehicleId = dto.VehicleId;
            order.StartLocationId = dto.StartLocationId;
            order.IsRoundTrip = dto.IsRoundTrip;
            order.EmployeeId = dto.EmployeeId;
            order.DriverId = dto.DriverId;
            order.UserId = dto.UserId;
            try
            {
                orderRepository.Update(order);
            }catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return "OK";
        }
    }
}
