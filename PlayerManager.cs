using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 5;
    public int jumpPower = 5;

    public bool isJump = false;
    public bool isDash = false;
    public bool isGrounded = false;
    protected bool isBorder = false;
    protected bool isWall = false;

    public volatile float yVelocity = 0f;
    protected float getGravity = -0.1f;

    protected int jumpCount = 0;

    Rigidbody rb;
    Vector3 dir;
    RaycastHit hitInfo;

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //player move
    protected virtual void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");

        dir = new Vector3(h, 0, 0).normalized;

        if (!isBorder)
        {
            transform.position += dir * moveSpeed * Time.deltaTime;
        } 
    }

    protected void IsBorder()
    {
        Ray ray = new Ray(transform.position, dir);
        hitInfo = new RaycastHit();
        if (Physics.Raycast(ray, out hitInfo, 0.25f))
        {
            if (hitInfo.collider.tag == "Wall")
            {
                isBorder = true;
            }
        }
        else
        {
            isBorder = false;
        }
    }

    protected void Turn()
    {
        transform.LookAt(transform.position + dir);
    }

    //player jump
    protected virtual void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCount < 2 || (isJump == false && isGrounded == true))
            {
                isJump = true;
                isGrounded = false;
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                jumpCount++;
            }
        }

        if (isJump == true)
        {
            yVelocity += getGravity * Time.deltaTime;
            rb.velocity += new Vector3(0, yVelocity, 0);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        //play double jump & bool value change
        if(collision.gameObject.tag == "Floor")
        {
            isJump = false;
            isGrounded = true;
            jumpCount = 0;
            yVelocity = 0;
        }
    }
}
