using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreaddeMoSharp
{
    class ControlThread
    {
        private int numThreads; 
        private bool[] threadFinished; 
        private bool stop; 

        // Конструктор класу
        public ControlThread(int numThreads)
        {
            this.numThreads = numThreads;
            threadFinished = new bool[numThreads];
        }

        // Метод для початку роботи керуючого потоку
        public void Start()
        {
            // Чекаємо, поки всі потоки завершаться
            while (true)
            {
                bool allFinished = true;
                for (int i = 0; i < numThreads; i++)
                {
                    if (!threadFinished[i])
                    {
                        allFinished = false;
                        break;
                    }
                }

                if (allFinished)
                {
                    stop = true;
                    break;
                }
            }

            Console.WriteLine("All threads finished their work.");
        }

        // Метод для встановлення прапорця завершення для конкретного потоку
        public void SetThreadFinished(int threadIndex)
        {
            threadFinished[threadIndex] = true;
        }

        // Метод для перевірки, чи потрібно зупиняти роботу
        public bool ShouldStop()
        {
            return stop;
        }
    }
    // Клас робочого потоку
    class WorkerThread
    {
        private int id; 
        private int step; 
        private int workingTimeMilliseconds;
        private ControlThread controlThread; 
    
        public WorkerThread(int id, int step, int workingTimeMilliseconds, ControlThread controlThread)
        {
            this.id = id;
            this.step = step;
            this.workingTimeMilliseconds = workingTimeMilliseconds;
            this.controlThread = controlThread;
        }

        public void Start()
        {
            double sum = 0; 
            int count = 0;

            DateTime startTime = DateTime.Now;
            // Обчислення суми послідовності протягом вказаного часу
            while ((DateTime.Now - startTime).TotalMilliseconds < workingTimeMilliseconds)
            {
                sum +=  step; 
                count++;
            }

            // Виведення результатів
            Console.WriteLine($"Thread {id}: Sum = {sum}, Elements = {count}");
            controlThread.SetThreadFinished(id - 1); // Повідомляємо керуючому потоку, що цей потік завершив роботу
        }
    }
}
