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
    Hesitate,
    Die
}

public class MeleeEnemy : MonoBehaviour
{
    int ranMove;
    int moveSpeed = 3;

    Rigidbody2D rigid;
    SpriteRenderer sprite;
    GameObject target;         // 플레이어 위치 저장 변수

    Vector2 originPos;

    MeleeEnemyState meState;

    private void Awake()
    {
        meState = MeleeEnemyState.Idle;
        Invoke("RandomDirection", 4f);

        //플레이어 위치 변수

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

            case MeleeEnemyState.Hesitate:
                Hesitate();
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

        // 애니메이션으로 flip 조절 예정
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

        //일정 거리 이상되면 Idle -> Recog
        if (Vector2.Distance(target.transform.position, transform.position) < 7f)
        {
            meState = MeleeEnemyState.Recog;
            Debug.Log("Idle -> Recog");
        }
    }

    void Recog()
    {
        //캐릭터 위에 느낌표를 띄우고(애니메이션 or UI) 몇 초뒤에 Recog -> Move
        Debug.Log("Recog");
        //애니메이션 또는 UI자리

        Invoke("ChasePlayer", 1f);

    }

    //놀라는 표시가 나타난 후 Invoke로 지연시간 뒤에 Move로 이동
    void ChasePlayer()
    {
        meState = MeleeEnemyState.Move;
        Debug.Log("Recog -> Move");
    }

    void Move()
    {
        //공격 가능 거리까지 이동
        if(Vector2.Distance(target.transform.position, transform.position) > 1f)
        {
            Vector2 dir = new Vector2(target.transform.position.x - transform.position.x, rigid.velocity.y);
            dir.Normalize();
            rigid.velocity = dir * moveSpeed;

            // 애니메이션으로 flip 조절 예정
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

        //이동 완료 됐으면 Move -> Attack
        else
        {
            meState = MeleeEnemyState.Attack;
            Debug.Log("Move -> Attack");
        }
    }

    void Attack()
    {
        //일정 거리로 들어오면 공격
        if (Vector2.Distance(target.transform.position, transform.position) < 1f)
        {
            Debug.Log("공격");
        }

        //만약 내 위치가 처음 위치와 비교했을때 지정 범위를 벗어났다면
        //자리에서 멈추고 Hesitate -> Idle
        else if (Vector2.Distance(transform.position, originPos) > 20f)
        {
            meState = MeleeEnemyState.Idle;
            Debug.Log("Attack -> Hesitate");
        }

        //벗어나면 재추격 시작
        else
        {
            Vector2 dir = new Vector2(target.transform.position.x - transform.position.x, rigid.velocity.y);
            dir.Normalize();
            rigid.velocity = dir * moveSpeed;
            Debug.Log("재추격");

            // 애니메이션으로 flip 조절 예정
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
        //만약 내 위치가 처음 위치와 비교했을때 지정 범위를 벗어났다면
        //자리에서 멈추고 Hesitate -> Idle
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

//나중에 애니메이션으로 flip 조절 예정.
//Ray 제대로 적용되지 않음 추후 수정 예정.
