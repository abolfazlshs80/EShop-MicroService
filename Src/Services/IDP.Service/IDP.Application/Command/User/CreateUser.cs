using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Command.User
{
    public record CreateUser:IRequest<bool>
    {
        [Required]
        [MinLength(3)]
        [DefaultValue("System")]
        public required string FullName { get; set; }
        public required string CodeNumber { get; set; }
    }
}
