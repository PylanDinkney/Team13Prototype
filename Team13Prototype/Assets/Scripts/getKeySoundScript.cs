using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getKeySoundScript : MonoBehaviour
{
    public AudioSource getKeySound;
    private bool playedSound;
    playerAttributes tomAttrib;

    // Start is called before the first frame update

    void Start()
    {
        GameObject tempSound = GameObject.Find("ItemSound");
        getKeySound = tempSound.GetComponent<AudioSource>();
        GameObject tom = GameObject.Find("Tom");
        tomAttrib = tom.GetComponent<playerAttributes>();
        playedSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(tomAttrib.Item == "Office Key" && playedSound == false){
            getKeySound.Play();
            playedSound = true;
        }
        else if(tomAttrib.Item != "Office Key" && playedSound == true){
            playedSound = false;
        }
    }
}
