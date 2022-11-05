using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.Events;

public class GazeTargetTimed : MonoBehaviour
{

    private GazeAware gazeAware;
    private Animator animator;
    private Player player;

    public AudioClip soundEffect;

    public float triggerTime = 2f;
    private float timeStamp = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gazeAware = GetComponent<GazeAware>();
        Transform model = transform.parent.Find("male-atlas");
        if (model == null)
        {
            model = transform.parent.Find("female-atlas");
        }
        animator = model.GetComponent<Animator>();
    }


    private void Update()
    {
        if (!gazeAware.HasGazeFocus)
        {
            timeStamp = Time.time;
        }
        else
        {
            if (Time.time > timeStamp + triggerTime)
            {
                animator.SetBool("Spotted Player", true);
                transform.parent.parent = player.transform;
                transform.parent.localPosition = new Vector3(0, -0.93f, .94f);
                transform.parent.localEulerAngles = new Vector3(0, 180, 0);
                transform.parent.Find("Human Audio Source").GetComponent<AudioSource>().mute = true;
                player.GetComponent<AudioSource>().PlayOneShot(soundEffect);
                player.GetComponent<PlayerController>().enabled = false;
                player.GetComponent<PlayerMotor>().enabled = false;
                Destroy(this);
            }
        }
    }
}
