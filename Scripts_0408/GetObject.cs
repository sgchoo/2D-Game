using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary : �����ص� ���̾��� ������Ʈ�� Ʈ���ŵǰ�
//          FŰ�� ������ Ʈ���ŵ� ������Ʈ�� �������
//          PlayerFire�� objCount�� ������ �����ȴ�.

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
