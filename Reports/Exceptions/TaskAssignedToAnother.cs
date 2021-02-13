namespace Exceptions
{
    public class TaskAssignedToAnother : MyExceptions
    {
        public override string Message { get => "The task is assigned to another employee"; }
    }
}