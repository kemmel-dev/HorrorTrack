using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{

    public Material highlightMaterial;
    public Material normalMaterial;

    private Collectathon collectathon;

    private void Start()
    {
        collectathon = GameObject.FindGameObjectWithTag("Collectathon").GetComponent<Collectathon>();
    }

    public void Highlight(bool highlight)
    {
        GetComponentInChildren<Renderer>().material = highlight ? highlightMaterial : normalMaterial;
    }

    public void Shred()
    {
        Highlight(false);
        GetComponent<Animator>().SetTrigger("Shred");
        collectathon.ShowNextObject();
    }
}
