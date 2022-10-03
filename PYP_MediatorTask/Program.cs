using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PYP_MediatorTask.Context;
using PYP_MediatorTask.Interfaces;
using PYP_MediatorTask.Model;
using PYP_MediatorTask.Services.Token;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Connection String
builder.Services.AddDbContext<PYP_MediatorDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
#endregion

#region Jwt token handler
builder.Services.AddScoped<ITokenHandler, TokenHadler>();
#endregion

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

#region Identity services configure
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;

}).AddDefaultTokenProviders().AddEntityFrameworkStores<PYP_MediatorDbContext>();
builder.Services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromMinutes(60));
#endregion


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Enable Authorization using Swagger (JWT)
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Api" });
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

});
#endregion

var app = builder.Build();

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
