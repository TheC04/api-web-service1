using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_web_service1.Models;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace api_web_service1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly PostContext _contextpost;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var User = await _context.Users.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }

        [HttpGet("{id}/post")]
        public async Task<ActionResult<Post>> GetUserPost(int id)
        {
            var User = await _context.Users.FindAsync(id);

            Post? post;

            if (User is not null)
            {
                post = await _contextpost.Posts.FindAsync(User.Id);
            }
            else
            {
                return NotFound();
            }
            if(post is null)
            {
                return NotFound();
            }
            else
            {
                return post;
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(int id, User item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User item)
        {
            string hash = "";
            using (SHA512 shaM = new SHA512Managed())
            {
                hash = Encoding.ASCII.GetString(shaM.ComputeHash(Encoding.ASCII.GetBytes(item.Password)));
            }
            item.Password = hash;
            _context.Users.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var User = await _context.Users.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            _context.Users.Remove(User);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
