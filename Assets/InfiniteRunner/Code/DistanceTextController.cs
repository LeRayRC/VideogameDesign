using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceTextController : MonoBehaviour
{
    // Start is called before the first frame update
    public HeroController hc_;
    private TMP_Text textMesh_;
    // Start is called before the first frame update
    void Start()
    {
        textMesh_ = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(textMesh_ == null){
            Debug.Log("NULL");
        }else{
            textMesh_.text = "DISTANCE: " + (int)hc_.distanceDone_;

        }
    }
}
