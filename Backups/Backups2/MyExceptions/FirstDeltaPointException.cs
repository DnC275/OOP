namespace Backups2.MyExceptions
{
    public class FirstDeltaPointException : MyException
    {
        public override string Message => $"Error! Delta restore point can't be first point in backup";
    }
}