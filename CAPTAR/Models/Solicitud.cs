using System.ComponentModel.DataAnnotations;
using CAPTAR.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CAPTAR.Models
{
    public class Solicitud
    {
        public int id { get; set; }
        [Required]
        public required string NombreCompleto{ get; set; }
        [Required]  
        public required string Direccion { get; set; }
        [Required]
        public required string Email { get; set; }
        public string? Contacto { get; set; }
        [Required]
        public required string Telefono { get; set; }

        public string? Numero { get; set; }
        [Precision(20,2)]
        public decimal Valor { get; set; }
        public int Banos { get; set; }
        public int Dormitorios { get; set; }
        [Required]
        public required int Estacionamiento { get; set; }
        public string Imagen { get; set; } = string.Empty;

        public string Zona { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;

        //public ICollection<Zona>? Zonas { get; set; }
        public ICollection<SoliDetalle>? SoliDetalles { get; set; }


   


    }

    //public static class SolicitudEndpoints
    //{
    //	public static void MapSolicitudEndpoints (this IEndpointRouteBuilder routes)
    //    {
    //        var group = routes.MapGroup("/api/Solicitud").WithTags(nameof(Solicitud));

    //        group.MapGet("/", async (AppDbContext db) =>
    //        {
    //            return await db.Solicitud.ToListAsync();
    //        })
    //        .WithName("GetAllSolicituds")
    //        .WithOpenApi();

    //        group.MapGet("/{id}", async Task<Results<Ok<Solicitud>, NotFound>> (int id, AppDbContext db) =>
    //        {
    //            return await db.Solicitud.AsNoTracking()
    //                .FirstOrDefaultAsync(model => model.id == id)
    //                is Solicitud model
    //                    ? TypedResults.Ok(model)
    //                    : TypedResults.NotFound();
    //        })
    //        .WithName("GetSolicitudById")
    //        .WithOpenApi();

    //        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Solicitud solicitud, AppDbContext db) =>
    //        {
    //            var affected = await db.Solicitud
    //                .Where(model => model.id == id)
    //                .ExecuteUpdateAsync(setters => setters
    //                  .SetProperty(m => m.id, solicitud.id)
    //                  .SetProperty(m => m.NombreCompleto, solicitud.NombreCompleto)
    //                  .SetProperty(m => m.Direccion, solicitud.Direccion)
    //                  .SetProperty(m => m.Email, solicitud.Email)
    //                  .SetProperty(m => m.Contacto, solicitud.Contacto)
    //                  .SetProperty(m => m.Fecha, solicitud.Fecha)
    //                  );
    //            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
    //        })
    //        .WithName("UpdateSolicitud")
    //        .WithOpenApi();

    //        group.MapPost("/", async (Solicitud solicitud, AppDbContext db) =>
    //        {
    //            db.Solicitud.Add(solicitud);
    //            await db.SaveChangesAsync();
    //            return TypedResults.Created($"/api/Solicitud/{solicitud.id}",solicitud);
    //        })
    //        .WithName("CreateSolicitud")
    //        .WithOpenApi();

    //        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, AppDbContext db) =>
    //        {
    //            var affected = await db.Solicitud
    //                .Where(model => model.id == id)
    //                .ExecuteDeleteAsync();
    //            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
    //        })
    //        .WithName("DeleteSolicitud")
    //        .WithOpenApi();
    //    }
    //}
}
