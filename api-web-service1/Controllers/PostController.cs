using api_web_service1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace api_web_service1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostContext _context;
        private readonly UserContext _contextuser;
    
        public PostController(PostContext context)
        {
            _context = context;
        }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPost([FromQuery] int? id)
        {
            var queryable = _context.Posts.AsQueryable();
            if (id is null)
            {
                return await _context.Posts.ToListAsync();
            }
            else
            {
                return await queryable.Where(x => x.Id == id).ToListAsync();
            }
        }
    
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
    
            if (post == null)
            {
                return NotFound();
            }
    
            return post;
        }
    
        [HttpGet("{id}/user")]
        public async Task<ActionResult<User>> GetPostUser(int id)
        {
            var post = await _context.Posts.FindAsync(id);
    
            User? user;
    
            if (post is not null)
            {
                user = await _contextuser.Users.FindAsync(post.user_Id);
            }
            else
            {
                return NotFound();
            }
            if (user is null)
            {
                return NotFound();
            }
            else
            {
                return user;
            }
        }
    
        [HttpPut]
        public async Task<IActionResult> PutPost(int id, Post item)
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
        public async Task<ActionResult<Post>> PostPost(Post item)
        {
            _context.Posts.Add(item);
            await _context.SaveChangesAsync();
    
            return CreatedAtAction(nameof(GetPost), new { id = item.Id }, item);
        }
    }
}
