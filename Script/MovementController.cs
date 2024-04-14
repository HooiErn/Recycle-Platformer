using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] int speed;
    float speedMultiplier;

    [Range(1,10)]
    [SerializeField] float acceleration;

    bool btnPressed;

    bool isWallTouch;
    public LayerMask wallLayer;
    public Transform wallCheckPoint;

    Vector2 relativeTransform;

    public bool isOnPlatform;
    public Rigidbody2D platformRb;

    public ParticleController particleController;
    public PointController pointController;

    AudioManager audioManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pointController = FindObjectOfType<PointController>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        UpdateRelativeTransform();
    }

    private void FixedUpdate()
    {
        UpdateSpeedMultiplier();

        float targetSpeed = speed * speedMultiplier * relativeTransform.x;

        if (isOnPlatform)
        {
            rb.velocity = new Vector2(targetSpeed + platformRb.velocity.x, rb.velocity.y);
        }

        else
        {
            rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
        }

        // me
        // isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.7f, 0.5f), 0, wallLayer);

        // HE
        //isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.2f, 0.2f), 0, wallLayer);

        // XY
        isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.25f, 0.6f), 0, wallLayer);

        if (isWallTouch)
        {
            Flip();
        }
    }

    public void Flip()
    {
        particleController.PlayTouchParticle(wallCheckPoint.position);
        transform.Rotate(0, 180, 0);
        UpdateRelativeTransform();
    }

    public void UpdateRelativeTransform()
    {
        relativeTransform = transform.InverseTransformVector(Vector2.one);
    }


    public void Move(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            btnPressed = true;
        }
        else if (value.canceled)
        {
            btnPressed = false;
        }
    }

    void UpdateSpeedMultiplier()
    {
        if (btnPressed && speedMultiplier < 1f)
        {
            speedMultiplier += Time.deltaTime * acceleration;
        }
        else if ( !btnPressed && speedMultiplier > 0f)
        {
            speedMultiplier -= Time.deltaTime * acceleration;
            if (speedMultiplier < 0f) speedMultiplier = 0f; 
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            Debug.Log("Point collected!");
            audioManager.PlaySFX(audioManager.gain);
            Destroy(collision.gameObject);
            pointController.pointCount++;
        }
        //  if (pointController.pointCount > pointController.highScore)
        {
            //pointController.highScore = pointController.pointCount;
            //PlayerPrefs.SetInt("HighScore", pointController.highScore);
            // pointController.UpdateHighScoreText();
        }
    }
}
