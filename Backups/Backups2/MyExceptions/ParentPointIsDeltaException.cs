namespace Backups2.MyExceptions
{
    public class ParentPointIsDeltaException : MyException
    { 
        private string pointName;
        public ParentPointIsDeltaException(string pointName) => this.pointName = pointName;
        
        public override string Message => $"\"{pointName}\" is delta restore point. Impossible to create delta restore point by delta restore point";
    }
}