namespace Exceptions
{
    public class NonexistentDirector : MyExceptions
    {
        public override string Message { get => "Nonexistent director"; }
    }
}