using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{

    private Collectathon collectathon;
    public UnityEvent onCollect;
 
    public void SetCollectathon(Collectathon collectathon)
    {
        this.collectathon = collectathon;
    }

    public void Collect()
    {
        collectathon.MarkAsCollected(this);
        if (onCollect != null)
        {
            onCollect.Invoke();
        }
        Destroy(this.gameObject);
    }
}
