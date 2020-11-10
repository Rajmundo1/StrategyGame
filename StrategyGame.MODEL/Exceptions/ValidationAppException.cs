using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.MODEL.Exceptions
{
    public class ValidationAppException: Exception
    {
        public IEnumerable<string> Errors { get; set; }

        public ValidationAppException(string message, IEnumerable<string> errors = null): base(message)
        {
            if(errors == null)
            {
                errors = new string[] { };
            }
            Errors = errors;
        }

    }
}
