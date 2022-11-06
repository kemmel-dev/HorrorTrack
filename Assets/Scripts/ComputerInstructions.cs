using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputerInstructions : MonoBehaviour
{

    public List<string> instructionsInOrder = new List<string>();
    private TextMeshPro textMesh;

    private int instructionIndex = 0;
    
    private void Start()
    {
       textMesh = GetComponent<TextMeshPro>();
       textMesh.SetText(instructionsInOrder[instructionIndex]);
    }

    public void NextInstruction()
    {
        textMesh.SetText(instructionsInOrder[++instructionIndex]);
    }
}
