using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StrategyGame.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.DAL
{
    public class ApplicationUserStore: UserStore<User>
    {
        public ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber describer = null): base(context, describer)
        {
            AutoSaveChanges = false;
        }
    }
}
