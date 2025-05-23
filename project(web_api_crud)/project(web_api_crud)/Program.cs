using Microsoft.EntityFrameworkCore;
using project_web_api_crud_.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigins",
//        builder =>
//        {
//            builder.WithOrigins("https://localhost:7075/") // Add your MVC URL
//                   .AllowAnyHeader()
//                   .AllowAnyMethod();
//        });
//});


builder.Services.AddControllers();
builder.Services.AddDbContext<CrudApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
