//#define DEBUG_MODE

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 控制玩家行动
/// </summary>
public class PlayerControl : MonoBehaviour
{
    /// <summary>
    /// 炸弹物体
    /// </summary>
    public GameObject bomb;

    /// <summary>
    /// 炸弹生成位置
    /// </summary>
    public GameObject bumb;

    /// <summary>
    /// 炸弹释放CD，默认为2
    /// </summary>
    //private Rigidbody mmRigidbody;
    private float CD = 2;

    /// <summary>
    /// 计时器，用于延时处理，默认为1
    /// </summary>
    private float CD2 = 1;

    /// <summary>
    /// 脚步音效CD
    /// </summary>
    private float time = 0f;

    /// <summary>
    /// 玩家方向向量，用于控制玩家移动方向
    /// </summary>
    private Vector3 dir;

    /// <summary>
    /// 炸弹爆炸状态
    /// </summary>
    private bool isbumb;

    /// <summary>
    /// 游戏胜利UI界面
    /// </summary>
    public GameObject win;

    /// <summary>
    /// 音频播放器
    /// </summary>
    private AudioSource player;

    /// <summary>
    /// 草地走路音效
    /// </summary>
    [Header("走路音效设定")]
    public AudioClip[] terrain_audio_clips;

    /// <summary>
    /// 石板走路音效
    /// </summary>
    public AudioClip[] floor_audio_clips;

    /// <summary>
    /// 木板走路音效设定
    /// </summary>
    public AudioClip[] wood_audio_clips;

    /// <summary>
    /// 背景音乐播放器
    /// </summary>
    private AudioSource BackgroundMusic;

    /// <summary>
    /// 玩家速度，默认为6
    /// </summary>
    [Header("玩家速度设定")]
    public float player_speed = 6f;

    /// <summary>
    /// 敌人数量
    /// </summary>
    [HideInInspector] public int enemy_num;

    /// <summary>
    /// 获取虚拟轴以赋予玩家方向
    /// </summary>
    private void GetAxis()
    {
        //获取水平虚拟轴
        float horizontal = Input.GetAxis("Horizontal");
        //获取垂直虚拟轴
        float vertical = Input.GetAxis("Vertical");
        //根据垂直和水平虚拟轴确定玩家方向
        dir = new Vector3(horizontal, 0, vertical);
    }

    /// <summary>
    /// 检测F键，用于放炸弹
    /// </summary>
    private void GetKey()
    {
        //通过GetKeyDowm方式检测按键F
        if (Input.GetKeyDown(KeyCode.F)) isbumb = true;
        //GetKey()：当通过你所指定的按键被用户按住时返回true，记住！是按住，就是长按的意识，比如你想控制角色在你按住方向键时移动，那么就是用GetKey()
        //GetKeyDown()：当通过你所按下指定名称的按键时的那一帧时返回true，记住！是那一帧，就一下的事情，不管你按多久，只是在你按下的那一瞬间
        //GetKeyUp()：在通过你释放（按键弹起时）给定名字的按键的那一帧返回true，记住！是那一帧，就一下的事情。
    }

    /// <summary>
    /// 控制放炸弹
    /// </summary>
    private void PutBomb()
    {
        if (isbumb)
        {
            //已经按下F键释放炸弹，因此将状态更改
            isbumb = false;
            if (CD > 2)
            {
                //CD计算时间大于2时炸弹冷却结束，可以再次释放炸弹，并把CD时间清空开始从先计算CD
                CD = 0;
                //在bumb的位置生成炸弹
                Instantiate(bomb, bumb.transform.position, bumb.transform.rotation);
            }
        }
    }

    /// <summary>
    /// 计算CD
    /// </summary>
    private void CDtime()
    {
        //计算炸弹CD时间
        CD += Time.deltaTime;
        //计算走路音效CD时间
        CD2 += Time.deltaTime;
        //计算时间使用的是增量时间，因不同设备的帧率是不一样的，而unity里的方法是每帧调用一次的，若按普通的秒来算，想导致计算不准确且不同设备的计算结果也不太同
        //若帧率为1秒30帧，则增量时间为1/帧数 s，这样就能保证不同设备计算的时间都是一样的，因为虽然帧率不同，但都是1秒的帧率
    }

    /// <summary>
    /// 控制玩家移动
    /// </summary>
    private void Move()
    {
        if (dir != Vector3.zero)
        {
            //创建一个四元数来保存看向dir方向需要的旋转量
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            //也可以这样旋转，但不自然，transform.rotation = Quaternion.LookRotation(dir);
            //因为是旋转移动，所以通过球形插值进行旋转，使旋转更加平滑，角速度为10度/s
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            //碰撞更自然的运动方式，但需要刚体组件，rigidbody.MovePosition(rigidbody.position + speed * Time.deltaTime);
            //mmRigidbody.MovePosition(transform.position + dir * 0.15f);
            //向前方向进行移动，速度为6m/s，或者说6个单位/s
            transform.Translate(Vector3.forward * player_speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 控制游戏是否胜利
    /// </summary>
    private void GameWin()
    {
        //获取所有敌人
        //GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        //若敌人数量为0则代表游戏胜利
        if (enemy_num == 0)
        {
            //开始计时
            time += Time.fixedUnscaledDeltaTime;
            //暂停游戏时间
            Time.timeScale = 0;
            //停止背景音乐
            BackgroundMusic.Stop();
            //使Esc组件失效
            GetComponent<Esc>().enabled = false;
            //激活胜利UI
            win.SetActive(true);
            //等待10s才可按键返回主界面
            if (time > 10)
            {
                //按下任意键使界面跳转到开始界面
                if (Input.anyKey)
                {
                    time = 0;
                    SceneManager.LoadScene(0);
                    Time.timeScale = 1;
                }
            }
        }
    }

    private void Start()
    {
        player = GetComponent<AudioSource>();
        //mmRigidbody = gameObject.GetComponent<Rigidbody>();
        BackgroundMusic = GameObject.Find("MusicTrigger").GetComponent<AudioSource>();
        enemy_num = GameObject.FindGameObjectsWithTag("Enemy").Length;
#if DEBUG_MODE
        if (!player)
        {
            Debug.LogError("未获取到AudioSource组件");
            return;
        }
        if (!BackgroundMusic)
        {
            Debug.LogError("未获取到背景音乐播放器");
            return;
        }
        if (!PlayerControlDeBug())
        {
            Debug.LogError("初始化失败");
            return;
        }
#endif
    }

    private void Update()
    {
        GetAxis();
        GetKey();
        CDtime();
        GameWin();
    }

    private void FixedUpdate()
    {
        PutBomb();
        Move();
        //GameWin();
    }

    /// <summary>
    /// 随机播放脚步音效
    /// </summary>
    /// <param name="s">接触的物体名称</param>
    private void play_clip(string s)
    {
        //若为terrain地块，则播放对应的随机走路音效
        if (s == "Terrain") player.PlayOneShot(terrain_audio_clips[Random.Range(0, terrain_audio_clips.Length)]);
        else
        {
            if (s == "floor") player.PlayOneShot(floor_audio_clips[Random.Range(0, floor_audio_clips.Length)]);
            else player.PlayOneShot(wood_audio_clips[Random.Range(0, wood_audio_clips.Length)]);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (CD2 > 0.5)
        {
            //脚步音效CD冷却完毕则播放脚步音效，播放完后清0再次进行冷却
            CD2 = 0;
            //玩家碰到的物体的名称是否为Terrain，是则播放对应音效
            if (collision.gameObject.name == "Terrain") play_clip("Terrain");
            else
            {
                if (collision.gameObject.transform.parent != null)
                {
                    if (collision.gameObject.transform.parent.gameObject.tag == "floor") play_clip("floor");
                    else if (collision.gameObject.transform.tag == "wood") play_clip("wood");
                }
            }
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// PlayerControl初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool PlayerControlDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(bomb, "bomb/炸弹物体未初始化");
        checkNull.Check(bumb, "bumb/生成位置未初始化");
        checkNull.Check(win, "win/胜利UI未初始化");
        checkNull.Check(player, "player/音频播放器未初始化");
        checkNull.Check(terrain_audio_clips, "terrain_audio_clips/草地走路音效未初始化");
        checkNull.Check(floor_audio_clips, "floor_audio_clips/石板走路音效未初始化");
        checkNull.Check(wood_audio_clips, "wood_audio_clips/木板走路音效未初始化");
        checkNull.Check(BackgroundMusic, "BackgroundMusic/背景音乐播放器未初始化");
        return checkNull.State;
    }
#endif
}