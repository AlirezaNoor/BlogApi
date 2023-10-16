using BLG.Infrastructure.Context;
using BLG.Infrastructure.customRepository;
using BLG.Services.Customrepository;
using BLG.Services.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Blog"));
});
builder.Services.AddDbContext<UserContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Blog"));
});
builder.Services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Plus")
    .AddEntityFrameworkStores<UserContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<Iunitofwork, unitofwork>();
builder.Services.AddScoped<IPostblogReposetory, PostblogReposetory>();
builder.Services.AddScoped<IUploadimageReposetory, UploadimageReposetory>();
builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequiredLength = 6;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        AuthenticationType = "JWt",
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer =  builder.Configuration["jwt:Issure"],
        ValidAudience = builder.Configuration["jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]))
    };
});
var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseCors(opt =>
{
    opt.AllowAnyHeader();
    opt.AllowAnyMethod();
    opt.AllowAnyOrigin();
});
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "images")),
    RequestPath = "/images"
});
app.MapControllers();

app.Run();