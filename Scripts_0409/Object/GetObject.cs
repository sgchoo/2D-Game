using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary : 설정해둔 레이어의 오브젝트가 트리거되고
//          F키를 누르면 트리거된 오브젝트는 사라지고
//          PlayerFire의 objCount의 갯수가 증가된다.

public class GetObject : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.F) && collision.gameObject.layer == 10)
        {
            Destroy(collision.gameObject);

            PlayerFire.objCount++;
        }
    }
}
