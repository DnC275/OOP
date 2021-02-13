namespace Backups2.MyExceptions
{
    public class FileAlreadyNotTrackedException : MyException
    {
        private string path;
        
        public FileAlreadyNotTrackedException(string path)
        {
            this.path = path;
        }
        
        public override string Message => $"Info! File {path} is already being tracked";
    }
}