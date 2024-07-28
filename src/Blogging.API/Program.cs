
using Blogging.Infrastructure.Extensions;
using Blogging.API.Extensions;
using Blogging.API.Middlewares;
using Blogging.Application.Extensions;
using Blogging.Infrastructure.Seeders;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrasturcture(builder.Configuration);



var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();


var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IBlogSeeder>();
await seeder.Seed();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();





app.Run();
