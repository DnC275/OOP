using System;

namespace Backups2.MyExceptions
{
    public class SetLimitModeException : MyException
    {
        public override string Message => "Invalid syntax with \"slm\"";
    }
}