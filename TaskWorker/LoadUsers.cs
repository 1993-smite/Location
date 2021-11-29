using DBDapper.Models;
using DBDapper.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskWorker
{
    public class LoadUsers
    {
        private string connectionString;
        private TaskCollectionGeneric<User> tasks;

        public LoadUsers([NotNull]string connectionString)
        {
            this.connectionString = connectionString;
            this.tasks = new TaskCollectionGeneric<User>();
        }

        void AddUser(User user)
        {
            var rep = new UserRepository(connectionString);
            var list = rep.GetList(user.fullname);

            if (list.Count() < 1)
            {
                rep.Create(user);
            }
        }

        void ExecuterUser()
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

        public void Run([NotNull]string fileName = null)
        {
            tasks = new TaskCollectionGeneric<User>();
            int id = 0;

            var tsks = new List<Task>();

            using (var sr = new StreamReader(fileName))
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
