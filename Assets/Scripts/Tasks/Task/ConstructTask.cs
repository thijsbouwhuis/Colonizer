using System;
using UnityEngine;

public class ConstructTask : TaskBase
{
    private Action moveCompleteCallback;
    private ConstructionBuilding target;
    private float buildTime;
    private float timer;
    private GameObject finishedBuilding;
    public ConstructTask(Action callback, ConstructionBuilding target, GameObject finishedBuilding, float buildTime) : base(callback)
    {
        this.target = target;
        this.buildTime = buildTime;
        this.finishedBuilding = finishedBuilding;
    }

    public override void StartTask()
    {
        base.StartTask();
        
        moveCompleteCallback += OnMoveToComplete;
        
        FindPathTask task = new FindPathTask(moveCompleteCallback, new Vector2Int(0,0), target.PlacedPos, 0.1f);
        task.Initialize(manager);
        subTasks.Add(task);
    }

    public override void UpdateTask()
    {
        if (UpdateSubTask()) { return; }
        
        base.UpdateTask();
        
        //Build functionality here
        if (timer >= buildTime)
        {
            OnTaskFinished();
        }

        timer += Time.fixedDeltaTime;
    }

    protected override void OnTaskFinished()
    {
        //Replace cur building with a new one
        GameObject building = UnityEngine.Object.Instantiate(finishedBuilding);
        building.transform.position = target.gameObject.transform.position;
        UnityEngine.Object.Destroy(target.gameObject);
        
        base.OnTaskFinished();
    }

    private void OnMoveToComplete()
    {
        curSubTask = null;
    }
}
