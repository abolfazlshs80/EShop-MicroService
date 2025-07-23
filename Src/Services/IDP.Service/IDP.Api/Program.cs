using Asp.Versioning;
using Auth;
using AutoMapper;
using IDP.Application.Handler.Command.Auth;
using IDP.Application.Handler.Command.User;
using IDP.Application.Handler.Query.Auth;
using IDP.Application.Helper;
using IDP.Domain.IRepository.Base;
using IDP.Domain.IRepository.Command;
using IDP.Domain.IRepository.Query;
using IDP.Infra.Data;
using IDP.Infra.Repositories.Command;
using IDP.Infra.Repositories.Query;
using IDP.Infra.Repository.Command;
using IDP.Infra.Repository.Command.Base;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfiles());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(opition =>
{
    opition.Configuration = builder.Configuration.GetValue<string>("CashSetting:RedisUrl");
    opition.InstanceName = "SampleInstance";
});
builder.Services.AddMediatR(typeof(UserHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(LoginAuthHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(AuthLoginCommandHandler).GetTypeInfo().Assembly);
//builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOtpRedisRepository, OtpRedisRepository>();
builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();
builder.Services.AddScoped(typeof(IBaseCommonRepository<>), typeof(CommandRepository<>));
builder.Services.AddScoped<IOtpRedisRepository, OtpRedisRepository>();

#region messageMq   

//builder.Services.AddCap(options =>
//{
//    options.UseEntityFramework<ShopCommandDbContext>();
//    options.UseDashboard(path => path.PathMatch = "/cap");
//    options.UseRabbitMQ(options =>
//    {
//        options.ConnectionFactoryOptions = options =>
//        {
//            options.Ssl.Enabled = false;
//            options.HostName = builder.Configuration["Cap:HostName"];
//            options.UserName = builder.Configuration["Cap:UserName"];
//            options.Password = builder.Configuration["Cap:Password"];
//            options.Port = int.Parse( builder.Configuration["Cap:Port"]);
//        };

//    });
//    options.FailedRetryCount = int.Parse( builder.Configuration["Cap:FailedRetryCount"]);
//    options.FailedRetryInterval = int.Parse( builder.Configuration["Cap:FailedRetryInterval"]);


//});
builder.Services.AddMassTransit(busConfig =>
{


    busConfig.SetKebabCaseEndpointNameFormatter();
    busConfig.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(builder.Configuration.GetValue<string>("Rabbit:Host")), h =>
        {
            h.Username(builder.Configuration.GetValue<string>("Rabbit:UserNmae"));
            h.Password(builder.Configuration.GetValue<string>("Rabbit:Password"));

        });

        cfg.UseMessageRetry(r => r.Exponential(10, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(5)));

        cfg.ConfigureEndpoints(context);
    });
});
#endregion


Extensions.AddJwt(builder.Services, builder.Configuration);
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
})

.AddMvc() // This is needed for controllers
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddTransient<ShopCommandDbContext>();
builder.Services.AddTransient<ShopQueryDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
