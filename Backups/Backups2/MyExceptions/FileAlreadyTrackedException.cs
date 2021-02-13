namespace Backups2.MyExceptions
{
    public class FileAlreadyTrackedException : MyException
    {
        private string path;
        
        public FileAlreadyTrackedException(string path)
        {
            this.path = path;
        }
        
        public override string Message => $"Info! File {path} is already being tracked";
    }
}