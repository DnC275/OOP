namespace Backups2.MyExceptions
{
    public class UncreatedBackupException : MyException
    {
        public override string Message => "Backup isn't created";
    }
}