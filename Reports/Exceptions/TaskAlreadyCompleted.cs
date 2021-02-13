namespace Exceptions
{
    public class TaskAlreadyCompleted : MyExceptions
    {
        public override string Message { get => "Task is already completed"; }
    }
}