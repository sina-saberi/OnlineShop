using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Core;
using Shop.Core.Entites;
using Shop.Core.Entites.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.CQRS.Notfication
{
    public record AddRefresTokenNotfication(string RefreshToken, Guid UserId, int RefreshTokenTimeOut) : INotification;

    public class AddRefresTokenNotficationHandler : INotificationHandler<AddRefresTokenNotfication>
    {
        private readonly OnlineShopDbContext context;
        private readonly IMapper mapper;

        public AddRefresTokenNotficationHandler(OnlineShopDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task Handle(AddRefresTokenNotfication notification, CancellationToken cancellationToken)
        {
            var userRefreshToken = mapper.Map<UserRefreshToken>(notification);
            var currentUserRefreshToken = await context.UserRefreshTokens.FirstOrDefaultAsync(ur => ur.UserId == userRefreshToken.UserId);
            if (currentUserRefreshToken == null)
            {
                await context.AddAsync(userRefreshToken);
            }
            else
            {
                currentUserRefreshToken.Refreshtoken = userRefreshToken.Refreshtoken;
                currentUserRefreshToken.RefreshTokenTieOut = userRefreshToken.RefreshTokenTieOut;
                currentUserRefreshToken.CreateDate = userRefreshToken.CreateDate;
                currentUserRefreshToken.IsValid = true;
            }
            await context.SaveChangesAsync();
        }
    }
}
