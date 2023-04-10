using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary:
// == enum EnemyState ������ ��� ��� ==
//1. �ֺ��� ���ƴٴϴٰ� => Idle
//2. ���� �߰��ϸ�(�����̻� �Ÿ��� �Ǹ�) => Idle->Recog
//3. ����ǥ�� �ְ�(�ִϸ��̼� �Ǵ� UI) �� �ʵڿ� => Recog
//4. Player�� �������� => Recog->Move
//5. �i�ƿ� �� => Move
//6. ���� �̻� �Ÿ��� �Ǹ� => Move -> Attack
//7. ���� => Attack
//8. ���� �Ÿ��� �־����ٸ� ���߰� => Attack -> Move
//9. �Ѵ�� ������, Enemy ���� => AnyState -> Die
//10.������ ����

public enum MeleeEnemyState
{
    Idle,
    Recog,
    Move,
    Attack,
    Hesitate,
    Die
}

public class MeleeEnemy : MonoBehaviour
{
    int ranMove;
    int moveSpeed = 3;

    Rigidbody2D rigid;
    SpriteRenderer sprite;
    GameObject target;         // �÷��̾� ��ġ ���� ����

    Vector2 originPos;

    MeleeEnemyState meState;

    private void Awake()
    {
        meState = MeleeEnemyState.Idle;
        Invoke("RandomDirection", 4f);

        //�÷��̾� ��ġ ����

        target = GameObject.Find("Player");
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        sprite = GetComponent<SpriteRenderer>();

        originPos = transform.position;
    }

    private void Update()
    {
        StateCheck();
        GroundCheck();
    }


    // switch ������ �� ���� üũ
    void StateCheck()
    {
        switch (meState)
        {
            case MeleeEnemyState.Idle:
                Idle();
                break;

            case MeleeEnemyState.Recog:
                Recog();
                break;

            case MeleeEnemyState.Move:
                Move();
                break;

            case MeleeEnemyState.Attack:
                Attack();
                break;

            case MeleeEnemyState.Hesitate:
                Hesitate();
                break;

            case MeleeEnemyState.Die:
                Die();
                break;
        }
    }

    //Enemy ���� �������� ����, Invoke�Լ� ���
    void RandomDirection()
    {
        ranMove = Random.Range(-1, 2);

        Invoke("RandomDirection", 4f);
    }

    void GroundCheck()
    {
        Vector2 frontVec = new Vector2(rigid.position.x + ranMove, rigid.position.y);

        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D groundHit = Physics2D.Raycast(frontVec, Vector3.down, 1.5f, LayerMask.GetMask("Floor"));

        if (groundHit.collider == null)
        {
            ranMove *= -1;
        }
    }

    void Idle()
    {
        //�߰� ������ ���� ��ġ �ֺ� �����̱�
        rigid.velocity = new Vector2(ranMove, rigid.velocity.y);

        // �ִϸ��̼����� flip ���� ����
        if (ranMove > 0)
        {
            sprite.flipX = false;
        }

        else if (ranMove == 0)
        {
            sprite.flipX = this.sprite.flipX;
        }

        else
        {
            sprite.flipX = true;
        }

        Debug.Log("Idle");

        //���� �Ÿ� �̻�Ǹ� Idle -> Recog
        if (Vector2.Distance(target.transform.position, transform.position) < 7f)
        {
            meState = MeleeEnemyState.Recog;
            Debug.Log("Idle -> Recog");
        }
    }

    void Recog()
    {
        //ĳ���� ���� ����ǥ�� ����(�ִϸ��̼� or UI) �� �ʵڿ� Recog -> Move
        Debug.Log("Recog");
        //�ִϸ��̼� �Ǵ� UI�ڸ�

        Invoke("ChasePlayer", 1f);

    }

    //���� ǥ�ð� ��Ÿ�� �� Invoke�� �����ð� �ڿ� Move�� �̵�
    void ChasePlayer()
    {
        meState = MeleeEnemyState.Move;
        Debug.Log("Recog -> Move");
    }

    void Move()
    {
        //���� ���� �Ÿ����� �̵�
        if(Vector2.Distance(target.transform.position, transform.position) > 1f)
        {
            Vector2 dir = new Vector2(target.transform.position.x - transform.position.x, rigid.velocity.y);
            dir.Normalize();
            rigid.velocity = dir * moveSpeed;

            // �ִϸ��̼����� flip ���� ����
            if (dir.x > 0)
            {
                sprite.flipX = false;
            }

            else if (dir.x == 0)
            {
                sprite.flipX = this.sprite.flipX;
            }

            else
            {
                sprite.flipX = true;
            }
        }

        //�̵� �Ϸ� ������ Move -> Attack
        else
        {
            meState = MeleeEnemyState.Attack;
            Debug.Log("Move -> Attack");
        }
    }

    void Attack()
    {
        //���� �Ÿ��� ������ ����
        if (Vector2.Distance(target.transform.position, transform.position) < 1f)
        {
            Debug.Log("����");
        }

        //���� �� ��ġ�� ó�� ��ġ�� �������� ���� ������ ����ٸ�
        //�ڸ����� ���߰� Hesitate -> Idle
        else if (Vector2.Distance(transform.position, originPos) > 20f)
        {
            meState = MeleeEnemyState.Idle;
            Debug.Log("Attack -> Hesitate");
        }

        //����� ���߰� ����
        else
        {
            Vector2 dir = new Vector2(target.transform.position.x - transform.position.x, rigid.velocity.y);
            dir.Normalize();
            rigid.velocity = dir * moveSpeed;
            Debug.Log("���߰�");

            // �ִϸ��̼����� flip ���� ����
            if (dir.x > 0)
            {
                sprite.flipX = false;
            }

            else if (dir.x == 0)
            {
                sprite.flipX = this.sprite.flipX;
            }

            else
            {
                sprite.flipX = true;
            }
        }
    }

    void Hesitate()
    {
        //���� �� ��ġ�� ó�� ��ġ�� �������� ���� ������ ����ٸ�
        //�ڸ����� ���߰� Hesitate -> Idle
        if (Vector2.Distance(transform.position, originPos) > 20f)
        {
            meState = MeleeEnemyState.Idle;
            Debug.Log("Attack -> Hesitate");
        }
    }

    void Die()
    {

    }
}

//���߿� �ִϸ��̼����� flip ���� ����.
//Ray ����� ������� ���� ���� ���� ����.
