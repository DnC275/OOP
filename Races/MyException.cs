using System;

namespace Races
{
    public class MyException : Exception
    {
        public ITransport transport;

        public MyException(ITransport transport)
        {
            this.transport = transport;
        } 
    }
}