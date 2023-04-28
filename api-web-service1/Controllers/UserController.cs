using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_web_service1.Models;

namespace api_web_service1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;
        
        public UserController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers([FromQuery] string? username)
        {
            var queryable = _context.Users.AsQueryable();
            if (string.IsNullOrWhiteSpace(username))
            {
                return await _context.Users.ToListAsync();
            }
            else
            {
                return await queryable.Where(x => x.NomeUtente == username).ToListAsync();
            }
        }
    }
}
