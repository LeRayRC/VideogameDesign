using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "New Obstacle", menuName = "MyAutorun/Obstacles/New Obstacle", order = 101)]
public class ObstacleData : ScriptableObject
{
    
    // Start is called before the first frame update
    public GameObject obstaclePrefab_;

    public virtual GameObject InstantiateOnGame(ObstaclesController oc_){
        Vector3 new_position = new Vector3();
        Vector3 edge_position;
        GameObject obstacleGo_ = null;
        //Forward obstacle
        if (oc_.lastGeneratedObstacle.transform.eulerAngles.y % 180 == 0 || oc_.lastGeneratedObstacle.transform.eulerAngles.y == 0)
        {
            edge_position = new Vector3(oc_.lastGeneratedObstacle.transform.forward.x * oc_.lastGeneratedObstacle.transform.localScale.x * 0.5f,
                                            oc_.lastGeneratedObstacle.transform.forward.y * oc_.lastGeneratedObstacle.transform.localScale.y * 0.5f,
                                            oc_.lastGeneratedObstacle.transform.forward.z * oc_.lastGeneratedObstacle.transform.localScale.z * 0.5f);
            new_position = oc_.lastGeneratedObstacle.transform.position + edge_position + oc_.lastGeneratedObstacle.transform.forward * oc_.spaceBetweenObstacles;


        }
        else
        {
            edge_position = new Vector3(oc_.lastGeneratedObstacle.transform.forward.x * oc_.lastGeneratedObstacle.transform.localScale.z * 0.5f,
                                            oc_.lastGeneratedObstacle.transform.forward.y * oc_.lastGeneratedObstacle.transform.localScale.y * 0.5f,
                                            oc_.lastGeneratedObstacle.transform.forward.z * oc_.lastGeneratedObstacle.transform.localScale.x * 0.5f);
            new_position = oc_.lastGeneratedObstacle.transform.position + edge_position + oc_.lastGeneratedObstacle.transform.forward * oc_.spaceBetweenObstacles;

            // Debug.Log("new pos: " + new_position);
        }


        obstacleGo_ = Instantiate<GameObject>(obstaclePrefab_,
                                                     new_position,
                                                     oc_.lastGeneratedObstacle.transform.rotation);

        obstacleGo_.transform.Translate(TranslateHalfScale(obstacleGo_.transform, oc_.lastGeneratedObstacle.transform, oc_.changeDirection), Space.World);


        return obstacleGo_;
    }

    public virtual Vector3 TranslateHalfScale(Transform tr_, Transform parentTr_, bool changeDirection = false)
    {
        Vector3 translate_pos = new Vector3();
        // Debug.Log(parentTr_.gameObject.name + " rot y : " + parentTr_.eulerAngles.y);

        if (parentTr_.eulerAngles.y % 180 == 0 || parentTr_.eulerAngles.y == 0)
        {
            // Debug.Log("Change Direction!");
            // Debug.Log("Translate pos " + translate_pos);
            translate_pos.x = tr_.localScale.x * 0.5f * tr_.forward.x;
            translate_pos.y = tr_.localScale.y * 0.5f * tr_.forward.y;
            translate_pos.z = tr_.localScale.z * 0.5f * tr_.forward.z;

        }
        else if (parentTr_.eulerAngles.y % 90 == 0)
        {
            // Debug.Log("Nothing");
            translate_pos.x = tr_.localScale.z * 0.5f * tr_.forward.x;
            translate_pos.y = tr_.localScale.y * 0.5f * tr_.forward.y;
            translate_pos.z = tr_.localScale.x * 0.5f * tr_.forward.z;
        }

        return translate_pos;
    }
}
