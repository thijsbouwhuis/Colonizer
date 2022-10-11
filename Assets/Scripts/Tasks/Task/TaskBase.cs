using System;

[Serializable]
public class TaskBase
{
    private Action callback;
    protected UnitTaskManager manager;

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

    public virtual void OnTaskFinished()
    {
        if (callback != null)
        {
            callback.Invoke();
        }

        manager.OnCurTaskFinished();
    }
}
