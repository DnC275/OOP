namespace Backups2.MyExceptions
{
    public class SomePointsNotDeletedException : MyException
    {
        public override string Message => $"Some points were not deleted because the Delta point can't be without a parent point";
    }
}