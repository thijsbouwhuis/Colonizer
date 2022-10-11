using System.Collections.Generic;
using UnityEngine;

public class UnitTaskManager : MonoBehaviour
{
    [SerializeField] private List<TaskBase> tasks = new List<TaskBase>();
    private TaskBase curTask;

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

    public void OnTaskFinished(TaskBase task)
    {
        //if it's not the same, then it's the current task of the subtask and we don't do anything with it
        if (curTask == task)
        {
            curTask = null;
        }
    }
    
    public void Tick()
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