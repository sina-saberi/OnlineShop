using MediatR;
using Shop.Core;
using Shop.Core.Entites;
using Shop.Core.Entites.Security;
using Shop.Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.CQRS.UserCommandQuery.Command
{
    public class RegisterCommand : IRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly EncriptionUtility encriptionUtility;
        private readonly OnlineShopDbContext context;

        public RegisterCommandHandler(EncriptionUtility encriptionUtility, Core.OnlineShopDbContext context)
        {
            this.encriptionUtility = encriptionUtility;
            this.context = context;
        }
        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var salt = encriptionUtility.GetNewSalt();
            var hashPassowrd = encriptionUtility.GetSHA256(request.Password, salt);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Password = hashPassowrd,
                PasswordSalt = salt,
                RegisterDate = DateTime.Now,
                UserName = request.UserName
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
