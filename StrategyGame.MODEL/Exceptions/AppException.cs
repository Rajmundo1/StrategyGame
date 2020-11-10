using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.MODEL.Exceptions
{
    public class AppException: Exception
    {
        public int? ErrorCode { get; set; }

        public AppException(string message, int? errorCode = null): base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
