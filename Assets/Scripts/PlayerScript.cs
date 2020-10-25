using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text winText; 
    private int scoreValue = 0;
    public Text lives;
    private int livesText = 3;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        livesText = 3;
        lives.text = "Lives: " + livesText.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
       
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 4)
            {
                transform.position = new Vector2(42.5f, 0.0f);
                livesText = 3;
                lives.text = "Lives: " + livesText.ToString();
            }

            if (scoreValue == 8)
            {
                winText.text = "You Win! Game created by Brenden Fairchild.";
            }

            if (scoreValue == 8)
            {
                musicSource.Stop();
                musicSource.clip = musicClipTwo;
                musicSource.Play();
            }
        }

        if (collision.collider.tag == "Enemy")
        {
            livesText -= 1;
            lives.text = "Lives: " + livesText.ToString();
            Destroy(collision.collider.gameObject);

            if (livesText == 0)
            {
                winText.text = "You lose! Game made by Brenden Fairchild.";
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}
