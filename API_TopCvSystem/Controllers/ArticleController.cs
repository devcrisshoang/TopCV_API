using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopCVSystemAPIdotnet.Data;
using TopCVSystemAPIdotnet.Data.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace TopCVSystemAPIdotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly TopCvDbContext _context;

        public ArticleController(TopCvDbContext context)
        {
            _context = context;
        }

        // GET: api/Article
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticle()
        {
            return await _context.Article.ToListAsync();
        }

        // GET: api/Article/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _context.Article.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // GET: api/Article/ByRecruiter/5
        [HttpGet("ByRecruiter/{recruiterId}")]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticleByRecruiter(int recruiterId)
        {
            try
            {
                // Check if the Recruiter with the specified ID exists in the Recruiter table
                var existingRecruiter = await _context.Recruiter.FindAsync(recruiterId);

                if (existingRecruiter == null)
                {
                    // If the Recruiter does not exist, return a NotFound response
                    return NotFound($"Recruiter with ID {recruiterId} does not exist.");
                }

                // Get all Article that have the matching ID_Recruiter
                var Article = await _context.Article
                    .Where(a => a.ID_Recruiter == recruiterId)
                    .ToListAsync();

                // Return the list of Article
                return Ok(Article);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Article
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            try
            {
                // Check if the Recruiter with the specified ID exists in the Recruiter table
                var existingRecruiter = await _context.Recruiter.FindAsync(article.ID_Recruiter);

                if (existingRecruiter == null)
                {
                    // If the Recruiter does not exist, return a BadRequest response
                    return BadRequest($"Recruiter with ID {article.ID_Recruiter} does not exist.");
                }

                // Add the new Article entry
                _context.Article.Add(article);
                await _context.SaveChangesAsync();

                // Return the created response with the new article
                return CreatedAtAction(nameof(GetArticle), new { id = article.ID }, article);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Cập nhật Company
        [HttpPut("{ID}")]
        public async Task<IActionResult> Edit(int ID, [FromBody] Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 nếu dữ liệu không hợp lệ
            }

            var existingArticle = await _context.Article.FindAsync(ID);
            if (existingArticle == null)
            {
                return NotFound(); // 404 nếu không tìm thấy
            }

            existingArticle.Article_Name = article.Article_Name;
            existingArticle.Content = article.Content;
            existingArticle.Create_Time = article.Create_Time;
            existingArticle.ID_Recruiter = article.ID_Recruiter;

            _context.Article.Update(existingArticle);
            await _context.SaveChangesAsync();

            return NoContent(); // Trả về 204 No Content sau khi cập nhật thành công
        }

        // DELETE: api/Article/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Article.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Private method to check if Article exists
        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.ID == id);
        }
    }
}
