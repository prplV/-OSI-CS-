using System;
using System.Threading;

//4 variant
namespace OSI
{
    class Program
    {
        public static Semaphore sem;
        public static int semCounter = 0;
        public static int counter = 0;
        public static int _padding = 0;

        static void Main()
        {
            Console.WriteLine("\nI");
            Thread threadA = new Thread(tempFunc);
            sem = new Semaphore(0, 1);
            threadA.Start("A");
            Thread.Sleep(500);
            sem.Release(1);
            Thread.Sleep(3000);
            sem.Close();
            threadA.Abort();
            Console.WriteLine();
            Console.WriteLine("\nII");

            Thread threadB = new Thread(tempFunc);
            Thread threadC = new Thread(tempFunc);
            Thread threadJ = new Thread(tempFunc);
            Thread threadI = new Thread(tempFunc);
            sem = new Semaphore(0, 4);
            threadB.Start("B");
            threadC.Start("C");
            threadJ.Start("J");
            threadI.Start("I");
            Thread.Sleep(1000);
            threadJ.Suspend();
            threadI.Suspend();
            sem.Release(4);
            Thread.Sleep(3000);
            sem.Close();
            threadB.Abort();
            threadC.Abort();
            Console.WriteLine();

            Console.WriteLine("\nIII");
            semCounter = 0;
            Thread threadD = new Thread(tempFunc);
            Thread threadE = new Thread(tempFunc);
            Thread threadF = new Thread(tempFunc);
            sem = new Semaphore(0, 4);
            threadD.Start("D");
            threadE.Start("E");
            threadF.Start("F");
            threadJ.Resume();
            Thread.Sleep(500);
            sem.Release(4);
            Thread.Sleep(3000);
            sem.Close();
            threadD.Abort();
            threadE.Abort();
            threadF.Abort();
            threadJ.Abort();
            Console.WriteLine();

            Console.WriteLine("\nIV");
            semCounter = 0;
            Thread threadG = new Thread(tempFunc);
            Thread threadH = new Thread(tempFunc);
            sem = new Semaphore(0, 3);
            threadG.Start("G");
            threadH.Start("H");
            threadI.Resume();
            Thread.Sleep(500);
            sem.Release(3);
            Thread.Sleep(3000);
            sem.Close();
            threadG.Abort();
            threadH.Abort();
            threadI.Abort();
            Console.WriteLine();

            Console.WriteLine("\nV");
            semCounter = 0;
            Thread threadK = new Thread(tempFunc);
            sem = new Semaphore(0, 1);
            threadK.Start("K");
            Thread.Sleep(500);
            sem.Release(1);
            Thread.Sleep(3000);
            sem.Close();
            threadK.Interrupt();
            Console.ReadKey();
        }
        static public void tempFunc(object sym)
        {
            Console.WriteLine($"   {++counter}. Thread {sym} is working and waiting for Semaphore!");
            sem.WaitOne();
            int padding = Interlocked.Add(ref _padding, 100);
            Thread.Sleep(1000 + padding);
            Console.WriteLine($"   ***   Thread {sym} in a Semaphore.");
            Console.WriteLine($"   ***   Semaphore variable is {++semCounter}. Thread {sym} out of a Semaphore.");
        }
    }
}