using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;
using System.Reflection;
using TomasekRestApi.BL.Data;
using TomasekRestApi.Model.Cryptography;
using TomasekRestApi.Model.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionStringEncrypted = builder.Configuration.GetConnectionString("WebApiDatabase");
var connectionStringDecrypted = CryptoHelper.DecryptString(connectionStringEncrypted);
builder.Services.AddDbContext<AlzaTestDBContext>(x => x.UseSqlServer(connectionStringDecrypted));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    //options.ApiVersionReader = new MediaTypeApiVersionReader("v");
});
builder.Services.AddTransient<DataSeeder>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

string XmlCommentsFilePath()
{
    
        var basePath = PlatformServices.Default.Application.ApplicationBasePath;
        var fileName = typeof(Program).GetTypeInfo().Assembly.GetName().Name + ".xml";
        return Path.Combine(basePath, fileName);
    
}
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options => {
    options.IncludeXmlComments(XmlCommentsFilePath());
});

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

//Seed Data
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DataSeeder>();
        service.Seed();
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
