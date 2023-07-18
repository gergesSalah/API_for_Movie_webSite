using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using WebApplication4.DTO;
using WebApplication4.Models;
using WebApplication4.Services;

namespace WebApplication4.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _GenreService;
        public GenresController(IGenreService genreService)
        {
            _GenreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var genres =  await _GenreService.GetAll();
            if (genres == null)
                return Ok("not good ");
         else
                return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreDto dto)
        {

            var genre = new Genre
            {
                Name = dto.Name
            };

            _GenreService.Add(genre);

            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateAsync(byte id, [FromBody] GenreDto dto)
        {
            var genere = await _GenreService.GetById(id);

            if (genere == null)
                return NotFound($"No genre was found with ID: {id}");

            genere.Name = dto.Name;

            _GenreService.Update(genere);
            return Ok(genere);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAscync(byte id)
        {
            var genre = await _GenreService.GetById(id);

            if (genre == null)
                return NotFound($"No genre was found with ID: {id}");

            _GenreService.Delete(genre);

            return Ok(genre);
        }


    }
}
