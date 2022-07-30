using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OctopusWebAPI.Data;
using OctopusWebAPI.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyOctpDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MyDB")));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = false,
                      ValidateAudience = false,

                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"])),
                      ClockSkew = TimeSpan.Zero
                  };
              });

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .SetIsOriginAllowed((host) => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Octopus API V1 30/7/2022");
    });
}


app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
