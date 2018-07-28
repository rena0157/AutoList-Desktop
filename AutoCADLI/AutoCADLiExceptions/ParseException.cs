// AutoCADLI
// ParseException.cs
// 
// ============================================================
// 
// Created: 2018-07-24
// Last Updated: 2018-07-28-3:34 PM
// By: Adam Renaud
// 
// ============================================================
// 
// Purpose: Custom Exceptions that are included in the AutoCADLI Project

using System;

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