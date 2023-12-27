using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Entities;
using WebApplication1.Contracts;
using Microsoft.AspNetCore.Authorization;
using IdentityModel.Client;
using Newtonsoft.Json;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class AnimeController : ControllerBase
    {
        private readonly IAnimeRepository _animeRepo;
        private readonly ITokenService _tokenService;

        public AnimeController(ITokenService tokenService, IAnimeRepository animeRepo)
        {
            _tokenService = tokenService;
            _animeRepo = animeRepo;
        }

        [HttpGet("GetAllAnime")]
        public async Task<ActionResult<IEnumerable<about>>> GetAllAnime()
        {
            try
            {
                var anime = await _animeRepo.GetAllAnime(); // Метод для отримання списку аніме
                return Ok(anime);
            }
            catch (Exception ex)
            {
                // Логування помилки
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "AnimeById")]
        public async Task<IActionResult> GetAnimeById(int id)
        {
            try
            {
                var anime = await _animeRepo.GetAnimeById(id);
                if (anime == null)
                    return NotFound();

                return Ok(anime);
            }
            catch (Exception ex)
            {
                // Логування помилки
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnime(AnimeForCreationDto anime)
        {
            try
            {
                var createdAnime = await _animeRepo.CreateAnime(anime);
                return CreatedAtRoute("AnimeById", new { id = createdAnime.about_id }, createdAnime);
            }
            catch (Exception ex)
            {
                // Логування помилки
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnime(int id, AnimeForUpdateDto about)
        {
            try
            {
                var dbAnime = await _animeRepo.GetAnimeById(id);
                if (dbAnime == null)
                    return NotFound();

                await _animeRepo.UpdateAnime(id, about);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Обробка помилок тут
                return StatusCode(500, ex.Message);
            }
        }




        [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteAnime(int id)
            {
                try
                {
                    await _animeRepo.DeleteAnime(id);
                    return NoContent(); // Повертаємо статус 204 No Content
                }
                catch (Exception ex)
                {
                    // Обробка помилки (наприклад, логування)
                    return StatusCode(500, ex.Message);
                }
            }

      
        [HttpGet("GetAnimeByAuthorId/{authorId}")]
        public async Task<ActionResult<List<about>>> GetAnimeByAuthorId(int authorId)
        {
            try
            {
                var animeList = await _animeRepo.GetAnimeByAuthorId(authorId);
                return Ok(animeList);
            }
            catch (Exception ex)
            {
                // Здійсніть логування помилки, якщо потрібно
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> Anime()
        {
            using var client = new HttpClient();

            var token = await _tokenService.GetToken("WebApiAndIdentity.read");

            client.SetBearerToken(token.AccessToken);

            var result = await client.GetAsync("https://localhost:5445/weatherforcast");

            if (result.IsSuccessStatusCode)
            {
                var model = await result.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<List<about>>(model);

                //return View(data);
            }
            throw new Exception("Unable to ger content");
        }



    }
}




