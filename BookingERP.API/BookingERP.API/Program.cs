using BookingERP.API.Helpers;
using BookingERP.Bussiness.Helpers;
using BookingERP.Bussiness.Helpers.Interfaces;
using BookingERP.Bussiness.Interfaces;
using BookingERP.Bussiness.Services;
using BookingERP.Common.Filters;
using BookingERP.Data.Context;
using BookingERP.Data.Entities;
using BookingERP.Data.Interfaces;
using BookingERP.Data.Repositories;
using BookingERP.Data.Seeder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region CORS
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
#endregion

#region SWAGGER OPTIONS
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "BookingERP.API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
#endregion

#region DATABASE
builder.Services.AddDbContext<BookingContext>(
    options => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BookingDB;Integrated Security=True; MultipleActiveResultSets=True"));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(
                 o =>
                 {
                     o.Password.RequireDigit = false;
                     o.Password.RequireLowercase = false;
                     o.Password.RequireUppercase = false;
                     o.Password.RequireNonAlphanumeric = false;
                     o.Password.RequiredLength = 5;

                     o.SignIn.RequireConfirmedEmail = false;
                 })
                .AddEntityFrameworkStores<BookingContext>()
                .AddDefaultTokenProviders();
#endregion

#region SERVICES REGISTER
builder.Services.AddScoped<IJwtHelper, JwtHelper>();

builder.Services.AddTransient<Seeder>();

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IGuestRepository, GuestRepository>();
builder.Services.AddTransient<IManagerRepostirory, ManagerRepository>();
builder.Services.AddTransient<IHotelRepository, HotelRepository>();
builder.Services.AddTransient<IDiscountRepository, DiscountRepository>();
builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();
builder.Services.AddTransient<IPaymentDetailsRepository, PaymentDetailsRepository>();
builder.Services.AddTransient<IRoomRepository, RoomRepository>();
builder.Services.AddTransient<IReservationRepository, ReservationRepository>();


builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IGuestService, GuestService>();
builder.Services.AddTransient<IManagerService, ManagerService>();
builder.Services.AddTransient<IHotelService, HotelService>();
builder.Services.AddTransient<IDiscountService, DiscountService>();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IPaymentDetailsService, PaymentDetailsService>();
builder.Services.AddTransient<IRoomService, RoomService>();
builder.Services.AddTransient<IReservationService, ReservationService>();


#endregion

#region AUTHENTICATION
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JWTSettings:validIssuer"],
        ValidAudience = builder.Configuration["JWTSettings:validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:securityKey"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();
#endregion



builder.Services.AddAutoMapper(typeof(SetupAutoMapper));

builder.Services.AddControllers(options => { options.Filters.Add<ExceptionFilter>(); });

var app = builder.Build();

var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var seeder = scope.ServiceProvider.GetService<Seeder>();
    seeder.SeedAsync().Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
