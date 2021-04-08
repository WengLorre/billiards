using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//编写人：天孤寒羽
public class PlayerControl : MonoBehaviour
{
    //母球
    private Transform whiteBall;
    //母球钢体
    private Rigidbody Wrigi;
    //球杆
    private Transform cue;
    //摄像机
    private Transform cam;
    //是否可以移动视角
    bool ismove = false;
    //是否开始球杆动画
    [HideInInspector]
    public bool isAnimator = false;

    // Start is called before the first frame update
    void Start()
    {
        //从单例取
        whiteBall = GameManger.instance.white;
        //从子物体取
        cue = this.transform.Find("cue2").transform;
        cam = this.transform.Find("Camera").transform;
        Wrigi = whiteBall.GetComponent<Rigidbody>() ;
        isAnimator = false;
    }



    // Update is called once per frame
    void Update()
    {
        ChangeView();

        AddForceAnimator();

    }

    ///如果球移动，那么不可操作视角变化
    void ChangeView() {
        if (Wrigi.IsSleeping())
        {
            if (ismove)
            {
                // float step = 1 * Time.deltaTime;

                Vector3 pos = whiteBall.position - new Vector3(0.5f, 0, 0.5f);

                pos.y = this.transform.position.y;

                if (Vector3.Distance(this.transform.position, pos) > 0.5f)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, pos, 100f);
                }
                else
                {
                    cue.gameObject.SetActive(true);
                    ismove = false;
                }
            }
            else
            {

                float an = 0;
                float x = Input.GetAxis("Mouse X");

                float y = Input.GetAxis("Mouse Y");

                this.transform.LookAt(whiteBall.position);

                transform.RotateAround(whiteBall.position, Vector3.up, an += x);

                cam.localEulerAngles += new Vector3(-y, 0, 0);

                if (checkAngle(cam.localEulerAngles.x) > 30)
                {
                    cam.localEulerAngles = new Vector3(30, cam.localEulerAngles.y, cam.localEulerAngles.z);
                }

                if (checkAngle(cam.localEulerAngles.x) < 5)
                {
                    cam.localEulerAngles = new Vector3(5, cam.localEulerAngles.y, cam.localEulerAngles.z);
                }

            }

        }
        else
        {
            ismove = true;
        }

    }

    //加动画
    void AddForceAnimator()
    {
        if (isAnimator)
        {
            if (cue.localPosition.z > -0.33f)
            {
                cue.localPosition = new Vector3(cue.localPosition.x, cue.localPosition.y, -0.4f);
                isAnimator = false;
                GameManger.instance.PlayCue();
                cue.gameObject.SetActive(false);
                whiteBall.GetComponent<WhiteBallController>().AddForce(GameManger.instance.timer, GameManger.instance.pos);
            }
            else
            {
                cue.localPosition += new Vector3(0, 0, 1) * Time.deltaTime;
            }
        }
    }

    //检测角度
    float checkAngle(float an) {
        an -= 180;

        if (an > 0)
        {
            return an - 180;
        }
        else
        {
            return an + 180;
        }
    }
}
