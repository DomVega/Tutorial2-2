using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    public Text winText;

    public Text livesText;

    public Text loseText;

    private int scoreValue = 0;

    private int lives = 3;

    private bool facingRight = true;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    
    Animator anim; 

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        loseText.text = "";
        SetLivesText();
        anim = GetComponent<Animator> ();

        musicSource.clip = musicClipOne;
        musicSource.Play();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    
        if (Input.GetKeyDown(KeyCode.D))
            {

                anim.SetInteger("State", 1);

            }
        if (Input.GetKeyDown(KeyCode.A))
            {

                anim.SetInteger("State", 1);

            }
        if (Input.GetKeyUp(KeyCode.A))
            {

                anim.SetInteger("State", 0);

            }
        if (Input.GetKeyUp(KeyCode.D))
            {

                anim.SetInteger("State", 0);

            }
        if (facingRight == false && hozMovement > 0)
            {
                Flip();
            }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            ScoreText();
            Destroy(collision.collider.gameObject);
        }

          if (scoreValue == 4)
        {
            transform.position = new Vector2(3.18f, -23.12f);
            lives = 3;
            SetLivesText ();
        }
        
         if (scoreValue == 8)
        {
            anim.SetInteger("State", 0);
            musicSource.clip=musicClipOne;
            musicSource.Stop();
            winText.text = "You Win! Game by Domenico Vega";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            
        }
       
        if (collision.collider.tag == "Enemy")
        {
            lives -= 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }
       
        if (lives <= 0)
        {
            loseText.text = "You Lose!";

        }
    
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                anim.SetInteger("State", 2);
            }
            else
            {
                anim.SetInteger("State", 1);
            }
        }
    }

    void SetLivesText ()
    {
        livesText.text = "Lives: " + lives.ToString ();
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
        Application.Quit();
        }
    }

    void ScoreText()
    {
        score.text = scoreValue.ToString();

         if (scoreValue == 4)
        {
            transform.position = new Vector2(3.18f, -23.12f);
            lives = 3;
            SetLivesText ();
        }
        
         if (scoreValue >= 8)
        {
            winText.text = "You win! Game by Domenico Vega";
        }
    }
}