using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shop.Application.CQRS.Notfication;
using Shop.Core;
using Shop.Infrastructure.Model;
using Shop.Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.CQRS.UserCommandQuery.Command
{
    public class LoginCommand : IRequest<LoginCommandResponse>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
    public class LoginCommandResponse
    {
        public string UserName { get; set; }
        public string Toke { get; set; }
        public string ExpireTime { get; set; }
        public string RefreshToken { get; set; }
    }
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly OnlineShopDbContext context;
        private readonly EncriptionUtility encriptionUtility;
        private readonly IMediator mediator;
        private readonly Configs configs;

        public LoginCommandHandler(
            OnlineShopDbContext context,
            EncriptionUtility encriptionUtility,
            IMediator mediator,
            IOptions<Configs> options)
        {
            this.context = context;
            this.encriptionUtility = encriptionUtility;
            this.mediator = mediator;
            this.configs = options.Value;
        }
        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.UserName == request.UserName);
            if (user == null) throw new ArgumentException();

            var hashPassword = encriptionUtility.GetSHA256(request.Password, user.PasswordSalt);
            if (user.Password != hashPassword) throw new Exception();

            var token = encriptionUtility.GetNewToken(user.Id);
            var refreshToken = encriptionUtility.GetNewRefreshToken();

            await mediator.Publish(new AddRefresTokenNotfication(refreshToken, user.Id, configs.RefreshTokenTimeout));
            return new()
            {
                UserName = user.UserName, 
                Toke = token,
                RefreshToken = refreshToken
            };
        }
    }
}
