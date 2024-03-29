﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjectTracking_WebAPI.Data;
using ProjectTracking_WebAPI.Data.Models;
using ProjectTracking_WebAPI.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// JWT AddAuthentication with AddJwtBearer
builder.Services.AddAuthentication(opt=>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        // Refresh JWT        
        ClockSkew = TimeSpan.Zero
        // end
    };
    // Refresh JWT
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
            }
            return Task.CompletedTask;
        }
    };
//    end
});

builder.Services.AddSingleton<IJWTManagerInterface, JWTManagerService>();

// Creating issues
builder.Services.AddScoped<IUserServiceInterface, UserService>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer("Name=DefaultConnectionString"));

// Refresh Token
builder.Services.AddIdentity<Users, IdentityRole>(
    options => {
    options.Password.RequireUppercase = true; // on production add more secured options
    options.Password.RequireDigit = true;
    options.SignIn.RequireConfirmedEmail = true;
}).AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();
// end


builder.Services.AddTransient<EmployeeService>();
builder.Services.AddTransient<ProjectServices>();
builder.Services.AddTransient<ProjectTaskService>();
builder.Services.AddTransient<UserStoryService>();
//builder.Services.AddTransient<IUserServiceInterface, UserService>();

builder.Services.AddEndpointsApiExplorer();
// JWT was working fine with Postman but not Swagger. Need to add things in SwaggerGen
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectTask-WebAPI with JWT Authentication", Version = "v1", Description = "This is a description" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new String[] { }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


// JWT Authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

DBInitializer.Seed(app);
DBInitializer.SeedUserAndRolesAsync(app).Wait();

app.Run();

