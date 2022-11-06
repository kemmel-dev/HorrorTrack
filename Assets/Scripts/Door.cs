using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool locked = false;

    public void SetLocked(bool locked)
    {
        this.locked = locked;
    }
}
