using Microsoft.OpenApi.Models;
using SatinAlmaTalep.Data;
using SatinAlmaTalep.Service;
using SatinAlmaTalep.Service.Services.Abstraction;
using SatinAlmaTalep.Service.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
var env = builder.Environment;
builder.Configuration.SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json", optional: false);

builder.Services.AddDataReg(builder.Configuration);
builder.Services.AddRegService(builder.Configuration);
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRequestService, RequestService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SatinAlmaTalep", Version = "v1", Description = "SatinAlmaTalep swagger client." });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "'Bearer' yazýp bosluk býraktýktan sonra Token'ý Girebilirsiniz \r\n\r\n Örnegin: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }

    });
});



var app = builder.Build();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost"));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
