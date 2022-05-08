using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    private Rigidbody2D rb;

    [SerializeField] private bool isGrounded;

    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2.5f;

    private bool justLanded = false;

    [Header("Ground Settings")]
    [SerializeField] private Transform[] groundChecks;
    [SerializeField] private float coyoteTime = 0.1f;

    public static Action playerGrounded;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Better jump https://www.youtube.com/watch?v=7KiK0Aqtmzc&t=9s
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.up * jumpVelocity;
            AudioManager.instance?.PlayTarget("Jump");
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {

        if (Physics2D.Linecast(transform.position, groundChecks[0].position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundChecks[1].position, 1 << LayerMask.NameToLayer("Ground")))
        {
            StopAllCoroutines();
            isGrounded = true;
            playerGrounded?.Invoke();

            if (justLanded)
            {
                AudioManager.instance?.PlayTarget("Land");
                justLanded = false;
            }
        }
        else
        {
            StartCoroutine(groundedFalse());
        }
        moveCharacter(moveSpeed);
    }

    private void moveCharacter(float speed)
    {
        float moveDirection = Input.GetAxis("Horizontal");
        //print("Absolute Speed: " + Mathf.Abs(moveDirection));

        if (Input.GetButton("Horizontal"))
        {
            if (moveDirection > 0)
            {
                transform.localScale = new Vector2(1, 1);
                rb.velocity = new Vector2(1 * speed, rb.velocity.y);
            }
            else if (moveDirection < 0)
            {
                transform.localScale = new Vector2(-1, 1);
                rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    IEnumerator groundedFalse()
    {
        yield return new WaitForSeconds(coyoteTime);
        isGrounded = false;
        justLanded = true;
    }
}
