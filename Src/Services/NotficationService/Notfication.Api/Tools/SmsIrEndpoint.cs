using Carter;

namespace Notfication.Api.Tools
{
    public class SmsIrEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/v1/sendsmsIr", async (SmsRequest request, ISender sender) =>
            {
                var result = await sender.Send(request.from,request.message);
                return Results.Ok(true);
            })
            .WithName("sendsmsIr")
            .WithTags("sendsmsIr");
        }
    }
    public record SmsRequest(string from, string message);
}
