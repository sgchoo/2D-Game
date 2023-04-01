using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerManager
{

    public void Update()
    {
        Move();
        Jump();
        Turn();
        IsBorder();
        WallJumpCheck();
    }
}
