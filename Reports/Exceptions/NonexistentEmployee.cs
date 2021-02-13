namespace Exceptions
{
    public class NonexistentEmployee : MyExceptions
    {
        public override string Message { get => "Nonexistent employee"; }
    }
}