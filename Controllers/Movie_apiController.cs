using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HD_Movies.Models;


namespace HD_Movies.Controllers;

[ApiController]
[Route("movie/api")]
public class MovieAPIController : ControllerBase
{
    public MovieAPIController(MoviedbContext context, ILogger<MovieAPIController> logger)
    {

        _context = context;
        _logger = logger;
    }


    private readonly MoviedbContext _context;
    private readonly ILogger<MovieAPIController> _logger;



  [HttpGet]
  // [ProducesResponseType(200)]
  // [ProducesResponseType(401)]
  [Authorize]
  public IEnumerable<Film> Get(int pageSize = 10, [FromQuery] int pageNumber = 1)
  {
    return _context.Films.OrderBy(film => film.FilmId).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
  }


[Authorize]
  [HttpGet("{id:int}")]
  // [ProducesResponseType(200)]
  // [ProducesResponseType(500)]
  // [ProducesResponseType(100)]
  public ActionResult<Film> Get_id(int id)
  {
    if (id == 0) { return BadRequest(); };

    var result = _context.Films.FirstOrDefault((film) => film.FilmId == id);
    if (result == null) { return NotFound(); };

        _logger.LogInformation("\n\n\n\n ----------------- result found  ------- \n" + result.Title + "\n\n\n\n");
       

    return Ok(result);

  }

  
  [HttpPost("")]
  public ActionResult <Film> create(Film film)
  {
    if (film == null) { return BadRequest(); }

    _context.Films.Add(film);
    _context.SaveChanges();
    
    return _context.Films.FirstOrDefault((f) => f.FilmId == film.FilmId)!;
  }

  [HttpPut("{id}")]
  public async Task<IActionResult>  UpdateProduct( int id,Film film){
      var affect = await _context.Films.Where(model => model.FilmId == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Title, film.Title)
                    .SetProperty(m => m.Description, film.Description)
                    .SetProperty(m => m.ReleaseYear, film.ReleaseYear)
                    .SetProperty(m => m.LanguageId, film.LanguageId)
                    .SetProperty(m => m.OriginalLanguageId, film.OriginalLanguageId)
                    .SetProperty(m => m.RentalDuration, film.RentalDuration)
                    .SetProperty(m => m.RentalRate, film.RentalRate)
                    .SetProperty(m => m.Length, film.Length)
                    .SetProperty(m => m.ReplacementCost, film.ReplacementCost)
                    .SetProperty(m => m.Rating, film.Rating)
                    .SetProperty(m => m.SpecialFeatures, film.SpecialFeatures)
                    );
            return affect == 1 ? Ok() : NotFound();
    }




[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
  var film = _context.Films.FirstOrDefault((f) => f.FilmId == id);
  if (film == null) { return NotFound(); }
  _context.Films.Remove(film);
  _context.SaveChanges();
  return Ok();
}
}