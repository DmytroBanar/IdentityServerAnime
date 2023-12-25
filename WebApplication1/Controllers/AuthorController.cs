using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Entities;
using WebApplication1.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepo;

        public AuthorController(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }

        [HttpGet("GetAllAuthors")]
        public async Task<ActionResult<IEnumerable<author>>> GetAllAuthors()
        {
            try
            {
                var authors = await _authorRepo.GetAllAuthors();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                // Логування помилки
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "AuthorById")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            try
            {
                var author = await _authorRepo.GetAuthorById(id);
                if (author == null)
                    return NotFound();

                return Ok(author);
            }
            catch (Exception ex)
            {
                // Логування помилки
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AuthorForCreationDto author)
        {
            try
            {
                var createdAuthor = await _authorRepo.CreateAuthor(author);
                return CreatedAtRoute("AuthorById", new { id = createdAuthor.author_id }, createdAuthor);
            }
            catch (Exception ex)
            {
                // Логування помилки
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorForUpdateDto author)
        {
            try
            {
                var dbAuthor = await _authorRepo.GetAuthorById(id);
                if (dbAuthor == null)
                    return NotFound();

                await _authorRepo.UpdateAuthor(id, author);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Обробка помилок тут
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                await _authorRepo.DeleteAuthor(id);
                return NoContent(); // Повертаємо статус 204 No Content
            }
            catch (Exception ex)
            {
                // Обробка помилки (наприклад, логування)
                return StatusCode(500, ex.Message);
            }
        }

      
        
    }
}
