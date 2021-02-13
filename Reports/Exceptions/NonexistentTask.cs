namespace Exceptions
{
    public class NonexistentTask : MyExceptions
    {
        public override string Message { get => "Nonexistent task"; }
    }
}