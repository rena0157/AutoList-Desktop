using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCADLI.AutoCADLiExceptions
{
    public class ParseException : Exception
    {
        public ParseException()
        {
            
        }

        public ParseException(string message)
        {
            Message = message;
        }

        public override string Message { get; }
    }
}
