using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("1.- Funciones anonimas");
            Console.WriteLine("2.- RX");
            var opc = Console.ReadKey(false);
            switch (opc.Key.ToString())
            {
                case "D1":
                    FuncionesAnonimas();
                    break;
                case "D2":
                    RX();
                    break;
            }
        }

        public static void RX()
        {
            Console.WriteLine("IObservable(escupe stream) - IObserver ");

            var numbers = new MySequenceOfNumbers();
            var observer = new MyConsoleObserver<int>();
            numbers.Subscribe(observer);

            Console.ReadKey(false);
            Console.WriteLine("_RX_:");

            {
                var subject = new Subject<string>();
                WriteSequenceToConsole(subject);
                subject.OnNext("a");
                subject.OnNext("b");
                subject.OnNext("c");
            }


            Console.WriteLine("Hemos conseguido no implementar IObserver");
            Console.ReadKey(false);
            {
                var subject = new Subject<string>();
                subject.OnNext("a");
                WriteSequenceToConsole(subject);
                subject.OnNext("b");
                subject.OnNext("c");
            }

            Console.WriteLine("Cachea elementos ReplaySubject: ");
            Console.ReadKey(false);

            {
                var ReSubject = new ReplaySubject<string>();
                ReSubject.OnNext("a");
                WriteSequenceToConsole(ReSubject);
                ReSubject.OnNext("b");
                ReSubject.OnNext("c");
            }

            Console.WriteLine("Cachea X elementos ReplaySubject: ");
            Console.ReadKey(false);

            {
                var bufferSize = 2;
                var rsubject = new ReplaySubject<string>(bufferSize);
                rsubject.OnNext("a");
                rsubject.OnNext("b");
                rsubject.OnNext("c");
                rsubject.Subscribe(Console.WriteLine);
                rsubject.OnNext("d");
            }

            Console.ReadKey(false);
            Console.WriteLine("Cachea X tiempo ReplaySubject: ");

            {
                var timeWindow = TimeSpan.FromMilliseconds(150);
                var subject = new ReplaySubject<string>(timeWindow);
                subject.OnNext("w");
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                subject.OnNext("x");
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                subject.OnNext("y");
                subject.Subscribe(Console.WriteLine);
                subject.OnNext("z");
            }

            Console.ReadKey(false);
            Console.WriteLine("AsyncSubject<T>");
            { 
                
            }

            Console.ReadKey(false);


        }

        public static void FuncionesAnonimas()
        {
            Func<int, int> miOperacion = mas1;

            miFuncion(1, miOperacion);
            miFuncion(1, mas1);
            miFuncion(1, d => d + 1);

            Console.ReadKey(false);


            var ints = new List<int>() { 1, 2, 3, 4 };

            // map function
            foreach (var v in ints.Select(d => d + 1))
            {
                Console.WriteLine(v);
            }

            Console.ReadKey(false);

            // filter function
            foreach (var v in ints.Where(d => d % 2 == 0))
            {
                Console.WriteLine(v);
            }

            Console.ReadKey(false);

            // reduce function
            Console.WriteLine(ints.Aggregate((a, b) => paraAgregar(a, b)));
            Console.WriteLine(ints.Aggregate((a, b) =>
            {
                if (a > b) return a;
                else return b;
            }));
            System.Console.WriteLine("reduce");
            System.Console.WriteLine(ints.Aggregate((a, b) =>
            {
                Console.WriteLine(String.Format("a:{0} b:{1}", a, b));
                return a + b;
            }));

            Console.ReadKey(false);
        }

        public void ReplaySubjectBufferExample()
        {
            var subject = new Subject<string>();
            subject.OnNext("a");
            WriteSequenceToConsole(subject);
            subject.OnNext("b");
            subject.OnNext("c");

            Console.ReadKey(false);

        }

        static void WriteSequenceToConsole(IObservable<string> sequence)
        {
            //siguiente linua es equivalente
            //sequence.Subscribe(value=>Console.WriteLine(value));
            sequence.Subscribe(Console.WriteLine);
        }

        public static int paraAgregar(int a, int b)
        {
            if (a > b) return a;
            else return b;
        }

        public static void miFuncion(int x, Func<int, int> f)
        {
            System.Console.WriteLine(String.Format("El resultado es: {0}", f(x)));
        }

        private static int mas1(int a)
        {
            return a+1;
        }
    }
}
