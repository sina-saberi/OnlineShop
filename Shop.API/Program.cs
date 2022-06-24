using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Shop.API;
using Shop.Application;
using Shop.Core;
using Shop.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiDi(builder.Configuration);
builder.Services.AddApplicationDi();
builder.Services.AddInfrastructureDi();


//register context
builder.Services.AddDbContext<OnlineShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineShop"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.WebRootPath, "Media")),
    RequestPath = "/Media"
});

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//call DB Seed Data

app.Run();
