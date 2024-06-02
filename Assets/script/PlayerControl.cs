using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bomb;
    //炸弹物体
    public GameObject bumb;
    //炸弹生成位置
    //private Rigidbody mmRigidbody;
    private float CD = 2;
    //炸弹释放CD时间
    private float CD2 = 1;
    //计时器，用于延迟处理
    private float time = 0f;
    //脚步音效CD时间
    private Vector3 dir;
    //玩家方向向量，用于控制玩家移动方向
    private bool isbumb;
    //用来控制炸弹释放
    public GameObject win;
    //游戏胜利UI界面
    private AudioSource player;
    [Header("走路音效设定")]
    //玩家角色的音频组件
    public AudioClip[] terrain_audio_clips;
    //草地走路音效
    public AudioClip[] floor_audio_clips;
    //石地板走路音效
    public AudioClip[] wood_audio_clips;
    //木板走路音效
    private AudioSource BackgroundMusic;
    //创建背景音乐播放器
    [Header("玩家速度设定")]
    public float player_speed = 6f;

    [HideInInspector]public int enemy_num;

    private void GetAxis(){//获取虚拟轴以赋予玩家方向
        float horizontal = Input.GetAxis("Horizontal");
        //获取水平虚拟轴
        float vertical = Input.GetAxis("Vertical");
        //获取垂直虚拟轴
        dir = new Vector3(horizontal, 0, vertical);
        //根据垂直与虚拟轴确定玩家方向
    }
    private void GetKey(){//检测F按键，用于释放炸弹
        if (Input.GetKeyDown(KeyCode.F)) isbumb = true;
        //通过GetKeyDowm方式检测按键F
        //GetKey()：当通过你所指定的按键被用户按住时返回true，记住！是按住，就是长按的意识，比如你想控制角色在你按住方向键时移动，那么就是用GetKey()
        //GetKeyDown()：当通过你所按下指定名称的按键时的那一帧时返回true，记住！是那一帧，就一下的事情，不管你按多久，只是在你按下的那一瞬间
        //GetKeyUp()：在通过你释放（按键弹起时）给定名字的按键的那一帧返回true，记住！是那一帧，就一下的事情。
    }

    private void PutBomb(){//控制炸弹释放
        if (isbumb){
            isbumb = false;
            //已经按下F键释放炸弹，因此将状态更改
            if (CD > 2){
                CD = 0;
                //CD计算时间大于2时炸弹冷却结束，可以再次释放炸弹，并把CD时间清空开始从先计算CD
                Instantiate(bomb, bumb.transform.position, bumb.transform.rotation);
                //在bumb的位置生成炸弹
            }
        }
    }

    private void CDtime(){//计算CD时间
        CD += Time.deltaTime;
        //计算炸弹CD时间
        CD2 += Time.deltaTime;
        //计算走路音效CD时间
        //计算时间使用的是增量时间，因不同设备的帧率是不一样的，而unity里的方法是每帧调用一次的，若按普通的秒来算，想导致计算不准确且不同设备的计算结果也不太同
        //若帧率为1秒30帧，则增量时间为1/帧数 s，这样就能保证不同设备计算的时间都是一样的，因为虽然帧率不同，但都是1秒的帧率
    }

    private void Move(){//控制玩家移动
        if (dir != Vector3.zero){
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            //也可以这样旋转，但不自然，transform.rotation = Quaternion.LookRotation(dir);
            //创建一个四元数来保存看向dir方向需要的旋转量
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            //碰撞更自然的运动方式，但需要刚体组件，rigidbody.MovePosition(rigidbody.position + speed * Time.deltaTime);
            //mmRigidbody.MovePosition(transform.position + dir * 0.15f);
            //因为是旋转移动，所以通过球形插值进行旋转，使旋转更加平滑，角速度为10度/s
            transform.Translate(Vector3.forward * player_speed * Time.deltaTime);
            //向前方向进行移动，速度为6m/s，或者说6个单位/s
        }
    }

    private void GameWin(){//控制游戏是否胜利
        //GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        //获取所有敌人
        if (enemy_num == 0){
            //开始计时
            time += Time.fixedUnscaledDeltaTime;
            //若敌人数量为0则代表游戏胜利
            Time.timeScale = 0;
            //停止背景音乐
            BackgroundMusic.Stop();
            //暂停游戏时间
            GetComponent<Esc>().enabled = false;
            //使Esc组件失效
            win.SetActive(true);
            //激活胜利UI
            if (time > 10){//等待10s才可按键返回主界面
                if (Input.anyKey){
                    time = 0;
                    SceneManager.LoadScene(0);
                    //按下任意键使界面跳转到开始界面
                    Time.timeScale = 1;
                    //使游戏时间恢复正常
                }
            }
        }
    }
    void Start(){//游戏开始就会调用一次
        player = GetComponent<AudioSource>();
        //mmRigidbody = gameObject.GetComponent<Rigidbody>();
        //获取玩家的音频组件，使得玩家角色可以听见声音以及触发音效
        BackgroundMusic = GameObject.Find("MusicTrigger").GetComponent<AudioSource>();
        //获取背景音乐播放器
        enemy_num= GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    // Update is called once per frame
    void Update(){//游戏开始后，每帧都会调用一次，以便实时监测游戏状态
        GetAxis();
        GetKey();
        CDtime();
        GameWin();
    }
    private void FixedUpdate(){//固定时间调用一次，不随帧率改变，可以使物理模拟更加稳定，因此用来调用存在物理碰撞的方法
        PutBomb();
        Move();
        //GameWin();
    }

    private void play_clip(string s){//随机播放脚步音效
        if (s == "Terrain") player.PlayOneShot(terrain_audio_clips[Random.Range(0, terrain_audio_clips.Length)]);
        //若为terrain地块，则播放对应的随机走路音效
        else{
            if (s == "floor") player.PlayOneShot(floor_audio_clips[Random.Range(0, floor_audio_clips.Length)]);
            else player.PlayOneShot(wood_audio_clips[Random.Range(0, wood_audio_clips.Length)]);
        }
    }
    private void OnCollisionStay(Collision collision){//自带的碰撞检测方法，collision表示碰到的物体
        if (CD2 > 0.5){
            CD2 = 0;
            //脚步音效CD冷却完毕则播放脚步音效，播放完后清0再次进行冷却
            if (collision.gameObject.name == "Terrain") play_clip("Terrain");
            //玩家碰到的物体的名称是否为Terrain，是则播放对应音效
            else{
                if (collision.gameObject.transform.parent != null){
                    if (collision.gameObject.transform.parent.gameObject.tag == "floor") play_clip("floor");
                    else if (collision.gameObject.transform.tag == "wood") play_clip("wood");
                }
            }
        }
    }
}
