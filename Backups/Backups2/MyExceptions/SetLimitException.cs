using System;

namespace Backups2.MyExceptions
{
    public class SetLimitException : MyException
    {
        public override string Message => "Invalid syntax with \"sl\"";
    }

    public class SetCountLimitException : SetLimitException
    {
        public override string Message => "Invalid syntax with \"sl -c\"";
    }
    
    public class SetSizeLimitException : SetLimitException
    {
        public override string Message => "Invalid syntax with \"sl -s\"";
    } 
    
    public class SetDateLimitException : SetLimitException
    {
        public override string Message => "Invalid syntax with \"sl -d\"";
    }
}