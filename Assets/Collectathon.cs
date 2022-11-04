using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectathon : MonoBehaviour
{
    public List<Collectible> collectiblesInOrder;
    private Queue<Collectible> collectibles;
    private Player player;

    private void Start()
    {
        PrepareCollectibles();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    internal void ShowNextObject()
    {
        collectibles.Peek().gameObject.SetActive(true);
    }

    private void PrepareCollectibles()
    {
        collectibles = new Queue<Collectible>(collectiblesInOrder);
        NotifyCollectibles();
        SetActivities();
    }

    private void SetActivities()
    {
        for (int i = 0; i < collectibles.Count; i++)
        {
            Collectible current = collectibles.Dequeue();
            current.gameObject.SetActive(i == 0);
            collectibles.Enqueue(current);
        }
    }

    private void NotifyCollectibles()
    {
        for (int i = 0; i < collectibles.Count; i++)
        {
            Collectible current = collectibles.Dequeue();
            current.SetCollectathon(this);
            collectibles.Enqueue(current);
        }
    }

    internal void MarkAsCollected(Collectible collectible)
    {
        if (collectibles.Peek() == collectible)
        {
            player.collectedCollectible = true;
            collectibles.Dequeue();
            if (collectibles.Count == 0)
                Finish();
        }
        else
        {
            throw new Exception("Collected a collectible out of order!");
        }
    }

    private void Finish()
    {
        Debug.Log("Collectathon finished!");
    }
}
