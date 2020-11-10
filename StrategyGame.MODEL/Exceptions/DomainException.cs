using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.MODEL.Exceptions
{
    public class DomainException: Exception
    {
        public DomainException(string message): base(message)
        {

        }
    }
}
