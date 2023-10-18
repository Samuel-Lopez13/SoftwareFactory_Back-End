using Core.Infraestructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Core.Infraestructure;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPresentationServices(builder.Configuration);
builder.Services.AddSecurity(builder.Configuration);

builder.Services.AddDbContext<FactoryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionMaestra"));
});

var app = builder.Build();

/*
using(var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<FactoryContext>();
    dataContext.Database.Migrate();
}
*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI( c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseAuthorization();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();