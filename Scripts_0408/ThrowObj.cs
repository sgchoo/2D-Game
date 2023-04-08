using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary : 투척 무기 구현, this 오브젝트가 생성되면 플레이어 방향 참조해서 앞으로 이동
//          PlayerFire에서 this 오브젝트 생성
//          Collider에 닿거나, 일정시간이 지나면 Destroy

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

    //일정 시간이 지나면 Destroy
    void DestroyObject()
    {
        Destroy(gameObject);
    }

    //Collision에 닿으면 Destroy
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

//추후 데미지 줄것
