using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shredder : MonoBehaviour
{

    public Material highlightMaterial;
    public Material normalMaterial;

    private ProgressionManager progressionManager;
    public UnityEvent onShred;
    private Renderer meshRenderer;

    private void Start()
    {
        progressionManager = GameObject.FindGameObjectWithTag("ProgressionManager").GetComponent<ProgressionManager>();
        meshRenderer = GetComponentInChildren<Renderer>();
    }

    public void Highlight(bool highlight)
    {
        meshRenderer.material = highlight ? highlightMaterial : normalMaterial;
    }

    public void Shred()
    {
        GetComponent<Animator>().SetTrigger("Shred");
    }
}
