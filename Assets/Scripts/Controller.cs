using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Controller : MonoBehaviour
{
    public abstract void Init(int p_levelIndex);
    public abstract void SetControllerState(GameState p_state);

    ///<summary>
    /// [0] StartGame [1] GameOver [2] UpdateProgress [3] PlayerPosition
    ///</summary>
    public virtual void HandleEvents(params Delegate[] p_delegates){

    }

    public virtual void EvaluateProgress(float p_progress){
        
    }

    public virtual void EvaluatePlayerPosition(Vector3 p_position){

    }
}
