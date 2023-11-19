using System.Data.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VillaApi.Data;
using VillaApi.Models;
using VillaApi.Models.Dto;

namespace VillaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VillaApiController : ControllerBase
{
    private readonly ILogger<VillaApiController> _logger;
    private readonly ApplicationDbContext _db;
    public VillaApiController(ILogger<VillaApiController> _logger, ApplicationDbContext _db)
    {
        this._logger = _logger;
        this._db = _db;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        _logger.LogInformation("Getting all Villa");
        return Ok(_db.Villas.ToList());
    }
    
    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if (id == 0)
        {
            _logger.LogError("Id is " + id);
            return BadRequest();
        }
        var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        return Ok(villa);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VillaDTO> AddVilla([FromBody] VillaDTO villaDto)
    {
        if (_db.Villas.FirstOrDefault(u => u.Name == villaDto.Name) != null)
        {
            ModelState.AddModelError("customError","Villa already exists");
            return BadRequest(ModelState);
        }
        if (villaDto == null)
        {
            return BadRequest();
        }
        if (villaDto.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        Villa villaModel = new()
        {
          Name = villaDto.Name,
          Amenity = villaDto.Amenity,
          Rate = villaDto.Rate,
          Sqft = villaDto.Sqft,
          Occupancy = villaDto.Occupancy,
          Details = villaDto.Details,
          ImageUrl = villaDto.ImageUrl
        };
        _db.Villas.Add(villaModel);
        _db.SaveChanges();
        return Ok(villaDto);
    }

    [HttpDelete("id")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        _db.Villas.Remove(villa);
        _db.SaveChanges();
        return NoContent();
    }

    [HttpPut("id")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDto)
    {
        if (id == null || id != villaDto.Id)
        {
            return BadRequest();
        }
        var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        Villa villaModel = new()
        {
            Name = villaDto.Name,
            Amenity = villaDto.Amenity,
            Rate = villaDto.Rate,
            Sqft = villaDto.Sqft,
            Occupancy = villaDto.Occupancy,
            Details = villaDto.Details,
            ImageUrl = villaDto.ImageUrl
        };
        _db.Villas.Update(villaModel);
        _db.SaveChanges();
        return NoContent();
    }

    [HttpPatch("id")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchVilla)
    {
        if (patchVilla == null || id == 0)
        {
            return BadRequest();
        }
        var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
        VillaDTO villaDtoModel = new()
        {
            Name = villa.Name,
            Amenity = villa.Amenity,
            Rate = villa.Rate,
            Sqft = villa.Sqft,
            Occupancy = villa.Occupancy,
            Details = villa.Details,
            ImageUrl = villa.ImageUrl
        };
        if (villa == null)
        {
            return NotFound();
        }
        patchVilla.ApplyTo(villaDtoModel, ModelState);
        Villa villaModel = new()
        {
            Name = villaDtoModel.Name,
            Amenity = villaDtoModel.Amenity,
            Rate = villaDtoModel.Rate,
            Sqft = villaDtoModel.Sqft,
            Occupancy = villaDtoModel.Occupancy,
            Details = villaDtoModel.Details,
            ImageUrl = villaDtoModel.ImageUrl
        };
        _db.Villas.Update(villaModel);
        _db.SaveChanges();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return NoContent();
    }

}