using Microsoft.AspNetCore.Http;
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
        private readonly IUserRepository userRepository;

        public IdentityService(IHttpContextAccessor httpContextAccessor,
                                IUserRepository userRepository)
        {
            this.context = httpContextAccessor.HttpContext;
            this.userRepository = userRepository;
        }

        public async Task<User> GetCurrentUser()
        {
            var id = context.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return await userRepository.FindAsync(id);
        }

        public async Task<string> GetCurrentUserId()
        {
            return await Task.Run(() => context.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
