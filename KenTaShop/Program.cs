using KenTaShop.Data;
using KenTaShop.Services;
using Microsoft.EntityFrameworkCore;
using static KenTaShop.Services.IBillInforRepository;
using static KenTaShop.Services.IBillRepository;
using static KenTaShop.Services.IUserRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ket noi vs SQL
builder.Services.AddDbContext<ClothesShopManagementContext>(o 
    => o.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"))
    );

builder.Services.AddScoped<IUserTypeRepositoty,  UserTypeRepositoty>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IBillInforRepository, BillInforRepository>();


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
