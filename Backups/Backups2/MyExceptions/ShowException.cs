using System;

namespace Backups2.MyExceptions
{
    public class ShowException : MyException
    {
        public override string Message => "Invalid syntax with \"show\"";
    }
}