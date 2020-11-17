using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace StrategyGame.MODEL.Entities
{
    public class User: IdentityUser
    {
        [NotMapped]
        public int Score => Kingdom.Counties.Sum(county => county.Score);
        public int ScoreboardPlace { get; set; }
        public Kingdom Kingdom { get; set; }

        [ForeignKey("Game")]
        public Guid GameId { get; set; }
    }
}
