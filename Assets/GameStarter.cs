using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{

    public TMP_InputField inputField;

    public void StartGame()
    {
        PlayerName.name = inputField.text;
        SceneManager.LoadScene(1);
    }
}
