using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shop.Application.Interfaces;
using Shop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly OnlineShopDbContext context;
        private readonly IMemoryCache cache;

        public PermissionService(OnlineShopDbContext context, IMemoryCache cache)
        {
            this.context = context;
            this.cache = cache;
        }
        public async Task<bool> CheckPermission(Guid Id, string permissionFlag)
        {
            string perrmisionCachKey = $"permission-{Id}";
            var result = await cache.GetOrCreateAsync(perrmisionCachKey, async f =>
            {
                var roles = await context.UserRoles.Where(x => x.UserId == Id)
                    .Select(x => x.RoleId).ToListAsync();

                var permissionFlags = await context.RolePermissions
                       .Where(x => roles.Contains(x.RoleId))
                       .Select(x => x.Permission.PermissionFlag)
                       .ToListAsync();

                f.SlidingExpiration = TimeSpan.FromSeconds(30);
                return permissionFlags;
            });
            return result.Contains(permissionFlag);
        }
    }
}
