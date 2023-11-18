using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using VillaApi.Data;
using VillaApi.Models.Dto;

namespace VillaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VillaApiController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        return Ok(VillaStore.VillaList);
    }
    
    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        return Ok(villa);
    }
}