using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    static public PlayerController S; // Singleton

    private Rigidbody _playerRb;
    private Animator _playerAnim;
    private AudioSource _playerAudio;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private GameObject dirtSplatter;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool isDoubleJump = false;
    public bool gameOver = false;
    public bool isSprint = false;
    public int _state;

    private void Awake()
    {
        if (S == null)
        {
            S = this; // singleton init
        }
        else
        {
            Debug.Log("The singleton S of Player has already been set!");
        }
    }

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        dirtSplatter = GameObject.Find("FX_DirtSplatter");
        dirtSplatter.SetActive(false);
    }

    void Update()
    {
        _state = (int)GameObject.Find("GameController").GetComponent<GameController>().state; // check the current player's state
        switch (_state)
        {
            case 0: // idle
                break;
            case 1: // walking
                break;
            case 2: // running
                _playerAnim.SetBool("EndWalk_b", true);
                dirtSplatter.SetActive(true);
                break;
        }
        Jump();
        Sprint();
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            isDoubleJump = false;
            dirtParticle.Play();
            if (gameOver) dirtParticle.Stop();
        }
        else if (coll.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            _playerAnim.SetBool("Death_b", true);
            _playerAnim.SetInteger("DeathType_int", 1);
            // VFX
            explosionParticle.Play();
            dirtParticle.Stop();
            // SFX
            _playerAudio.PlayOneShot(crashSound, 1f);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver && !isDoubleJump && _state == 2)
        {
            _playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            _playerAnim.Play("Running_Jump", -1, 0.0f);

            // VFX
            dirtParticle.Stop();
            // SFX
            _playerAudio.PlayOneShot(jumpSound, 1f);
        }

        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !gameOver && !isDoubleJump && _state == 2)
        {
            _playerRb.AddForce(Vector3.up * jumpForce / 2, ForceMode.Impulse);
            isDoubleJump = true;
            _playerAnim.Play("Running_Jump", -1, 0.0f);

            // SFX
            _playerAudio.PlayOneShot(jumpSound, 1f);
        }
    }

    void Sprint()
    {
        isSprint = Input.GetKey(KeyCode.LeftShift);
        if (isSprint) MoveLeft.sprintMult = 2;
        else MoveLeft.sprintMult = 1;
    }
}
