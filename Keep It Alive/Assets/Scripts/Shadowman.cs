using System.Collections;
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
                transform.Translate(transform.forward * 0.2f);
            }
            else
            {
                transform.Translate(transform.forward * 0.3f);
            }
            
            if(Random.Range(0, 7) == 3)
            {
                transform.localScale = new Vector3(Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5));
            }
            if(Random.Range(0, 10) == 3)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
           
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Food")
        {
            timer = 60;
            Destroy(collision.gameObject);
        }
    }
}
