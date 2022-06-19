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
    public record GenerateNewToken(string Token, string RefreshToken) : IRequest<GenerateNewToken>;

    public class GenerateNewTokenCommandHandler : IRequestHandler<GenerateNewToken, GenerateNewToken>
    {
        private readonly OnlineShopDbContext context;
        private readonly EncriptionUtility encriptionUtility;
        private readonly IMediator mediator;
        private readonly Configs configs;
        public GenerateNewTokenCommandHandler(OnlineShopDbContext context, EncriptionUtility encriptionUtility, IOptions<Configs> options, IMediator mediator)
        {
            this.context = context;
            this.encriptionUtility = encriptionUtility;
            this.mediator = mediator;
            configs = options.Value;
        }
        public async Task<GenerateNewToken> Handle(GenerateNewToken request, CancellationToken cancellationToken)
        {
            var userRefreshToken = await context.UserRefreshTokens.SingleOrDefaultAsync(ur => ur.Refreshtoken == request.RefreshToken);
            if (userRefreshToken == null) throw new NullReferenceException();

            var token = encriptionUtility.GetNewToken(userRefreshToken.UserId);
            var refreshToken = encriptionUtility.GetNewRefreshToken();

            await mediator.Publish(new AddRefresTokenNotfication(refreshToken, userRefreshToken.UserId, configs.RefreshTokenTimeout));
            return new(token, refreshToken);
        }
    }
}
