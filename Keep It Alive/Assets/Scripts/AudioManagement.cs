using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    [SerializeField]
    AudioSource main;
    [SerializeField]
    AudioSource cursed;
    [SerializeField]
    float transSpeed = 0.005f;
    bool anger = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (main == null || cursed == null)
        {
            AudioSource[] audioSs = GetComponents<AudioSource>();
            main = audioSs[0];
            cursed = audioSs[1];
        }

        if( anger )
        {
            if(cursed.volume < 1 )
            {
                cursed.volume += transSpeed;
            }
            if( main.volume > 0 )
            {
                main.volume -= transSpeed;
            }
        }
        else
        {
            if (cursed.volume > 0)
            {
                cursed.volume -= transSpeed;
            }
            if (main.volume < 1)
            {
                main.volume += transSpeed;
            }
        }

        if( Input.GetKeyDown(KeyCode.E) )
        {
            anger = !anger;
        }
    }
}
