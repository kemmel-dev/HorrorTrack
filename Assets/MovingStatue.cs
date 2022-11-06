using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingStatue : MonoBehaviour
{
    private Transform parent;

    private GazeAware gazeAware;
    private Player player;
    public float speed;
    public AudioClip soundEffect;

    private void Start()
    {
        gazeAware = GetComponent<GazeAware>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        parent = transform.parent;
    }

    private IEnumerator OnSpotted()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }

    private void FixedUpdate()
    {
        if (! gazeAware.HasGazeFocus)
        {
            Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
            Vector2 parentPos = new Vector2(parent.position.x, parent.position.z);
            Debug.Log(Vector2.Distance(playerPos, parentPos));
            if (Vector2.Distance(playerPos, parentPos) < 2f)
            {
                Transform model = transform.parent.Find("male-atlas");
                if (model == null)
                {
                    model = transform.parent.Find("female-atlas");
                }
                model.GetComponent<Animator>().SetBool("Spotted Player", true);
                transform.parent.parent = player.transform;
                transform.parent.localPosition = new Vector3(0, -0.93f, .94f);
                transform.parent.localEulerAngles = new Vector3(0, 180, 0);
                transform.parent.Find("Human Audio Source").GetComponent<AudioSource>().mute = true;
                player.GetComponent<AudioSource>().PlayOneShot(soundEffect);
                player.GetComponent<PlayerController>().enabled = false;
                player.GetComponent<PlayerMotor>().enabled = false;
                StartCoroutine(OnSpotted());
                Destroy(this);
            }
            else
            {
                Vector2 dir = (playerPos - parentPos).normalized;
                Vector2 delta = dir * Time.deltaTime * speed;
                parent.position = new Vector3(parent.position.x + delta.x, parent.position.y, parent.position.z + delta.y);
            }
        }
    }
}
