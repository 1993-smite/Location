using System;
using System.Collections.Concurrent;

namespace TaskWorker
{
    public class TaskCollection
    {
        public BlockingCollection<TaskAction<object>> TaskActions { get; private set; }

        public delegate void TaskActionHandler(TaskAction<object> task);
        public delegate void TaskActionCommonHandler();

        public event TaskActionHandler NotifyAdd;
        public event TaskActionHandler NotifyRemove;
        public event TaskActionCommonHandler NotifyClear;

        public TaskCollection()
        {
            TaskActions = new BlockingCollection<TaskAction<object>>();
        }

        public TaskCollection(TaskActionHandler notifyAdd, TaskActionHandler notifyRemove) : this()
        {
            NotifyAdd = notifyAdd;
            NotifyRemove = notifyRemove;
        }

        public TaskCollection(TaskActionHandler notifyAdd, TaskActionHandler notifyRemove, TaskActionCommonHandler notifyClear) : this(notifyAdd, notifyRemove)
        {
            NotifyClear = notifyClear;
        }

        public void AddTask(TaskAction<object> task)
        {
            TaskActions.TryAdd(task);
            NotifyAdd?.Invoke(task);
        }

        public void RemoveTask(TaskAction<object> task)
        {
            NotifyRemove?.Invoke(task);
        }

        public void Clear()
        {
            NotifyClear?.Invoke();
        }
    }
}
