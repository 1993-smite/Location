using System;
using System.Collections.Concurrent;

namespace TaskWorker
{
    public class TaskCollectionGeneric<TParam>
    {
        public BlockingCollection<TaskAction<TParam>> TaskActions { get; private set; }

        public delegate void TaskActionHandler(TaskAction<TParam> task);
        public delegate void TaskActionCommonHandler();

        public event TaskActionHandler NotifyAdd;
        public event TaskActionHandler NotifyRemove;
        public event TaskActionCommonHandler NotifyClear;

        public TaskCollectionGeneric()
        {
            TaskActions = new BlockingCollection<TaskAction<TParam>>();
        }

        public TaskCollectionGeneric(TaskActionHandler notifyAdd, TaskActionHandler notifyRemove) : this()
        {
            NotifyAdd = notifyAdd;
            NotifyRemove = notifyRemove;
        }

        public TaskCollectionGeneric(TaskActionHandler notifyAdd, TaskActionHandler notifyRemove, TaskActionCommonHandler notifyClear) : this(notifyAdd, notifyRemove)
        {
            NotifyClear = notifyClear;
        }

        public void AddTask(TaskAction<TParam> task)
        {
            TaskActions.TryAdd(task);
            NotifyAdd?.Invoke(task);
        }

        public void RemoveTask(TaskAction<TParam> task)
        {
            NotifyRemove?.Invoke(task);
        }

        public void Clear()
        {
            NotifyClear?.Invoke();
        }
    }
}
