using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    Camera dayCamera;
    Camera nightCamera;

    public Material daySkyBox;
    public Material nightSkyBox;
    public Color32 dayFogColour;
    public Color32 nightFogColour;

    // called zero
    void Awake()
    {
        dayCamera = Camera.main;
    }

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            nightCamera = GameObject.FindGameObjectWithTag("NightCamera").GetComponent<Camera>();
            dayCamera.GetComponent<UniversalAdditionalCameraData>().cameraStack.Add(nightCamera);
            nightCamera.cullingMask = LayerMask.GetMask("Nothing");
            nightCamera.transform.parent = dayCamera.transform;
            nightCamera.transform.localPosition = Vector3.zero;
            nightCamera.transform.localRotation = Quaternion.identity;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapCullingMasks();
        }
    }

    void SwapCullingMasks()
    {
        dayCamera.cullingMask = dayCamera.cullingMask == LayerMask.GetMask("DayTime") ? LayerMask.GetMask("Nothing") : LayerMask.GetMask("DayTime");
        nightCamera.cullingMask = nightCamera.cullingMask == LayerMask.GetMask("NightTime") ? LayerMask.GetMask("Nothing") : LayerMask.GetMask("NightTime");

        RenderSettings.skybox = dayCamera.cullingMask == LayerMask.GetMask("DayTime") ? daySkyBox : nightSkyBox;
        RenderSettings.fogColor = dayCamera.cullingMask == LayerMask.GetMask("DayTime") ? dayFogColour : nightFogColour;
    }

    // called when the game is terminated
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
