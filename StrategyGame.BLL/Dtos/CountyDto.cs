using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Text;

namespace StrategyGame.BLL.Dtos
{
    public class CountyDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string CountyName { get; set; }
        public string Score { get; set; }
    }
}
