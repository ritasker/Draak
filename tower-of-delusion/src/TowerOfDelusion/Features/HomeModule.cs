namespace TowerOfDelusion.Features;

public static class HomeModule
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGet("/api/start", GetStartData);
    }

    private static Task GetStartData(HttpContext context)
    {
        throw new NotImplementedException();
    }
}