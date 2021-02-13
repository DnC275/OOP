using System;

namespace Backups2.MyExceptions
{
    public class AddException : Exception
    {
        public override string Message => "Invalid syntax with \"add\"";
    }
}