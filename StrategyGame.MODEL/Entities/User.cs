using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace StrategyGame.MODEL.Entities
{
    public class User: IdentityUser
    {
        [NotMapped]
        public int Score => Kingdom.GlobalScore;
        public int ScoreboardPlace { get; set; }
        [ForeignKey("Kingdom")]
        public Guid KingdomId { get; set; }
        public Kingdom Kingdom { get; set; }

        [ForeignKey("Game")]
        public Guid GameId { get; set; }

        public string RefreshToken { get; set; }
    }
}
