using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Application.models;

namespace TremendBoard.Application.UseCases.Commands.GetFullName
{
    public class GetUserResponse
    {
        public SomeUserModel User { get; set; }
    }
}
