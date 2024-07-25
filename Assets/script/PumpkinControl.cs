//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// 南瓜控制脚本
/// </summary>
public class PumpkinControl : MonoBehaviour
{
    /// <summary>
    /// 南瓜音频播放器组件
    /// </summary>
    private AudioSource Pumpkin_Voice;

    /// <summary>
    /// 南瓜声音音效
    /// </summary>
    public AudioClip[] Pump_Say;

    /// <summary>
    /// 声音音效播放的时间间隔
    /// </summary>
    private float Voice_CD;

    /// <summary>
    /// 南瓜的速度，默认为10
    /// </summary>
    public float pumpkin_speed = 10f;

    /// <summary>
    /// 南瓜的感知范围，默认为2
    /// </summary>
    public float pump_cooleye = 2f;

    /// <summary>
    /// 南瓜的声音大小，默认为0.3
    /// </summary>
    [Range(0, 1f)] public float pump_volum = 0.3f;

    /// <summary>
    /// 南瓜的存活状态
    /// </summary>
    public bool pumpkin_dead = false;

    /// <summary>
    /// 南瓜的死亡音效
    /// </summary>
    public AudioClip[] pumpkin_dead_say;

    /// <summary>
    /// 玩家物体
    /// </summary>
    private GameObject player;

    /// <summary>
    /// 距玩家的距离
    /// </summary>
    private float distance = 0;

    /// <summary>
    /// 南瓜发出声音
    /// </summary>
    private void Voice()
    {
        if (Voice_CD > Random.Range(7, 12))
        {
            Voice_CD = 0;
            Pumpkin_Voice.PlayOneShot(Pump_Say[Random.Range(0, Pump_Say.Length)], pump_volum);
        }
    }

    /// <summary>
    /// 南瓜追击玩家
    /// </summary>
    private void FindPlayer()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        //是否与玩家的距离小于10m
        if (distance < 10)
        {
            //是，产生音效
            Voice();
            //是否与玩家距离小于2m
            if (distance < pump_cooleye)
            {
                //是，看向玩家
                transform.LookAt(player.transform);
                //以10m/s的速度接近玩家
                transform.Translate(Vector3.forward * pumpkin_speed * Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// 南瓜击杀玩家
    /// </summary>
    private void GameOver()
    {
        Time.timeScale = 0;
        player.GetComponent<RestartGame>().Enemy_name = "Pumpkin";
    }

    /// <summary>
    /// 南瓜死亡时发出声音
    /// </summary>
    private void Enemy_die_voice()
    {
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(pumpkin_dead_say[Random.Range(0, pumpkin_dead_say.Length)], maincamera.transform.position, 0.5f);
    }

    /// <summary>
    /// 南瓜死亡对应操作
    /// </summary>
    private void dead()
    {
        Enemy_die_voice();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        tag = "corpse";
        player.GetComponent<PlayerControl>().enemy_num--;
        GetComponent<PumpkinControl>().enabled = false;
    }

    private void Start()
    {
        //获取AudioSource组件
        Pumpkin_Voice = GetComponent<AudioSource>();
        //初始化一个范围内的随机值给Voice_CD，防止第一次遇见敌人时敌人全部一起发出音效
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
#if DEBUG_MODE
        if (!Pumpkin_Voice)
        {
            Debug.LogError("未获取到AudioSourec组件");
            return;
        }
        if (!player)
        {
            Debug.LogError("未获取到玩家物体");
            return;
        }
        if (!PumpkinControlDeBug())
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
        if (pumpkin_dead) dead();
    }

    private void FixedUpdate()
    {
        FindPlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!pumpkin_dead)
        {
            if (collision.gameObject.name == "Knight") GameOver();
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// PumpkinControl初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool PumpkinControlDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(Pumpkin_Voice, "南瓜音频播放器未赋值");
        checkNull.Check(Pump_Say, "南瓜声音音效未赋值");
        checkNull.Check(pumpkin_dead_say, "南瓜死亡音效未赋值");
        checkNull.Check(player, "玩家物体未赋值");
        return checkNull.State;
    }
#endif
}