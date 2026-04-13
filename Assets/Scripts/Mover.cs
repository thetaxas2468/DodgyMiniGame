using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;
    Vector2 move;
    Rigidbody rb;
    private float jumpForce = 4f;
    private bool isGrounded = true;
    private int jumpCounts = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CollisionHandler.gameWon) return;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        move = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded || (Input.GetKeyDown(KeyCode.Space) && jumpCounts < 2))
        {
            Jump();
            this.jumpCounts++;
        }

    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        rb.velocity = new Vector3(
            movement.x * moveSpeed,
            rb.velocity.y,
            movement.z * moveSpeed
        );

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Cube"))
        {
            isGrounded = true;
            this.jumpCounts = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Cube"))
        {
            isGrounded = false;
        }
    }





    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, this.jumpForce, rb.velocity.z);
    }


}
