using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TaskBase
{
    protected Action callback;
    protected UnitTaskManager manager;
    protected List<TaskBase> subTasks = new List<TaskBase>();
    protected TaskBase curSubTask;

    public TaskBase(Action callback)
    {
        this.callback = callback;
    }

    public virtual void Initialize(UnitTaskManager manager)
    {
        this.manager = manager;
    }
    
    public virtual void StartTask()
    {
    }

    public virtual void UpdateTask()
    {
        
    }

    protected virtual void OnTaskFinished()
    {
        if (callback != null)
        {
            callback.Invoke();
        }

        manager.OnTaskFinished(this);
    }

    //returns true if there's a subtask to tick
    protected virtual bool UpdateSubTask()
    {
        if (curSubTask != null)
        {
            curSubTask.UpdateTask();
            return true;
        } 
        else if (subTasks.Count > 0)
        {
            curSubTask = subTasks[0];
            subTasks.RemoveAt(0);
            curSubTask.StartTask();
            return true;
        }

        return false;
    } 
}
