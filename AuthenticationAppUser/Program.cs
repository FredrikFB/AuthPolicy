using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using AuthenticationAppUser.Context;
using Microsoft.EntityFrameworkCore;
using AuthenticationAppUser.Models;
using AuthenticationAppUser.Repository;
using AuthenticationAppUser;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthAppUser")));

builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>();



builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<SchoolRepository>();


builder.Services.AddAuthentication();
builder.Services.AddAuthorization(AuthorizationPolicyConfig.ConfigurePolicies);
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RequireAdminRole", policy =>
//    {
//        policy.RequireAssertion(context =>
//        {
//            //var schoolId = context.User.FindFirstValue("");
//            context.User.IsInRole("admin8");

//            return true;
//        });
//    });
//});
builder.Services.AddAuthorizationBuilder();
builder.Services.AddAutoMapper(typeof(MappingConfig));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{


    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
             "Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n" +
             "Example: \"Bearer 1234abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

var app = builder.Build();

app.MapIdentityApi<AppUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
