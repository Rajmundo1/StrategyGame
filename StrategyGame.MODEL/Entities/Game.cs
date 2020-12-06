using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.MODEL.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public IEnumerable<User> Users { get; set; }
        public int Round { get; set; }
        public string WoodPictureUrl { get; set; }
        public string MarblePictureUrl { get; set; }
        public string WinePictureUrl { get; set; }
        public string SulfurPictureUrl { get; set; }
        public string GoldPictureUrl { get; set; }
        public string TechnologyPictureUrl { get; set; }
        public string ForceLimitPictureUrl { get; set; }

    }
}
