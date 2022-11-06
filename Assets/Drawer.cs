using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Drawer : MonoBehaviour
{
    public bool holdsKey;
    private Animator animator;

    public UnityEvent onKeyFind;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    internal void Open()
    {
        if (holdsKey)
        {
            if (animator.GetBool("Open"))
            {
                onKeyFind.Invoke();
                holdsKey = false;
                return;
            }
        }
        animator.SetBool("Open", !animator.GetBool("Open"));

        
    }
}
