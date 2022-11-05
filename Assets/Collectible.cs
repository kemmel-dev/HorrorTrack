using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{

    public UnityEvent onCollect;

    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Collect()
    {
        if (onCollect != null)
        {
            onCollect.Invoke();
        }
        player.collectedCollectible = true;
        Destroy(this.gameObject);
    }
}
