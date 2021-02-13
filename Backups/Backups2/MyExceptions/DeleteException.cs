namespace Backups2.MyExceptions
{
    public class DeleteException : MyException
    {
        public override string Message => "Invalid syntax with \"delete\"";
    }
}