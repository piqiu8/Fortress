//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// 僵尸控制脚本
/// </summary>
public class ZombieControl : MonoBehaviour
{
    /// <summary>
    /// 僵尸音频播放器
    /// </summary>
    private AudioSource ZombieVoice;

    /// <summary>
    /// 僵尸叫声音效
    /// </summary>
    public AudioClip[] Zombie_say;

    /// <summary>
    /// 音效CD
    /// </summary>
    private float Voice_CD;

    /// <summary>
    /// 僵尸速度，默认为3
    /// </summary>
    public float zombie_speed = 3f;

    /// <summary>
    /// 僵尸感知范围，默认为10
    /// </summary>
    public float zombie_cooleye = 10f;

    /// <summary>
    /// 僵尸声音大小，默认为0.3
    /// </summary>
    [Range(0, 1f)] public float zombie_volum = 0.3f;

    /// <summary>
    /// 僵尸存活状态
    /// </summary>
    public bool zombie_dead = false;

    /// <summary>
    /// 僵尸的死亡音效
    /// </summary>
    public AudioClip[] Zombie_dead_say;

    /// <summary>
    /// 玩家物体
    /// </summary>
    private GameObject player;

    /// <summary>
    /// 距离玩家距离
    /// </summary>
    private float distance = 0;

    /// <summary>
    /// 发出声音
    /// </summary>
    private void Voice()
    {
        if (Voice_CD > Random.Range(7, 12))
        {
            Voice_CD = 0;
            ZombieVoice.PlayOneShot(Zombie_say[Random.Range(0, Zombie_say.Length)], zombie_volum);
        }
    }

    /// <summary>
    /// 寻找玩家
    /// </summary>
    private void FindPlayer()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < zombie_cooleye)
        {
            Voice();
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * zombie_speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 杀死玩家
    /// </summary>
    private void GameOver()
    {
        Time.timeScale = 0;
        player.GetComponent<RestartGame>().Enemy_name = "Zombie";
    }

    /// <summary>
    /// 死亡发出声音
    /// </summary>
    private void Enemy_die_voice()
    {
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(Zombie_dead_say[Random.Range(0, Zombie_dead_say.Length)], maincamera.transform.position, 0.5f);
    }

    /// <summary>
    /// 死亡执行对应操作
    /// </summary>
    private void dead()
    {
        Enemy_die_voice();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        tag = "corpse";
        player.GetComponent<PlayerControl>().enemy_num--;
        GetComponent<ZombieControl>().enabled = false;
    }

    private void Start()
    {
        ZombieVoice = GetComponent<AudioSource>();
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
#if DEBUG_MODE
        if (!ZombieVoice)
        {
            Debug.LogError("未获取到AudioSource组件");
            return;
        }
        if (!player)
        {
            Debug.LogError("未获取到玩家物体");
            return;
        }
        if (!ZombieControlDeBug())
        {
            Debug.LogError("初始化失败");
            return;
        }
#endif
    }

    private void Update()
    {
        Voice_CD += Time.deltaTime;
        if (zombie_dead) dead();
    }

    private void FixedUpdate()
    {
        FindPlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //若自身没死
        if (!zombie_dead)
        {
            if (collision.gameObject.name == "Knight") GameOver();
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// ZombieControl初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool ZombieControlDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(ZombieVoice, "ZombieVoice/僵尸音频播放器未赋值");
        checkNull.Check(Zombie_say, "Zombie_say/僵尸叫声音效未赋值");
        checkNull.Check(Zombie_dead_say, "Zombie_dead_say/僵尸死亡音效未赋值");
        checkNull.Check(player, "player/玩家物体未赋值");
        return checkNull.State;
    }
#endif
}