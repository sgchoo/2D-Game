using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
