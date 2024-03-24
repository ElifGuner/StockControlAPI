using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using StockControlAPI.Models;
using StockControlAPI.Security;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StockApiDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
});

builder.Services.AddAuthentication("Basic").AddScheme<BasicAuthenticationOption, BasicAuthenticationHandler>("Basic", null);
builder.Services.AddTransient<IAuthenticationHandler, BasicAuthenticationHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    var c = new HttpClient();
    c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "elif:elif1");

    var settings = new SwaggerUIOptions
    {
        RoutePrefix = "swagger",
        DocumentTitle = "Swagger UI",
        HeadContent = @"<script src=""https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js""></script>"
    };

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        options.OAuthClientId("your_client_id");
        options.OAuthClientSecret("your_client_secret");
        options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
    });

}

app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();


/*
 using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using StockControlAPI.Models;
using StockControlAPI.Security;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StockApiDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
});

builder.Services.AddCors(x =>
    x.AddPolicy("AllowAll", x =>
    {
        x.AllowAnyOrigin();
        x.AllowAnyMethod();
        x.AllowAnyHeader();
    })
);
builder.Services.AddAuthentication("Basic").AddScheme<BasicAuthenticationOption, BasicAuthenticationHandler>("Basic", null);
builder.Services.AddTransient<IAuthenticationHandler, BasicAuthenticationHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    var c = new HttpClient();
    c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "elif:elif1");

    var settings = new SwaggerUIOptions
    {
        RoutePrefix = "swagger",
        DocumentTitle = "Swagger UI",
        HeadContent = @"<script src=""https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js""></script>"
    };

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        options.OAuthClientId("your_client_id");
        options.OAuthClientSecret("your_client_secret");
        options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
    });

}

app.UseHttpsRedirection();
/*
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});
*//*
app.UseCors("AllowAll");

app.UseAuthorization();



app.MapControllers();

app.Run();

*/