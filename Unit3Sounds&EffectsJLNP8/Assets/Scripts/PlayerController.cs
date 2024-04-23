using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;

    public ParticleSystem explosionPartical;
    public ParticleSystem dirtSplatter;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    public float jumpForce = 15;
    public float gravityModifier = 2;

    public float score = 0;
    public float dashScore = 0;

    public bool isOnGround = true;
    public bool gameOver = false;
    public bool doubleJump = false;
    public bool dash = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtSplatter.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !gameOver && !doubleJump)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            doubleJump = true;
            playerAnim.SetTrigger("Jump_trig");
            dirtSplatter.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            dash = true;
            dashScore =+ Time.deltaTime * 2;
        }

        score += Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            doubleJump = false;
            dirtSplatter.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle")) {
            gameOver = true;
            Debug.Log("Game Over");
            Debug.Log("Score: " + Mathf.RoundToInt(score));
            
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionPartical.Play();
            dirtSplatter.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
