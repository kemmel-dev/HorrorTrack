using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Replacer : MonoBehaviour
{


    public List<GameObject> toPickFrom;

    public List<Transform> objectsToReplace;

    public void Start()
    {
        ReplacePrefabs();
    }

    private void ReplacePrefabs()
    {
        int i = 0;
        foreach (Transform toReplace in objectsToReplace)
        {
            Transform parent = toReplace.parent;
            GameObject replacement = PrefabUtility.InstantiatePrefab(toPickFrom[i++]) as GameObject;
            replacement.transform.position = toReplace.position;
            replacement.transform.rotation = toReplace.rotation;
            replacement.transform.localScale = toReplace.localScale;
            Destroy(toReplace.gameObject);
            replacement.transform.parent = parent;
        }
    }
}
