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
    public class MessageController : ControllerBase
    {
        private readonly IBaseApplication<Message> _application;

        public MessageController(IBaseApplication<Message> application)
        {
            _application = application;
        }

        /// <summary>
        ///     消息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Message>> GetAsync()
        {
            return await _application.GetAsync();
        }

        /// <summary>
        ///     消息详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Message> GetAsync(int id)
        {
            return await _application.GetAsync(id);
        }

        /// <summary>
        ///     创建或修改消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<Message> CreateOrUpdateAsync(Message input)
        {
            return await _application.CreateOrUpdateAsync(input);
        }

        /// <summary>
        ///     删除消息
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