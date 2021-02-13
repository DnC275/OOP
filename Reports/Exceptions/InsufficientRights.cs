namespace Exceptions
{
    public class InsufficientRights : MyExceptions
    {
        public override string Message { get => "Insufficient rights to edit the Sprint Report"; }
    }
}