using System;
using System.Collections.Generic;
using System.Text;

namespace TaskWorker
{
    public class TaskAction<T>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public T Param { get; private set; }

        public Action<T> Action;

        public TaskAction(T param, Action<T> action)
        {
            Param = param;
            Action = action;
        }

        public TaskAction(int id, Action<T> action)
        {
            Id = id;
            Action = action;
        }

        public TaskAction(int id, Action<T> action, string name): this(id, action)
        {
            Name = name;
        }

        public TaskAction(int id, Action<T> action, string name, string description): this(id, action, name)
        {
            Description = description;
        }

        public void Invoke(T param = default)
        {
            T arg = param ?? Param;
            Action.Invoke(arg);
        }
    }
}
