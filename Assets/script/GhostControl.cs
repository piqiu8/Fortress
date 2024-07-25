//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// 幽灵控制脚本
/// </summary>
public class GhostControl : MonoBehaviour
{
    /// <summary>
    /// 幽灵的AudioSource组件
    /// </summary>
    private AudioSource Ghost_Voice;

    /// <summary>
    /// 幽灵的声音音效
    /// </summary>
    public AudioClip[] Ghost_Say;

    /// <summary>
    /// 声音音效播放的时间间隔
    /// </summary>
    private float Voice_CD;

    /// <summary>
    /// 幽灵的速度，默认为2
    /// </summary>
    public float ghost_speed = 2f;

    /// <summary>
    /// 幽灵的感知范围，默认为20
    /// </summary>
    public float ghost_cooleye = 20f;

    /// <summary>
    /// 幽灵声音的音量，默认为1倍
    /// </summary>
    [Range(0, 1f)] public float ghost_volum = 1f;

    /// <summary>
    /// 幽灵的存活状态
    /// </summary>
    public bool ghost_dead = false;

    /// <summary>
    /// 幽灵的死亡音效
    /// </summary>
    public AudioClip[] ghost_dead_say;

    /// <summary>
    /// 距玩家的距离
    /// </summary>
    private float distance = 0;

    /// <summary>
    /// 玩家物体
    /// </summary>
    private GameObject player;

    /// <summary>
    /// 使幽灵发出声音
    /// </summary>
    private void Voice()
    {
        if (Voice_CD > Random.Range(7, 12))
        {
            Voice_CD = 0;
            Ghost_Voice.PlayOneShot(Ghost_Say[Random.Range(0, Ghost_Say.Length)], ghost_volum);
        }
    }

    /// <summary>
    /// 使幽灵追击玩家
    /// </summary>
    private void FindPlayer()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < ghost_cooleye)
        {
            Voice();
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * ghost_speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 杀死玩家
    /// </summary>
    private void GameOver()
    {
        if (distance < 1)
        {
            //因ghost没有碰撞检测，所以需要手动更改玩家死亡状态
            player.GetComponent<IfDie>().IsDie = true;
            //表明杀死玩家的敌人名字
            Time.timeScale = 0;
            player.GetComponent<RestartGame>().Enemy_name = "Ghost";
        }
    }

    /// <summary>
    /// 死亡发出叫声
    /// </summary>
    private void Enemy_die_voice()
    {
        GameObject maincamera = GameObject.Find("Main Camera");
#if DEBUG_MODE
        if (!maincamera)
        {
            Debug.LogError("未获取到主摄像机");
            return;
        }
#endif
        AudioSource.PlayClipAtPoint(ghost_dead_say[Random.Range(0, ghost_dead_say.Length)], maincamera.transform.position, 0.5f);
    }

    /// <summary>
    /// 幽灵死亡对应操作
    /// </summary>
    private void Dead()
    {
        Enemy_die_voice();
        player.GetComponent<PlayerControl>().enemy_num--;
        Destroy(gameObject);
    }

    private void Start()
    {
        //获取AudioSource组件
        Ghost_Voice = GetComponent<AudioSource>();
        //初始化一个范围内的随机值给Voice_CD，防止第一次遇见敌人时敌人全部一起发出音效
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
#if DEBUG_MODE
        if (!Ghost_Voice)
        {
            Debug.LogError("未获取到AudioSource组件");
            return;
        }
        if (!player)
        {
            Debug.LogError("未获取到玩家物体");
            return;
        }
        if (!GhostControlDeBug())
        {
            Debug.LogError("初始化失败");
            return;
        }
#endif
    }

    private void Update()
    {
        if (ghost_dead) Dead();
        Voice_CD += Time.deltaTime;
        FindPlayer();
        GameOver();
    }

#if DEBUG_MODE
    /// <summary>
    /// GhostControl初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool GhostControlDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(Ghost_Voice, "Ghost_Voice/幽灵音频播放器未初始化");
        checkNull.Check(Ghost_Say, "Ghost_Say/幽灵声音音效未初始化");
        checkNull.Check(ghost_dead_say, "ghost_dead_say/幽灵死亡音效未初始化");
        checkNull.Check(player, "player/玩家物体未初始化");
        return checkNull.State;
    }
#endif
}