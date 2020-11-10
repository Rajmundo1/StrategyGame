using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.MODEL.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
