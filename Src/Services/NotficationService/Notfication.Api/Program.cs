using Carter;
using MassTransit;
using Notfication.Api.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISender,MessageSender>();

builder.Services.AddMassTransit(busConfig =>
{


    //  busConfig.SetKebabCaseEndpointNameFormatter();
    busConfig.UsingRabbitMq((context, cfg) =>
    {
        busConfig.AddConsumer<GetEvent>();
        cfg.Host(new Uri(builder.Configuration["Rabbit:Host"]), h =>
        {
            h.Username(builder.Configuration["Rabbit:UserNmae"]);
            h.Password(builder.Configuration["Rabbit:Password"]);

        });
        cfg.ReceiveEndpoint("my-message-q", e =>
        {
            e.ConfigureConsumer<GetEvent>(context);
        });
        //cfg.UseMessageRetry(r => r.Exponential(10, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(5)));

        //cfg.ConfigureEndpoints(context);
    });
});

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
app.MapCarter();

app.Run();
