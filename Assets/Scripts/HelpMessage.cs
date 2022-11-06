using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpMessage : MonoBehaviour
{

    private TextMeshProUGUI textMeshProUGUI;

    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.enabled = false;
    }

    private void Update()
    {
        textMeshProUGUI.enabled = Input.GetKey(KeyCode.Tab);
    }
}
