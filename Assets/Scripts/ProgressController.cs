using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProgressController : Controller
{
    private static Vector3 OFFSET_VECTOR = Vector3.forward * 7f;

    public event Action<float> ProgressUpdateEvent;

    public PartLine PartLinePrefab;

    public Transform ProgressTarget;

    public Transform StartPoint;
    public Transform EndPoint;

    private bool _shouldSendProgress;

    private void Update(){
        if(!_shouldSendProgress){
            return;
        }

        if (ProgressUpdateEvent != null)
        {
            ProgressUpdateEvent.Invoke(CalculateProgress());
        }
    }

    public override void SetControllerState(GameState p_state)
    {
        switch (p_state)
        {
            case GameState.play: {
                _shouldSendProgress = true;
                break;
            }
            case GameState.stop:
            case GameState.pause:
                {
                    _shouldSendProgress = false;
                    break;
                }
            case GameState.gameOver:{
                _shouldSendProgress = false;
                break;
            }
            case GameState.win:{
                _shouldSendProgress = false;
                break;
            }
        }
    }

    public override void Init(int p_levelIndex)
    {
        CreatePartLines();

        _shouldSendProgress = false;
    }

    public override void HandleEvents(params Delegate[] p_delegates)
    {
        ProgressUpdateEvent += (Action<float>)p_delegates[2];
    }

    public override void EvaluatePlayerPosition(Vector3 p_position){
        if(CalculateProgress(p_position - OFFSET_VECTOR) >= 1f){
            if (ProgressUpdateEvent != null)
            {
                ProgressUpdateEvent.Invoke(1f);
            }
        }
    }

    private float CalculateProgress()
    {
        return CalculateProgress(ProgressTarget.position);
    }

    private float CalculateProgress(Vector3 p_position){
        Vector3 projectedVector = Vector3.Project(p_position - StartPoint.position + OFFSET_VECTOR, EndPoint.position - StartPoint.position + OFFSET_VECTOR);

        if (Vector3.Dot(projectedVector, EndPoint.position - StartPoint.position) > 0)
        {
            return projectedVector.magnitude / (EndPoint.position - StartPoint.position).magnitude;
        }
        else
        {
            return 0f;
        }
    }

    private void CreatePartLines(){
        for(int i = 1; i <= 3; i++){
            CreatePartLine(25 * i);
        }
    }

    private void CreatePartLine(int p_percentage){
        PartLine tempPartLine = Instantiate(PartLinePrefab);
        tempPartLine.transform.SetParent(transform);
        tempPartLine.transform.position = StartPoint.position + (EndPoint.position - StartPoint.position) * (p_percentage / 100f);
        tempPartLine.Init(p_percentage);
    }
}
