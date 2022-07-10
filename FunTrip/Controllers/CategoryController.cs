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
using DataAccess.Paging;
namespace FunTrip.Controllers
{
    [Route("api/categories")]
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
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return "OK";
        }
        [HttpGet("")]
        public IEnumerable<CategoryDTO> search(int pageNumber, int pageSize)
        {
            if (pageNumber == 0) pageNumber = 1;
            if (pageSize == 0) pageSize = 10;
            PagingParams pagingParams = new PagingParams()
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            PagedList<Category> pagedList = new PagedList<Category>(categoryRepository.GetList(null).AsQueryable(), pageNumber, pageSize);
            IEnumerable<CategoryDTO> categoryDTOs = pagedList.List.Select
                (
                    x => mapper.Map<CategoryDTO>(x)
                    );
            return categoryDTOs;
        }
        [HttpPost("")]
        public string create([FromBody] CategoryDTO dto)
        {
            Category category = mapper.Map<Category>(dto);
            category.Status = "Active";
            categoryRepository.Create(category);
            return "OK";
        }
        [HttpPut("{id}")]
        public string update([FromRoute]int id, [FromBody] CategoryDTO dto)
        {
            Category cate = categoryRepository.Get(id);
            try
            {
                categoryRepository.Update(cate);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return "ok";
        }

    }
}
