using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary : ��ô ���� ����, this ������Ʈ�� �����Ǹ� �÷��̾� ���� �����ؼ� ������ �̵�
//          PlayerFire���� this ������Ʈ ����
//          Collider�� ��ų�, �����ð��� ������ Destroy

public class ThrowObj : MonoBehaviour
{
    float throwPower = 120f;

    Vector2 dir;
    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        GetDirection();
    }

    void Update()
    {
        ThrowObject();
    }

    private void LateUpdate()
    {
        Invoke("DestroyObject", 3f);
    }

    void GetDirection()
    {
        if (GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX == true)
        {
            dir = Vector2.left;
        }

        else
        {
            dir = Vector2.right;
        }
    }

    void ThrowObject()
    {
        rigid.AddForce(dir * throwPower * Time.deltaTime, ForceMode2D.Impulse);
    }

    //���� �ð��� ������ Destroy
    void DestroyObject()
    {
        Destroy(gameObject);
    }

    //Collision�� ������ Destroy
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

//���� ������ �ٰ�
