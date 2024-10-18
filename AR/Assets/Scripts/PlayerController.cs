using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    AudioSource dolphinSound;
    int position = 0;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SayBadWord();
        }
    }

    void SayBadWord()
    {
        dolphinSound.Play();
    }
}
