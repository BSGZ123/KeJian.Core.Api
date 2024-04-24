﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GuiJun.Core.Application.Interface;
using KeJian.Core.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuiJun.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyController : ControllerBase
    {
        private readonly IBaseApplication<Study> _application;

        public StudyController(IBaseApplication<Study> application)
        {
            _application = application;
        }

        /// <summary>
        ///     学习模块列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Study>> GetAsync()
        {
            return await _application.GetAsync();
        }

        /// <summary>
        ///     学习模块详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Study> GetAsync(int id)
        {
            return await _application.GetAsync(id);
        }

        /// <summary>
        ///     创建或修改学习模块
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<Study> CreateOrUpdateAsync(Study input)
        {
            return await _application.CreateOrUpdateAsync(input);
        }

        /// <summary>
        ///     删除学习模块
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<bool> DeleteAsync(int id)
        {
            return await _application.DeleteAsync(id);
        }
    }
}