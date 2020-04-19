﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadowman : MonoBehaviour
{

    public float timer = 20;
    [SerializeField]
    GameObject player;
    [SerializeField]
    AudioManagement music;
    [SerializeField]
    Material[] materials;
    [SerializeField]
    SkinnedMeshRenderer shadowman;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        music.anger = (timer < 10);

        //Look at the player
        Vector3 relative = player.transform.position - transform.position;
        float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);

        //When angered, approach
        if(music.anger)
        {
            if(timer > 5)
            {
                transform.Translate(transform.forward * 0.1f);
            }
            else
            {
                transform.Translate(transform.forward * 0.2f);
            }
            
            if(Random.Range(0, 7) == 3)
            {
                transform.localScale = new Vector3(Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5));
            }
            if(Random.Range(0, 10) == 3)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            
            shadowman.material = materials[Random.Range(1,4)];
            
           
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            shadowman.material = materials[0];
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Food")
        {
            timer = 40;
            Destroy(collision.gameObject);
        }

        if(music.anger && collision.gameObject.name == "Player")
        {
            //end game

        }
    }
}
