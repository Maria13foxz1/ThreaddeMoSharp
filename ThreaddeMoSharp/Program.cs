using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.NetworkInformation;

namespace ThreaddeMoSharp
{
    internal class Program
    {
        public bool canStop = false;
        public bool CanStop { get => canStop; }
        static void Main(string[] args)
        {
            /*int numThreads = 3; // Кількість потоків для створення
            int[] steps = { 1, 2, 3 }; // Кроки для кожного потоку
            int[] threadWorkingTimes = { 10000, 12000, 9000 }; // Час в мілісекундах для кожного потоку
            // Запускаємо керуючий потік
            ControlThread controlThread = new ControlThread(numThreads);
            Thread controller = new Thread(controlThread.Start);
            controller.Start();
            // Запускаємо робочі потоки
            for (int i = 0; i < numThreads; i++)
            {
                WorkerThread workerThread = new WorkerThread(i + 1, steps[i], threadWorkingTimes[i], controlThread);
                Thread worker = new Thread(workerThread.Start);
                worker.Start();
            }*/

            (new Program()).Start();
            Console.ReadKey();
        }
        void Start()
        {
            //Створюємо три потока
            (new Thread(() => Calcuator(11, 1))).Start();
            (new Thread(() => Calcuator(12, 2))).Start();
            (new Thread(() => Calcuator(13, 3))).Start();
            //Керуючий потік
            Thread controller = new Thread(Stoper);
            controller.Start();
        }
        void Calcuator(int id, int step)
        {
            long sum = 0;
            int count = 0;
            do
            {
                sum += step; // Додаємо крок до суми
                count++;
            } while (!canStop);

            Console.WriteLine($"Thread {id}: Sum = {sum}, Count = {count}");
        }
        public void Stoper()
        {
            Thread.Sleep(5 * 1000);
            canStop = true;
        }
    }
}

