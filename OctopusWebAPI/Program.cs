using Microsoft.EntityFrameworkCore;
using OctopusWebAPI.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

 builder.Services.AddDbContext<MyOctpDBContext>(options => 
 options.UseSqlServer(builder.Configuration.GetConnectionString("MyDB")));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

 
app.UseAuthorization();

app.MapControllers();

app.Run();
