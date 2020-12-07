using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovingObject
{
    public void Init()
    {
        moving_queue = new Queue<string>();

        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
}
