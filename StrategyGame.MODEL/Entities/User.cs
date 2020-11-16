using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StrategyGame.MODEL.Entities
{
    public class User: IdentityUser
    {
        public int Score { get; set; }
        public int ScoreboardPlace { get; set; }
        public Kingdom Kingdom { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
    }
}
