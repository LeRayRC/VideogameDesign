using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextController : MonoBehaviour
{
    public HeroFunctions hf_;
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
            textMesh_.text = "SCORE: " + hf_.score;

        }
    }
}
