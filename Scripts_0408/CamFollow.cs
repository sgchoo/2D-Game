using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary : 카메라가 플레이어 이동에 맞게 위치 변경

public class CamFollow : MonoBehaviour
{
    public Transform target;
    Vector3 offset;

    void Start()
    {
        offset = this.transform.position - target.position;
    }

    void Update()
    {
        Vector3 targetPos = target.position + offset;

        transform.position = targetPos;

        transform.LookAt(target);
    }
}
