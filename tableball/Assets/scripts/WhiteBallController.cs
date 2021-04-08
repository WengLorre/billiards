using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//编写人：天孤寒羽
public class WhiteBallController : MonoBehaviour
{

    float value = 20f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Reset();
    }

    //复位球
    public void Reset() {

        if (transform.position.y < 0.05)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            transform.position = new Vector3(0, 0.22f, -1f);
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

    }

    /// <summary>
    /// 添加球的力
    /// </summary>
    /// <param name="Force">力的大小</param>
    /// <param name="pos">力的方向</param>
    public void AddForce(float Force,Vector3 pos)
    {
        // GameManger.instance.TopCamera.SetActive(false);
        print("力量:"+pos * value * Force / 2f);
        this.GetComponent<Rigidbody>().velocity = pos * value * Force / 2f;
        //this.GetComponent<Rigidbody>().AddForce(pos * 300 * Force / 2f);
    }
}
