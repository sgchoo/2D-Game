using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary : ThrowObject�� �����Ѵ�.
//          ThrowObject�� ������ �����ؼ� ���� ��ġ�� ���Ѵ�.

public class PlayerFire : MonoBehaviour
{
    public static int objCount = 0;

    public GameObject throwObject;
    public Transform rightPosition;
    public Transform leftPosition;

    private void Update()
    {
        ObjectThrow();
        Debug.Log(objCount);
    }

    void ObjectThrow()
    {
        if (Input.GetMouseButtonDown(1) && objCount == 1)
        {
            Debug.Log("Throw!");
            GameObject throwObj = Instantiate(throwObject);

            if (gameObject.GetComponent<SpriteRenderer>().flipX == true)
            {
                throwObj.transform.position = leftPosition.position;
            }
            else
            {
                throwObj.transform.position = rightPosition.position;
            }
            --objCount;
        }
    }
}
