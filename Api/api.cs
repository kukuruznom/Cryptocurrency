using Microsoft.AspNetCore.Mvc;
namespace KURS.Api;

public static class Api
{
    private static WebApplication? _app;

    public static void Start(string blockPath, string Port)
    {
        if (_app != null)
            throw new InvalidOperationException("La API ya está en ejecución");

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseUrls($"http://localhost:{Port}");
        _app = builder.Build();
        Console.WriteLine($"API iniciada en http://localhost:{Port}");

        _app.MapPost("/api/transfer", (CrearRequest request) =>
        {

            var resultado = Commands.ApiC(
                "transfer",
                request.ammount,
                request.address,
                request.toAddress,
                blockPath
            );

            return Results.Ok(resultado);
        });
        _app.MapGet("/api/utxo/{address}", (string address) =>
        {
            var resultado = Commands.ApiC(
                "utxo",
                0,
                address,
                "",
                blockPath
            );

            return Results.Ok(resultado);
        });

        // ⚠️ Run bloquea el hilo → usar Task
        Task.Run(() => _app.Run());
    }

    public static async Task Stop()
    {
        if (_app == null)
            return;

        await _app.StopAsync();
        _app = null;
    }
}

public record CrearRequest(int ammount, string address, string toAddress);
