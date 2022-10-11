using System.Collections.Generic;
using UnityEngine;

public class UnitTaskManager : MonoBehaviour
{
    [SerializeField]
    private List<TaskBase> tasks = new List<TaskBase>();
    private TaskBase curTask;
    private TaskBase curSubTask;

    private void Start()
    {
        TaskManager.instance.RegisterUnit(this);
    }

    public void AddTask(TaskBase task)
    {
        tasks.Add(task);
        task.Initialize(this);
    }

    public void RemoveTask(TaskBase task)
    {
        tasks.Remove(task);
    }

    public void OnCurTaskFinished()
    {
        curTask = null;
    }
    
    public void SetSubTask(TaskBase task)
    {
        curSubTask = task;
        curSubTask.StartTask();
    }

    public void Tick()
    {
        if (curSubTask != null)
        {
            curSubTask.UpdateTask();
        }
        else
        {
            //Update cur task
            if (curTask != null)
            {
                curTask.UpdateTask();
            }
            else if (tasks.Count > 0)
            {
                curTask = tasks[0];
                tasks.RemoveAt(0);
                curTask.StartTask();
            }
        }
    }
}
