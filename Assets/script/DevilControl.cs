//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// 实现 恶魔 的基本行为逻辑，包括跟踪玩家，触碰玩家时杀死玩家，播放叫声，死亡，杀死玩家音效 |
/// 实现方法：跟踪玩家通过当玩家与 恶魔 的距离小于一定值时，使 恶魔 看向玩家，并向正前方前进实现；
/// 触碰时杀死玩家通过碰撞检测方法，并检测碰撞对象名称是否为“knight”，若是则结束游戏实现；
/// 播放音效通过基本音效组件
/// </summary>
public class DevilControl : MonoBehaviour
{
    /// <summary>
    /// 恶魔音效播放器
    /// </summary>
    private AudioSource Devil_Voice;

    /// <summary>
    /// 恶魔音效数组
    /// </summary>
    public AudioClip[] Devil_Say;

    /// <summary>
    /// 声音音效播放的时间间隔
    /// </summary>
    private float Voice_CD;

    /// <summary>
    /// 恶魔的速度，默认为5
    /// </summary>
    public float devil_speed = 5f;

    /// <summary>
    /// 恶魔的感知范围，默认为15
    /// </summary>
    public float devil_cooleye = 15f;

    /// <summary>
    /// 恶魔的音量，默认为1倍
    /// </summary>
    [Range(0, 1f)] public float devil_volum = 1f;

    /// <summary>
    /// 恶魔的存活状态
    /// </summary>
    public bool devil_dead = false;

    /// <summary>
    /// 恶魔的死亡音效数组
    /// </summary>
    public AudioClip[] devil_dead_say;

    /// <summary>
    /// 玩家物体
    /// </summary>
    private GameObject player;

    /// <summary>
    /// 距玩家的距离
    /// </summary>
    private float distance = 0;

    /// <summary>
    /// 播放恶魔的语音音效
    /// </summary>
    private void Voice()
    {
        //获得一个随机数使叫声随机播放
        if (Voice_CD > Random.Range(7, 12))
        {
            //播放语音音效后重置CD
            Voice_CD = 0;
            //随机播放敌人语音音效集合中的其中一个音效
            Devil_Voice.PlayOneShot(Devil_Say[Random.Range(0, Devil_Say.Length)], devil_volum);
        }
    }

    /// <summary>
    /// 追踪玩家
    /// </summary>
    private void FindPlayer()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        //当玩家进入devil的感知范围内
        if (distance < devil_cooleye)
        {
            Voice();
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * devil_speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 获取杀死玩家的敌人信息
    /// </summary>
    private void GameOver()
    {
        //暂停游戏
        Time.timeScale = 0;
        //将杀死玩家的敌人名称进行记录
        player.GetComponent<RestartGame>().Enemy_name = "Devil";
    }

    /// <summary>
    /// 播放敌人死亡音效
    /// </summary>
    private void Enemy_die_voice()
    {
        GameObject maincamera = GameObject.Find("Main Camera");
#if DEBUG_MODE
        if (!maincamera)
        {
            Debug.LogError("未获取到摄像机");
            return;
        }
#endif
        //敌人死亡时，在摄像机位置随机生成一个死亡音效以确保音量正常，音量为1倍
        AudioSource.PlayClipAtPoint(devil_dead_say[Random.Range(0, devil_dead_say.Length)], maincamera.transform.position, 0.5f);
        //因为PlayClipAtPoint生成的音效是3D音效，会随距离衰减，不太好控制，而摄像机上挂载了音效接收器，所以干脆生成在摄像机位置就不用担心距离衰减了
    }

    /// <summary>
    /// 控制敌人死亡行为
    /// </summary>
    private void dead()
    {
        Enemy_die_voice();
        //解锁刚体的x、y、z，使尸体拥有正常物理效果
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //将tag改为corpse
        tag = "corpse";
        //使敌人总数减一
        player.GetComponent<PlayerControl>().enemy_num--;
        //关闭行动脚本
        GetComponent<DevilControl>().enabled = false;
    }

    private void Start()
    {
        Devil_Voice = GetComponent<AudioSource>();
        Voice_CD = Random.Range(5, 15);
        //初始化一个范围内的随机值给Voice_CD，防止第一次遇见敌人时敌人全部一起发出音效
        player = GameObject.FindWithTag("Player");
#if DEBUG_MODE
        if (!Devil_Voice)
        {
            Debug.LogError("未获取到AudioSource组件");
            return;
        }
        if (!player)
        {
            Debug.LogError("未获取到玩家物体");
            return;
        }
        if (!DevilControlDeBug())
        {
            Debug.LogError("初始化失败");
            return;
        }
#endif
    }

    private void Update()
    {
        //对音效时间间隔进行计时
        Voice_CD += Time.deltaTime;
        //执行死亡操作
        if (devil_dead) dead();
    }

    private void FixedUpdate()
    {
        //追击玩家
        FindPlayer();
    }

    /// <summary>
    /// 检测是否碰到玩家
    /// </summary>
    /// <param name="collision">碰撞物信息</param>
    private void OnCollisionEnter(Collision collision)
    {
        //若没死
        if (!devil_dead)
        {
            if (collision.gameObject.name == "Knight") GameOver();
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// DevilControl初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool DevilControlDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(Devil_Voice, "Devil_Voice/恶魔音效播放器未赋值");
        checkNull.Check(Devil_Say, "Devil_Say/恶魔音效数组未赋值");
        checkNull.Check(devil_dead_say, "devil_dead_say/恶魔死亡音效数组未赋值");
        checkNull.Check(player, "player/玩家物体未赋值");
        return checkNull.State;
    }
#endif
}