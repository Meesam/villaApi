using VillaApi.Models.Dto;

namespace VillaApi.Data;

public static class VillaStore
{
    public static List<VillaDTO> VillaList = new List<VillaDTO> {
        new VillaDTO{Id = 1, Name = "Pool View", Sqft = 400, Occupancy = 2},
        new VillaDTO{Id = 2, Name = "Beach View", Sqft = 500, Occupancy = 5}
    };
}