using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//编写人：天孤寒羽
public class BallAudio : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource Audio;

    void Start()
    {
        Audio=this.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "ball")
        {
            float value = Vector3.Distance(this.GetComponent<Rigidbody>().velocity, Vector3.zero) / 4>1?1: Vector3.Distance(this.GetComponent<Rigidbody>().velocity, Vector3.zero) / 4;
            Audio.volume = value;
            Audio.PlayOneShot(GameManger.instance.ballToBall);
        }
    }
}
