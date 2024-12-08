using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plain Obstacle", menuName = "MyAutorun/Obstacles/Medium/Plain Jump", order = 101)]

public class Medium_PlainJump : PlainObstacle
{
    // Start is called before the first frame update
    public float minPatrolSpeed;
    public float maxPatrolSpeed;

    Medium_PlainJumpController plainJumpController_;

    public override GameObject InstantiateOnGame(ObstaclesController oc_){
        
        GameObject go_ = base.InstantiateOnGame(oc_);

        plainJumpController_ = go_.GetComponentInChildren<Medium_PlainJumpController>();
        plainJumpController_.Init(minPatrolSpeed,maxPatrolSpeed);

        return go_;
    }
}
