using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    //GROUP OF PUBLIC AND PRIVATE OBJECTS
    private Rigidbody2D rd2d;

    public Animator anim;

    public float speed;

    public float jumpForce;

    public Text score;

    public Text lives;

     public AudioClip CoinGet;



    public AudioSource musicSource;

    private int scoreValue = 0;

    private int livesValue = 3;

    private bool facingRight = false;
    private bool isOnGround;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask allGround;

    public GameObject winTextObject;
    public GameObject loseTextObject;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        lives.text = livesValue.ToString();

        setCoinText();
        setLivesText();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, allGround);

        if (facingRight == false && hozMovement < 0)
        {
            Flip();
        }

        if (facingRight == true && hozMovement > 0)
        {
            Flip();
        }

        if ((hozMovement < 0 || hozMovement > 0) && isOnGround != false)
        {
            anim.SetInteger("State", 1);
        }

        else if (hozMovement == 0 && isOnGround != false)
        {
            anim.SetInteger("State", 0);
        }

        if (vertMovement != 0)
        {
            anim.SetInteger("State", 2);
        }

        
    }

    void Update()
    {
        
    }

//what happens when the player gets a coin, goes out of bounds, or touches an enemy
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
            setLivesText();

            if (collision.collider.tag == "Enemy" && scoreValue >= 4)
            {
                lives.text = livesValue.ToString();
                transform.position = new Vector3(59f, 1.5f);
                setLivesText();
            }
        }

//was going to use this for defeating enemies, but scrapped it.
        else if (collision.collider.tag == "WeakPoint" && collision.collider.tag == "Enemy")
        {
            musicSource.clip = CoinGet;
            musicSource.Play();
            Destroy(collision.collider.gameObject);

        }
//Out of bounds in case the player falls
        if (collision.collider.tag == "Boundary")
        {
            transform.position = new Vector3(0f, 0.7f);

            if (collision.collider.tag == "Boundary" && scoreValue >= 4)
            {
                transform.position = new Vector3(59f, 1.5f);
            }
        }
    }

    //creating the UI for coin collection and creating win conditions
    void setCoinText()
    {
        score.text = "Coins: " + scoreValue.ToString();

        if (scoreValue == 4)
        {
            transform.position = new Vector3(59f, 1.5f);
            livesValue = 3;
            setLivesText();
        }

        if (scoreValue == 8)
        {
            winTextObject.SetActive(true);
        }
    }

//used for calculating lives left
    private void setLivesText()
    {
        lives.text = "Lives: " + livesValue.ToString();
        if (livesValue == 0)
        {
            anim.SetInteger("State", 3);
            Destroy(this);
            loseTextObject.SetActive(true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
                if (Input.GetKeyUp(KeyCode.W))
                {
                    rd2d.AddForce(new Vector2(0, -4), ForceMode2D.Impulse);
                }
            }
        }

    }

       //for changing character direction
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
}
