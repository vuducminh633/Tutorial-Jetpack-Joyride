using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class MouseController : MonoBehaviour
{
    public float jetPackForce = 75f;
    public float forwardMoveSpeed = 3f;

    public Transform groundCheck;
    private Animator anim;
    private bool isGround;

    public ParticleSystem jetPackParticle;

    private Rigidbody2D playerRb;

    bool isDead;

    private int coins = 0;
    public Text coinCollectedText;



    public AudioClip coinCollectedSound;



    public AudioSource jetpackAudio;
    public AudioSource footstepsAudio;

    public ParallaxCamera parallaxCamera;

    [Header("Lose Panel")]
    public GameObject reStartDialog;


    void Start()
    {
        reStartDialog.SetActive(false);
        isDead = false;
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
    }

    private void FixedUpdate()
    {
      
        bool jetpackActive = Input.GetButton("Fire1");


        if(!isDead)
        {

            if (jetpackActive)
            {
                playerRb.AddForce(new Vector2(0, jetPackForce));
            }

            //Move forward logic 
            playerRb.velocity = new Vector2(forwardMoveSpeed, playerRb.velocity.y);
        }
      

        //Check Ground
        UpdateGroundStatus();

        AdjustJetpackParticle(jetpackActive);  // jetpack active when press, flase when releash


        AdjustFootStepAndJetPackSound(jetpackActive);

        //camera offset follow player
        parallaxCamera.offset = transform.position.x;

    }

    private void UpdateGroundStatus()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, .1f, LayerMask.GetMask("Ground"));

        anim.SetBool("isGrounded", isGround);
    }

    private void AdjustJetpackParticle(bool jetpackActive)
    {
        var jetPackEmission = jetPackParticle.emission;

        jetPackEmission.enabled = !isGround;

        if (jetpackActive)
        {
            jetPackEmission.rateOverTime = 300.0f;
        }
        else
        {
            jetPackEmission.rateOverTime = 75.0f;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
      

        if (collision.CompareTag("Coin"))
        {
            CollectCoint(collision);
        }
        else
        {
            HitByLaser(collision);
        }
    }

    void HitByLaser(Collider2D laserCollider)
    {
        if (!isDead)
        {
            AudioSource laserZap = laserCollider.gameObject.GetComponent<AudioSource>();
            laserZap.Play();
        }

        isDead = true;
        anim.SetBool("isDead", true);

        reStartDialog.SetActive(true);
    }

    private void CollectCoint(Collider2D coinCollider)
    {
        AudioSource.PlayClipAtPoint(coinCollectedSound, transform.position);
        coins++;
        coinCollectedText.text = coins.ToString();
        Destroy(coinCollider.gameObject);
    }

  

    private void AdjustFootStepAndJetPackSound(bool jetpackActive)
    {
        footstepsAudio.enabled = !isDead && isGround;
        jetpackAudio.enabled = !isDead && !isGround;

        if (jetpackActive)
        {
            jetpackAudio.volume = 1.0f;
        }
        else
        {
            jetpackAudio.volume = 0.5f;
        }
    }

    #region Lose Panel

    public void ReStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    #endregion



}
