using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary : �����ص� ���̾��� ������Ʈ�� Ʈ���ŵǰ�
//          FŰ�� ������ Ʈ���ŵ� ������Ʈ�� �������
//          PlayerFire�� objCount�� ������ �����ȴ�.

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
