using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;
    private List<UnitTaskManager> unitTaskManagers = new List<UnitTaskManager>();

    private List<TaskBase> assignableTasks = new List<TaskBase>();

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        List<TaskBase> assignedTasks = new List<TaskBase>();
        foreach (TaskBase task in assignableTasks)
        {
            //TODO proper division of tasks
            unitTaskManagers[0]?.AddTask(task);
            assignedTasks.Add(task);
        }

        if (assignedTasks.Count > 0) { assignableTasks = assignableTasks.Except(assignedTasks).ToList(); }


        foreach (UnitTaskManager taskManager in unitTaskManagers)
        {
            taskManager.Tick();
        }
    }

    public void RegisterUnit(UnitTaskManager unitTaskManager)
    {
        unitTaskManagers.Add(unitTaskManager);
    }

    public void UnregisterUnit(UnitTaskManager unitTaskManager)
    {
        unitTaskManagers.Remove(unitTaskManager);
    }

    public void AddTask(TaskBase task)
    {
        assignableTasks.Add(task);
    }
}
