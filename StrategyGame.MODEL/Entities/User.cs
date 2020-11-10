using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.MODEL.Entities
{
    public class User: IdentityUser
    {
        public List<Kingdom> Kingdoms { get; set; }
    }
}
