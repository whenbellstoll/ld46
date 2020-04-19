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
    [SerializeField]
    SkinnedMeshRenderer shadowman;
    [SerializeField]
    UnityEngine.UI.Text text;
    public enum AskFor
    {
        Cigarette,
        Glass,
        Brandy,
        Piano
    }
    public AskFor askFor;
    // Start is called before the first frame update
    void Start()
    {
        askFor = AskFor.Cigarette;
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

        if( timer > 39 )
        {
            if(askFor == AskFor.Cigarette)
            {
                text.text = "Hello there young man, could you get a corporeal man some smokes? \n (R click pick up & place objects)"; 
            }
            else if(askFor == AskFor.Glass)
            {
                text.text = "Thank you. would you kindly get me a wine glass, I'd like to have a drink."; 
            }
            else if(askFor == AskFor.Brandy)
            {
                text.text = "My you're quite the pleaser, get me some Brandy to fill this up.";
            }
            else
            {
                text.text = "Play me and my guests a tune on the piano, I'm sure you know how";
            }
        }
        else if( timer > 29.85 && timer < 30 )
        {
            switch( Random.Range(0,3) )
            {
                case 0:
                    text.text = "Hurry along now";
                    break;
                case 1:
                    text.text = "Chop chop!";
                    break;
                case 2:
                    text.text = "Expedience is of the essence, my good sir.";
                    break;
            }
            
        }
        else if( timer > 19 && timer < 20 )
        {
            if(askFor != AskFor.Piano)
            {
                text.text = "I grow impatient, boy, get me my " + askFor;
            }
            else
            {
                text.text = "Come on now, play.";
            }
        }

        if( (int)timer % 10 == 5 )
        {
             text.text = " ";
        }

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
            if (askFor != AskFor.Piano)
            {
                text.text = "I told you I wanted you to get me my " + askFor;
            }
            else
            {
                text.text = "I told you to play piano! Now get PLAYING!";
            }


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
            if(askFor == AskFor.Cigarette && collision.gameObject.name == "Cigarette")
            {
                timer = 40;
                Destroy(collision.gameObject);
                askFor = AskFor.Glass;
            }
            else if(askFor == AskFor.Glass && collision.gameObject.name == "Glass")
            {
                timer = 40;
                Destroy(collision.gameObject);
                askFor = AskFor.Brandy;
            }
            else if(askFor == AskFor.Brandy && collision.gameObject.name == "Brandy")
            {
                timer = 40;
                Destroy(collision.gameObject);
                askFor = AskFor.Piano;
            }
            else
            {
                //modify text
                text.text = "I asked for a " + askFor + " not a " + collision.gameObject.name;
            }

        }

        if(music.anger && collision.gameObject.name == "Player")
        {
            //end game

        }
    }
}
