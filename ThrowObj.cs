using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObj : MonoBehaviour
{
    float throwPower = 35f;

    Vector2 dir;

    GameObject player;
    Rigidbody2D rigid;
    

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = GetComponent<GameObject>();
    }

    void Update()
    {
        ThrowObject();
    }

    void ThrowObject()
    {
        if (player.GetComponent<SpriteRenderer>().flipX == true)
        {
            dir = Vector2.left;
        }

        else
        {
            dir = Vector2.right;
        }

        rigid.AddForce(dir * throwPower * Time.deltaTime, ForceMode2D.Impulse);
    }

    void OnCollisionStay(Collision collision)
    {
        Debug.Log("Crush!");
    }
}
