using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windSoundTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Renderer renderer = GetComponent<Renderer>();
        AudioSource audio = GetComponent<AudioSource>();
        if (renderer.isVisible && (!audio.isPlaying))
        {
            GetComponent<AudioSource>().Play();
        }
        if (!renderer.isVisible)
        {
            GetComponent<AudioSource>().Stop();
        }
    }

    
}
