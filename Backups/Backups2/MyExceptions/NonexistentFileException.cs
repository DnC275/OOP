namespace Backups2.MyExceptions
{
    public class NonexistentFileException : MyException
    {
        private string path;
        public override string Message => $"File {path} doesn't exist";

        public NonexistentFileException(string path)
        {
            this.path = path;
        }
    }
}