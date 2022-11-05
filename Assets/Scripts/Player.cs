using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{

    public float interactionDistance;
    private Transform rayOrigin;
    public bool collectedCollectible = false;
    private ProgressionManager progressionManager;

    // Start is called before the first frame update
    void Start()
    {
        rayOrigin = Camera.main.transform;
        progressionManager = GameObject.FindGameObjectWithTag("ProgressionManager").GetComponent<ProgressionManager>();
    }

    internal void AttemptInteract()
    {
        Debug.DrawRay(rayOrigin.position, rayOrigin.forward.normalized * interactionDistance, Color.red, 1f);
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward.normalized * interactionDistance, out hit, interactionDistance, LayerMask.GetMask("Interactable")))
        {
            if (hit.collider.CompareTag("Door"))
            {
                if (! hit.collider.GetComponentInParent<Door>().locked)
                {
                    Animator animator = hit.collider.gameObject.GetComponentInParent<Animator>();
                    animator.SetBool("Open", !animator.GetBool("Open"));
                }
            }
            else if (hit.collider.CompareTag("Drawer"))
            {
                Animator animator = hit.collider.gameObject.GetComponent<Animator>();
                animator.SetBool("Open", !animator.GetBool("Open"));
            }
            else if (hit.collider.CompareTag("Collectible"))
            {
                hit.collider.GetComponent<Collectible>().Collect();
            }
            else if (hit.collider.CompareTag("Shredder") && collectedCollectible)
            {
                progressionManager.NextEvent();
                collectedCollectible = false;
            }
        }
    }
}
