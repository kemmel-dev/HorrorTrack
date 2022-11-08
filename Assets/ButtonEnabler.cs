using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEnabler : MonoBehaviour
{


    public TMP_InputField inputField;
    public Button button;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        button.gameObject.SetActive(inputField.text.Length != 0);
        
    }
}
