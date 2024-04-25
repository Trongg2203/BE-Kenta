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
builder.Services.AddScoped<IProducerRepository, ProducerRepository>();
builder.Services.AddScoped<IImportGoodRepository, ImportGoodRepository>();
builder.Services.AddScoped<IImportGoodsinforRepository, ImportGoodsinforRepository>();
builder.Services.AddScoped<IGoodstypeRepository, GoodstypeRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IGoodsinforRepository, GoodsinforRepository>();
builder.Services.AddScoped<IGoodsRepository, GoodsRepository>();
builder.Services.AddScoped<IPictureRepository, PictureRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<ISendEmailRepository, SendEmailServices>();




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
