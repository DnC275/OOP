using System;

namespace Backups2.MyExceptions
{
    public class CreateException : MyException
    {
        public override string Message => "Invalid syntax with \"create\"";
    }

    public class CreateBackupException : CreateException
    {
        public override string Message => "Invalid syntax with \"create backup\"";
    }
    
    public class CreateRestorePointException : CreateException
    {
        public override string Message => "Invalid syntax with \"create RestorePoint\"";
    }
    
    public class CreateDeltaRestorePointException : CreateException
    {
        public override string Message => "Invalid syntax with \"create DeltaRestorePoint\"";
    }
}