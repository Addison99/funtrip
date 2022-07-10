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
using DataAccess.Paging;
using System;

namespace FunTrip.Controllers
{
    [Route("api/orders")]
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
            Booking order = orderRepository.Get(id);
            order.Status = "Active";
        }
        private void AddToDic(Dictionary<int, Booking> dic, IEnumerable<Booking> orders)
        {
            foreach (Booking order in orders)
                if (!dic.ContainsKey(order.Id)) dic.Add(order.Id, order);
        }
        [HttpGet]
        public IEnumerable<OrderDTO> search(bool? all,DateTime? starttime,DateTime? endtime,string? DriverName,
            string? EmployeeName, string? UserName, string? GroupName, int pageNumber, int pageSize)
        {
            if (pageNumber == 0) pageNumber = 1;
            if (pageSize == 0) pageSize = 10;
            if (all == true) 
            {
                var orders = orderRepository.GetList(x => x.Id != null).ToList();
                PagedList<Booking> pagedList1 = new PagedList<Booking>(orders.AsQueryable(), pageNumber, pageSize);
                IEnumerable<OrderDTO> orderDTOs1 = pagedList1.List.Select(x => mapper.Map<OrderDTO>(x));
                return orderDTOs1;
            }

            Dictionary<int, Booking> dic = new Dictionary<int, Booking>();
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
            if (GroupName != null)
            {
                var orders = orderRepository.GetList(x => x.StartLocation.Group.GroupName.Contains(GroupName));
                AddToDic(dic, orders);
            }          
                 PagedList<Booking> pagedList = new PagedList<Booking>(dic.Values.AsQueryable(), pageNumber, pageSize);
            IEnumerable<OrderDTO> orderDTOs = pagedList.List.Select(x => mapper.Map<OrderDTO>(x));
            return orderDTOs;
        }
        [HttpPost("")]
        public string Create([FromBody] OrderDTO dto)
        {
            Booking order = mapper.Map<Booking>(dto);
            order.Status = "Pending";
            orderRepository.Create(order);
            return "OK";
        }
        [HttpPut("{id}")]
        public string update(int id,[FromBody] OrderDTO dto)
        {
            Booking order = orderRepository.Get(dto.Id);
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
