using Microsoft.EntityFrameworkCore;
using VillaApi.Models;

namespace VillaApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
    }
    
    public DbSet<Villa> Villas { get; set; }
}