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

public class RangeEnemy : MonoBehaviour
{
    enum RangeEnemyState
    {
        Idle,
        Recog,
        Move,
        Attack,
        Die
    }

    RangeEnemyState reState;
}
