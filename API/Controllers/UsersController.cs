using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]   NO LONGER NEEDED AS THEY EXIST IN BASEAPICONTROLLER PARENT CLASS
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
        }

        //adding two endpoints for all users in db, and specific user in db
        // This code is not scalable as it is synchronous, not asynchronous!!!
        // [HttpGet]
        // public ActionResult<IEnumerable<AppUser>> GetUsers() //used enumerable because users list doesn't need functionality
        // {
        //     return _context.Users.ToList();
        // }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers() //uses task async and await to leave thread open until the response is ready
        {

            var users = await _userRepository.GetMembersAsync();
            return Ok(users);
        }


        // to do api/users/x to retrieve individual user #x - their id
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username) //used enumerable because users list doesn't need functionality
        {
            return await _userRepository.GetMemberAsync(username);
        }
    }
}