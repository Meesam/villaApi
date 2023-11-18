using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using VillaApi.Models;

namespace VillaApi.Controllers;

[Route("api/VillaApi")]
[ApiController]
public class VillaApiController : ControllerBase
{
    [HttpGet]
    public IEnumerable<Villa> GetVillas()
    {
        return new List<Villa> {
            new Villa{Id = 1, Name = "Pool View"},
            new Villa{Id = 2, Name = "Beach View"}
        };
    }
}