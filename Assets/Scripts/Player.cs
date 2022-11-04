using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{

    public float interactionDistance;
    private Transform rayOrigin;

    // Start is called before the first frame update
    void Start()
    {
        rayOrigin = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void AttemptInteract()
    {
        Debug.DrawRay(rayOrigin.position, rayOrigin.forward.normalized * interactionDistance, Color.red, 1f);
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward.normalized * interactionDistance, out hit, interactionDistance, LayerMask.GetMask("Interactable")))
        {
            if (hit.collider.CompareTag("Door") || hit.collider.CompareTag("Drawer"))
            {
                Debug.Log(hit.collider.gameObject.name);
                Animator animator = hit.collider.gameObject.GetComponent<Animator>();
                Debug.Log(animator);
                animator.SetBool("Open", ! animator.GetBool("Open"));
            }

            if (hit.collider.CompareTag("Collectible"))
            {
                hit.collider.GetComponent<Collectible>().Collect();
            }
        }
    }
}
