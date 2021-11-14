using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]   NO LONGER NEEDED AS THEY EXIST IN BASEAPICONTROLLER PARENT CLASS
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;

        }

        //adding two endpoints for all users in db, and specific user in db
        // This code is not scalable as it is synchronous, not asynchronous!!!
        // [HttpGet]
        // public ActionResult<IEnumerable<AppUser>> GetUsers() //used enumerable because users list doesn't need functionality
        // {
        //     return _context.Users.ToList();
        // }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() //uses task async and await to leave thread open until the response is ready
        {
            return await _context.Users.ToListAsync();
        }


        // to do api/users/x to retrieve individual user #x - their id
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id) //used enumerable because users list doesn't need functionality
        {
            return await _context.Users.FindAsync(id);
        }
    }
}