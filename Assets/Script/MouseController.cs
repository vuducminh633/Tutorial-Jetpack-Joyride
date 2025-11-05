using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float jetPackForce = 75f;
    public float forwardMoveSpeed = 3f;

    public Transform groundCheck;
    private Animator anim;
    private bool isGround;

    public ParticleSystem jetPackParticle;

    private Rigidbody2D playerRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
    }

    private void FixedUpdate()
    {
        bool jetpackActive = Input.GetButton("Fire1");
        if (jetpackActive)
        {
            playerRb.AddForce(new Vector2(0, jetPackForce));
        }

        //Move forward logic 
        playerRb.velocity = new Vector2( forwardMoveSpeed , playerRb.velocity.y);

        //Check Ground
        UpdateGroundStatus();

        AdjustJetpackParticle(jetpackActive);  // jetpack active when press, flase when releash
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
}
