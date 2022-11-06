using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class GazeTarget : MonoBehaviour
{

    private GazeAware gazeAware;
    public Transform target;

    public bool visibleOnGaze;
    public float maxRange;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        gazeAware = GetComponent<GazeAware>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < maxRange)
        {
            target.transform.gameObject.SetActive(visibleOnGaze ? gazeAware.HasGazeFocus : !gazeAware.HasGazeFocus);
        }
        else
        {
            target.transform.gameObject.SetActive(false);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, maxRange);
    }
}
