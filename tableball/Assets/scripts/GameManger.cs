using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//编写人：天孤寒羽
public class GameManger : MonoBehaviour
{

    //母球
    public Transform white;
    //玩家
    public Transform player;
    //能量条
    public Image Value;
    //顶部摄像机
    public GameObject TopCamera;
    //玩家摄像机
    public GameObject PlayerCamera;

    //位置
    [HideInInspector]
    public Vector3 pos;
    //时间
    [HideInInspector]
    public float timer;
    //单例
    public static GameManger instance;

    //音效播放
    private AudioSource audioManger;

    //出杆音效
    public AudioClip cueToBall;
    //球撞球音效
    public AudioClip ballToBall;
    //球进袋音效
    public AudioClip ballTobox;
    //绘制射线
    public LineRenderer line;
    //是否开启射线
    bool isRender;

    //所有球
    public Transform [] Allballs;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
   
    void Start()
    {
        //隐藏鼠标
        Cursor.visible = false;
        audioManger = this.GetComponent<AudioSource>();
        //复位
        Swingball();
    }

    // Update is called once per frame
    void Update()
    {
        if (white.GetComponent<Rigidbody>().IsSleeping())//白球是否静止
        {
            if (Input.GetMouseButtonDown(0))
            {
                //开始计时
                timer = 0f;

            }
            if (Input.GetMouseButton(0))
            {
                //计时
                if (timer > 2)
                {
                    timer = 2f;
                    Value.fillAmount = 1;
                }
                else
                {
                    timer += Time.deltaTime;
                    Value.fillAmount = timer / 2f;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                //发射
                pos = white.position - player.position;
                pos.y = 0f;
                player.GetComponent<PlayerControl>().isAnimator = true;//改变玩家脚本上的播放动画参数
                Value.fillAmount = 0f;
            }

            if (isRender)
            {
                SetLineRender();
            }
            TopCamera.GetComponent<Camera>().enabled = false;
        }
        else
        {
            line.enabled = false;
            TopCamera.GetComponent<Camera>().enabled = true;

        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            isRender = !isRender;
        }

        //unity内运行无效
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //退出游戏
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            //复位其他球
            Swingball();
            //复位白球
            white.GetComponent<WhiteBallController>().Reset();
        }

    }

    //设置射线
    void SetLineRender()
    {
        pos = white.position - player.position;
        pos.y =0;  
        RaycastHit hit;

        if (Physics.Raycast(white.position, pos, out hit,10f)) {
            if (hit.transform.tag == "ball")
            {
                line.enabled = true;
                Vector3 pos = hit.point;
                line.SetPosition(0, white.position - new Vector3(0, 0.03f, 0));
                line.SetPosition(1, pos-new Vector3(0,0.03f,0));
                line.SetPosition(2,(hit.transform.position-pos)*20+hit.transform.position - new Vector3(0, 0.03f, 0));
            }
            else
            {
                line.enabled = false;
            }
        }
    }

    //播放球杆打球的声音
    public void PlayCue() {
        audioManger.PlayOneShot(cueToBall);
    }

    //摆球
    void Swingball() {
        float x = (-1 + Mathf.Sqrt(1 + 8 * Allballs.Length)) / 2;

        for (int i = 0; i < x; i++) 
        {
            for (int j = 0; j <= i; j++) 
            {
                int num = 0;
                for (int k = i; k > 0; k--)
                {
                    num += k;
                }
                Allballs[num + j].position = new Vector3(i / 2f * 0.061f - j * 0.061f, 0.22f, 0.6f + i * Mathf.Sqrt(3) * 0.0305f);
            }
        }
    }
}
