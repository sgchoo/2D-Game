using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10;
    public float jumpPower = 10;
    public float slidingSpeed = 5f;
    public float attackSpeed = 15f;

    bool isWallJump;

    int jumpCnt = 0;
    int dashCnt = 0;

    Vector2 dir;
    Vector2 simpleShootingDirection;
    public Vector2 wallJumpPower;
    public Transform wallCheck;
    Rigidbody2D rigid;
    SpriteRenderer sprite;

    float startDashTime = 0.1f;
    float dashSpeed = 20f;

    float currentDashTime;

    bool canDash = true;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        Jump();
        IsBorder();
        WallJump();
        Dash();
        AttackToMouseDirection();
    }

    private void Move()
    {
        //Player move
        float xAxis = Input.GetAxisRaw("Horizontal");

        dir = new Vector2(xAxis * moveSpeed, rigid.velocity.y);

        rigid.velocity = dir;

        //Player Flip Direction
        if (xAxis > 0)
        {
            sprite.flipX = false;
        }
        else if (xAxis == 0)
        {
            sprite.flipX = this.sprite.flipX;
        }

        else
        {
            sprite.flipX = true;
        }
    }

    private void Jump()
    {
        //Player do double jump
        if (Input.GetButtonDown("Jump") && jumpCnt < 2)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
            ++jumpCnt;
        }
    }

    private void WallJump()
    {
        //Player do wall jump
        //during touch the wall, player sliding function will fix.
        float h = Input.GetAxis("Horizontal");

        Collider2D wallCol = Physics2D.OverlapCapsule(wallCheck.position, Vector2.one, CapsuleDirection2D.Vertical, 0, LayerMask.GetMask("Wall"));

        if (wallCol != null && Input.GetKey(KeyCode.Space) && isWallJump == false)
        {
            isWallJump = true;
            rigid.velocity = new Vector2(rigid.velocity.x, Mathf.Clamp(rigid.velocity.y, -slidingSpeed, float.MaxValue));
        }

        if (wallCol != null && Input.GetKeyUp(KeyCode.Space) && isWallJump == true)
        {
            rigid.velocity = new Vector2(-h * wallJumpPower.x, wallJumpPower.y);
            isWallJump = false;
            jumpCnt++;
            dashCnt = 0;
        }
    }

    void IsBorder()
    {
        //ground check for limit jump
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D groundHit = Physics2D.Raycast(rigid.position, Vector3.down, 1.5f, LayerMask.GetMask("Floor"));

            if (groundHit.collider != null)
            {
                jumpCnt = 0;
                dashCnt = 0;
            }
        }
    }

    private void Dash()
    {
        if (canDash && Input.GetKeyDown(KeyCode.LeftShift) && dashCnt < 1)
        {
            dashCnt++;
            if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(Dash(Vector2.up));
            }

            else if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dash(Vector2.left));
            }

            else if (Input.GetKey(KeyCode.S))
            {
                StartCoroutine(Dash(Vector2.down));
            }

            else if (Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(Vector2.right));
            }
        }
    }

    IEnumerator Dash(Vector2 direction)
    {
        canDash = false;
        currentDashTime = startDashTime;

        while (currentDashTime > 0f)
        {
            currentDashTime -= Time.deltaTime;

            rigid.velocity = direction * dashSpeed;

            yield return null;
        }

        rigid.velocity = Vector2.zero;


        yield return new WaitForSeconds(0.3f);

        canDash = true;
    }

    void AttackToMouseDirection()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseCursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 mouseDirection = (mouseCursorWorldPosition - this.transform.position).normalized;

            Vector2 simpleShootingDirection = Vector2.zero;
            if (Mathf.Abs(mouseDirection.y) > Mathf.Abs(mouseDirection.x))
            {
                if (mouseDirection.y > 0)
                {
                    simpleShootingDirection = (Vector2)mouseDirection;
                }
                else
                {
                    simpleShootingDirection = (Vector2)mouseDirection;
                }
            }

            else
            {
                if (mouseDirection.x > 0)
                {
                    simpleShootingDirection = (Vector2)mouseDirection;
                }
                else
                {
                    simpleShootingDirection = (Vector2)mouseDirection;
                }
            }

            rigid.velocity = new Vector2(simpleShootingDirection.x * attackSpeed, simpleShootingDirection.y * attackSpeed);

            print(rigid.velocity);
        }
    }
}
