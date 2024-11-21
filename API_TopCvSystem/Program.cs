using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using TopCVSystemAPIdotnet.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Thêm CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TopCvDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDB")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")),
    RequestPath = "/images"
});

app.UseHttpsRedirection();

// Thêm middleware CORS
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();