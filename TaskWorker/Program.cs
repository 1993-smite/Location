using DBDapper.Models;
using DBDapper.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskWorker
{
    class Program
    {
        private static string cs = "Server=DOSTZAL;Initial Catalog=TestUsers;Integrated Security=True";
        private static TaskCollection list;
        private static TaskCollectionGeneric<User> tasks;

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

        static void Main1(string[] args)
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
                list.AddTask(new TaskAction<object>(i, Tsk ));
            }

            Thread.Sleep(3000);
            Console.WriteLine("Hello World!");
        }

        static void AddUser(User user)
        {
            var rep = new UserRepository(cs);
            var list = rep.GetList(user.fullname);

            if (list.Count() < 1)
            {
                rep.Create(user);
            }
        }

        static void ExecuterUser()
        {
            TaskAction<User> tsk;
            do
            {
                tsk = tasks?.TaskActions.Take();
                tsk?.Invoke();
                tasks?.RemoveTask(tsk);
            }
            while (tsk != null);
        }

        static void Main(string[] args)
        {
            tasks = new TaskCollectionGeneric<User>();
            int id = 0;

            var tsks = new List<Task>();

            using (var sr = new StreamReader("D:\\users.csv"))
            {
                do
                {
                    string line = sr.ReadLine();
                    string[] cels = line.Split(';');

                    var user = new User()
                    {
                        id = int.Parse(cels[0]?.Trim()),
                        fullname = cels[2].Replace("\"", "").Trim(),
                        phone = cels[5]?.Trim(),
                        address = cels[7]?.Trim()
                    };

                    var tsk = new Task(ExecuterUser);
                    tsks.Add(tsk);
                    tsk.Start();

                    tasks.AddTask(new TaskAction<User>(user, AddUser));
                }
                while (!sr.EndOfStream);
            }

            while (tasks.TaskActions.Count > 0)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Task count equals {0}", tasks.TaskActions.Count);
            }

            Console.WriteLine("The End!");
        }
    }
}
