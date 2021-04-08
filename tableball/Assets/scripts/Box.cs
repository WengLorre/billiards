using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//编写人：天孤寒羽
public class Box : MonoBehaviour
{
    AudioSource Audio;
    private void Awake()
    {
        Audio=   this.gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ball") {
            Audio.PlayOneShot(GameManger.instance.ballTobox);
        }
    }
}
