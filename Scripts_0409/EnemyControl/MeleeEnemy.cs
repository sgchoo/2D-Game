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
    Die
}

public class MeleeEnemy : MonoBehaviour
{
    int ranMove;

    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Vector2 target;         // �÷��̾� ��ġ ���� ����

    MeleeEnemyState meState;

    private void Awake()
    {
        meState = MeleeEnemyState.Idle;
        Invoke("RandomDirection", 4f);

        //�÷��̾� ��ġ ����

        target = GameObject.Find("Player").transform.position;
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        sprite = GetComponent<SpriteRenderer>();
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

        //���� �Ÿ� �̻�Ǹ� Idle -> Move
        //if (target)
        //{

        //}
    }

    void Recog()
    {

    }

    void Move()
    {

    }

    void Attack()
    {

    }

    void Die()
    {

    }
}
