using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskWorker
{
    public static class Runner
    {
        private static TaskCollection list;
        static void Tsk(object param = null)
        {
            Console.WriteLine("Thread #{0}, task id {1}", Thread.CurrentThread.ManagedThreadId, param);
        }

        static void Executer()
        {
            TaskAction<object> tsk;
            do
            {
                tsk = list.TaskActions.Take();
                tsk.Invoke(tsk.Id);
            }
            while (tsk != null);
        }

        public static void Run()
        {
            list = new TaskCollection();

            var tsks = new List<Task>();
            for (int index = 0; index < 4; index++)
            {
                var tsk = new Task(Executer);
                tsks.Add(tsk);
                tsk.Start();
            }

            int i = 0;
            while (i++ < 100)
            {
                list.AddTask(new TaskAction<object>(i, Tsk));
            }

            Thread.Sleep(3000);
            Console.WriteLine("Hello World!");
        }
    }
}
