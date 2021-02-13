using System;

namespace Exceptions
{
    public abstract class MyExceptions : Exception
    {
        public override string Message { get; }
    }
}