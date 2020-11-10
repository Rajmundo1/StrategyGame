using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.MODEL.Entities
{
    public class User: IdentityUser
    {
        public int Score { get; set; }
        public List<Kingdom> Kingdoms { get; set; }
    }
}
