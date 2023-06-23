using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Commands.UserCommands
{
    public class UserRequestCommand : IRequest<UserRespond>
    {
        [DisplayName("სახელ")]
        public string Name { get; set; }

        [Display(Name="გვარ")]
        public string Lastname { get; set; }
    }
}