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
    public class CategoryController : ControllerBase
    {
        ICategoryRepository categoryRepository;
        IMapper mapper;
        public CategoryController(IMapper mapper, ICategoryRepository categoryRepository)
        {
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
        }
        [HttpGet("{id}")]
        public CategoryDTO get(int id)
        {
            return mapper.Map<CategoryDTO>(categoryRepository.Get(id));
        }
        [HttpDelete("{id}")]
        public string delete(int id)
        {
            Category category = categoryRepository.Get(id);
            category.Status = "Inactive";
            try
            {
                categoryRepository.Update(category);
            }catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return "OK";
        }
        [HttpGet("")]
        public IEnumerable<CategoryDTO> search()
        {
            return categoryRepository.GetList(null).Select(x=> mapper.Map<CategoryDTO>(x));
        }
        [HttpPost("")]
        public string create([FromBody] CategoryDTO dto)
        {
            Category category = mapper.Map<Category>(dto);
            category.Status = "Active";
            categoryRepository.Create(category);
            return "OK";
        }
        [HttpPut("")]
        public string update([FromBody] CategoryDTO dto)
        {
            Category cate = categoryRepository.Get(dto.Id);
            try
            {
                categoryRepository.Update(cate);
            }catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return "ok";
        }

    }
}
