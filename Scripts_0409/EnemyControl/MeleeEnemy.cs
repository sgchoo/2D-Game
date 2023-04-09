using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary:
// == enum EnemyState 열거형 상수 사용 ==
//1. 주변을 돌아다니다가 => Idle
//2. 나를 발견하면(일정이상 거리가 되면) => Idle->Recog
//3. 느낌표를 주고(애니메이션 또는 UI) 몇 초뒤에 => Recog
//4. Player의 방향으로 => Recog->Move
//5. 쫒아온 후 => Move
//6. 일정 이상 거리가 되면 => Move -> Attack
//7. 공격 => Attack
//8. 만약 거리가 멀어진다면 재추격 => Attack -> Move
//9. 한대라도 맞으면, Enemy 죽음 => AnyState -> Die
//10.리턴은 없음

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
    Vector2 target;         // 플레이어 위치 저장 변수

    MeleeEnemyState meState;

    private void Awake()
    {
        meState = MeleeEnemyState.Idle;
        Invoke("RandomDirection", 4f);

        //플레이어 위치 변수

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


    // switch 문으로 현 상태 체크
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

    //Enemy 방향 랜덤으로 설정, Invoke함수 사용
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
        //발견 전까지 생성 위치 주변 움직이기
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

        //일정 거리 이상되면 Idle -> Move
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
