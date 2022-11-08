using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.Rendering;

public class TimeSwitch : MonoBehaviour
{

    public Transform dayTime, nightTime;
    public Volume volumePP;
    public Color fogColorDay, fogColorNight;
    public Material skyBoxDay, skyBoxNight;

    public VolumeProfile dayPP, nightPP;

    private void Start()
    {
        nightTime.transform.position = dayTime.transform.position;
        nightTime.gameObject.SetActive(false);
    }

    public void SwitchTime()
    {
        dayTime.gameObject.SetActive(!dayTime.gameObject.activeSelf);
        nightTime.gameObject.SetActive(!nightTime.gameObject.activeSelf);

        RenderSettings.skybox = dayTime.gameObject.activeSelf ? skyBoxDay : skyBoxNight;
        RenderSettings.fogColor = dayTime.gameObject.activeSelf ? fogColorDay : fogColorNight;

        volumePP.profile = dayTime.gameObject.activeSelf ? dayPP : nightPP;
    }

    public void SwitchTimeFor(float time)
    {
        StartCoroutine(SwitchFor(time));
    }

    public IEnumerator SwitchFor(float time)
    {
        SwitchTime();
        yield return new WaitForSeconds(time);
        SwitchTime();
    }
}
