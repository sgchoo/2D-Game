using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary : 설정해둔 레이어의 오브젝트가 트리거되고
//          F키를 누르면 트리거된 오브젝트는 사라지고
//          PlayerFire의 objCount의 갯수가 증가된다.

public class GetObject : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Contact");

        if (Input.GetKeyDown(KeyCode.F))
        {
            Destroy(gameObject);
            PlayerFire.objCount++;
        }
    }
}
