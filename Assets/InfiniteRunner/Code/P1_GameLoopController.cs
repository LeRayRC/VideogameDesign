using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum GameState{
    STARTED,
    PAUSED,
    SCORE,
}

public class P1_GameLoopController : MonoBehaviour
{
    public GameState state_;
    public Image image_;
    public Image score_;
    public TMP_Text scoreText_;

    public AudioClip newTrack;
    // Start is called before the first frame update
    void Start()
    {
        state_ = GameState.PAUSED;
        image_.gameObject.SetActive(true);
        score_.gameObject.SetActive(false);
        AudioManager.instance.ReturnToDefault();
    }

    // Update is called once per frame
    void Update()
    {
        if ( state_ == GameState.PAUSED){
            Time.timeScale = 0.0f;
        }else if (state_ == GameState.STARTED){
            Time.timeScale = 1.0f;
        }
    }

    public void PlayGame(){
        state_ = GameState.STARTED;
        Time.timeScale = 1.0f;
        image_.gameObject.SetActive(false);
        score_.gameObject.SetActive(false);
        AudioManager.instance.SwapTrack(newTrack);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void PauseGame(){
        state_ = GameState.PAUSED;
        Time.timeScale = 0.0f;
        image_.gameObject.SetActive(true);
        score_.gameObject.SetActive(false);
    }

    public void ShowScoreTable(int score, int distance){
        state_ = GameState.SCORE;
        Time.timeScale = 0.0f;
        image_.gameObject.SetActive(false);
        score_.gameObject.SetActive(true);
        AudioManager.instance.ReturnToDefault();

        scoreText_.text = "You got " + score + " coins \nduring " + distance + " m";
    }
}
