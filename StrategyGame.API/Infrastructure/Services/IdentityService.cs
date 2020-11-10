using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StrategyGame.API.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpContext context;
        private readonly UserManager<User> userManager;

        public IdentityService(IHttpContextAccessor httpContextAccessor,
                                UserManager<User> userManager)
        {
            this.context = httpContextAccessor.HttpContext;
            this.userManager = userManager;
        }

        public async Task<User> GetCurrentUser()
        {
            var id = context.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return await userManager.FindByIdAsync(id);
        }

        public async Task<string> GetCurrentUserId()
        {
            return await Task.Run(() => context.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
