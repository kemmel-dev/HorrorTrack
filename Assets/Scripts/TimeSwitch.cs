using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSwitch : MonoBehaviour
{

    public Transform dayTime, nightTime;
    public Color fogColorDay, fogColorNight;
    public Material skyBoxDay, skyBoxNight;

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
