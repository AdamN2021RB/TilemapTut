using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    //GROUP OF PUBLIC AND PRIVATE OBJECTS
    private Rigidbody2D rd2d;

    //Animator anim;

    public float speed;

    public Text score;

    public Text lives;

     public AudioClip CoinGet;

    public AudioSource musicSource;

    private int scoreValue = 0;

    private int livesValue = 3;

    public GameObject winTextObject;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winTextObject.SetActive(false);
        lives.text = livesValue.ToString();

        setCoinText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKeyDown(KeyCode.D))
        {
            
        }
    }


//what happens when the player gets a coin or touches an enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue = scoreValue + 1;
            score.text = scoreValue.ToString();
            musicSource.clip = CoinGet;
            musicSource.Play();
            setCoinText();
            Destroy(collision.collider.gameObject);
        }

 
         if (collision.collider.tag == "Enemy")
        {
            livesValue = livesValue - 1;
            lives.text = livesValue.ToString();
            transform.position = new Vector3(0f, 0.7f);
        }
    }

    //creating the UI for coin collection and creating win conditions
    void setCoinText()
    {
        score.text = "Coins: " + scoreValue.ToString();

        if (scoreValue == 4)
        {
            winTextObject.SetActive(true);
        }
    }

    private void LifeReset()
    {
        if (livesValue == 0)
        {
            livesValue = livesValue + 3;
            livesValue.ToString();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 5), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
                if (Input.GetKeyUp(KeyCode.W))
                {
                    rd2d.AddForce(new Vector2(0, -4), ForceMode2D.Impulse);
                }
            }
        }

    }
}
