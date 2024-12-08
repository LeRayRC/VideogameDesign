using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLayeringManager : MonoBehaviour
{

    public AudioSource baseMusic;   
    public AudioSource layer2Music; 
    public AudioSource layer3Music;

    public float VolumeBase = 0.1f;
    public float VolumeLayer2 = 0.1f;
    public float VolumeLayer3 = 0.1f; 

    public int triggerValueLayer2 = 100; 
    public int triggerValueLayer3 = 200; 

    public float fadeTime = 2.0f;
    public HeroController hero_;


    // Start is called before the first frame update
    void Start()
    {
        baseMusic.Stop();
        layer2Music.volume = 0;
        // StartCoroutine(FadeIn(baseMusic));

        layer2Music.Stop();
        layer2Music.volume = 0;
        layer3Music.Stop();
        layer3Music.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hero_.distanceDone_ <= 10.0f)
        {
            baseMusic.Stop();
            layer2Music.Stop();
            layer3Music.Stop();
        }
        else
        {
            if (!baseMusic.isPlaying){
                baseMusic.Play();
                StartCoroutine(FadeIn(baseMusic, VolumeBase));
            }

            if (hero_.distanceDone_ >= triggerValueLayer2 && !layer2Music.isPlaying)
            {
                layer2Music.Play();
                layer2Music.time = baseMusic.time;
                // layer2Music.volume = baseMusic.volume;
                StartCoroutine(FadeIn(layer2Music, VolumeLayer2));
            }

            if (hero_.distanceDone_ >= triggerValueLayer3 && !layer3Music.isPlaying)
            {
                layer3Music.Play();
                layer3Music.time = baseMusic.time;
                // layer3Music.volume = baseMusic.volume;
                StartCoroutine(FadeIn(layer3Music, VolumeLayer3));
            }
        }
    }

    private System.Collections.IEnumerator FadeIn(AudioSource audioSource, float maxVolume)
    {
        float startVolume = 0.0f;

        for (float t = 0.01f; t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, maxVolume, t / fadeTime);
            yield return null;
        }

        audioSource.volume = 1.0f; 
    }
}
