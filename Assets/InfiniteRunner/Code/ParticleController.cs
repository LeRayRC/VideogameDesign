using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public HeroController hc_;
    ParticleSystem particleSys_;
    Animator anim_;
    // Start is called before the first frame update
    void Start()
    {
        particleSys_ = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hc_.canJump_)
        {
            particleSys_.Stop();
        }
        else
        {
            particleSys_.Clear();
            //particleSys_.Simulate(particleSys_.main.duration);
            particleSys_.Play();
        }
    }
}
