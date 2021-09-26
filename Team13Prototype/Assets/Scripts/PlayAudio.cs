using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource ambience;

    // Start is called before the first frame update
    void Start()
    {
        ambience.Play();
        ambience.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
