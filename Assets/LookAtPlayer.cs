using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    private Player player;

    private Transform toRotate;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        toRotate = transform.Find("female-atlas/Female/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManNeck");
        if (toRotate == null)
        {
            toRotate = transform.Find("male-atlas/Male/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManNeck");
        }
    }

    // Update is called once per frame
    void Update()
    {
        toRotate.LookAt(Camera.main.transform);
    }
}
