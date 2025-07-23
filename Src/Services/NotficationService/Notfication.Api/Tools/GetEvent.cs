using EventBus.Events;
using MassTransit;
using Microsoft.AspNetCore.Hosting.Server;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Notfication.Api.Tools;

public class GetEvent : IConsumer<OtpEvents>
{
    private readonly ISender _sender;
    public GetEvent(ISender sender )
    {
        _sender = sender;
    }
    public async Task Consume(ConsumeContext<OtpEvents> context)
    {
       await _sender.Send(context.Message.MobileNumber,context.Message.OtpCode);
    }
}










