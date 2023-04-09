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
