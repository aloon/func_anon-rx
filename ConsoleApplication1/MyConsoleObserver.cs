using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class MyConsoleObserver<T> : IObserver<T>
    {
        public void OnCompleted()
        {
            Console.WriteLine("Sequence terminated");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Sequence faulted with {0}", error);
        }

        public void OnNext(T value)
        {
            Console.WriteLine("Received value {0}", value);
        }
    }
}
