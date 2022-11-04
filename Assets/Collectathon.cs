using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectathon : MonoBehaviour
{
    public List<Collectible> collectiblesInOrder;
    private Queue<Collectible> collectibles;

    private void Start()
    {
        PrepareCollectibles();
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
            collectibles.Dequeue();
            if (collectibles.Count > 0)
            {
                collectibles.Peek().gameObject.SetActive(true);
            }
            else
            {
                Finish();
            }
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
