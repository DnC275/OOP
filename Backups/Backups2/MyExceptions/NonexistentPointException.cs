namespace Backups2.MyExceptions
{
    public class NonexistentPointException : MyException
    {
        private string path;
        public override string Message => $"Point {path} doesn't exist";

        public NonexistentPointException(string path)
        {
            this.path = path;
        }
    }
}