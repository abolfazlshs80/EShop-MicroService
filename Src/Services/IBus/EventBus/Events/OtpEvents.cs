using EventBus.BaseEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events
{
    public class OtpEvents:BaseEventBus
    {
        public string? MobileNumber {  get; set; }  
        public string? OtpCode { get; set; }
    }
}
