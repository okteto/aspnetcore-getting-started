var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();
// The page with the Okteto logo is wwwroot/index.html
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.Run();
