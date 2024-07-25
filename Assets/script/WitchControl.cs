//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// 女巫控制脚本
/// </summary>
public class WitchControl : MonoBehaviour
{
    /// <summary>
    /// 女巫召唤僵尸物体
    /// </summary>
    public GameObject Witch_Zombie;

    /// <summary>
    /// 召唤僵尸CD
    /// </summary>
    private float CD = 6;

    /// <summary>
    /// 女巫对应音频播放器
    /// </summary>
    private AudioSource Witch_Voice;

    /// <summary>
    /// 女巫声音音效
    /// </summary>
    public AudioClip[] Witch_Say;

    /// <summary>
    /// 声音音效间隔
    /// </summary>
    private float Voice_CD;

    /// <summary>
    /// 对应速度，默认为1
    /// </summary>
    public float witch_speed = 1f;

    /// <summary>
    /// 对应感知范围
    /// </summary>
    public float witch_cooleye = 10f;

    /// <summary>
    /// 对应声音大小
    /// </summary>
    [Range(0, 1f)] public float witch_volum = 0.4f;

    /// <summary>
    /// 对应存活状态
    /// </summary>
    public bool witch_dead = false;

    /// <summary>
    /// 对应死亡音效
    /// </summary>
    public AudioClip[] witch_dead_say;

    /// <summary>
    /// 玩家物体
    /// </summary>
    private GameObject player;

    /// <summary>
    /// 距玩家距离
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
            Witch_Voice.PlayOneShot(Witch_Say[Random.Range(0, Witch_Say.Length)], witch_volum);
        }
    }

    /// <summary>
    /// 远离玩家
    /// </summary>
    private void AwayPlayer()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < witch_cooleye)
        {
            //如果玩家在Witch的10m范围内，内产生音效
            Voice();
            transform.LookAt(player.transform);
            //远离玩家
            transform.Translate(Vector3.back * witch_speed * Time.deltaTime);
            if (CD > 6)
            {//若CD大于6
                CD = 0;
                //在自身位置生成一个witch_zombie
                Instantiate(Witch_Zombie, transform.position, transform.rotation);
                player.GetComponent<PlayerControl>().enemy_num++;
            }
        }
    }

    /// <summary>
    /// 死亡发出声音
    /// </summary>
    private void Enemy_die_voice()
    {
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(witch_dead_say[Random.Range(0, witch_dead_say.Length)], maincamera.transform.position, 0.5f);
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
        GetComponent<WitchControl>().enabled = false;
    }

    private void Start()
    {
        //获取AudioSource组件
        Witch_Voice = GetComponent<AudioSource>();
        //初始化一个范围内的随机值给Voice_CD，防止第一次遇见敌人时敌人全部一起发出音效
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
#if DEBUG_MODE
        if (!Witch_Voice)
        {
            Debug.LogError("未获取到AudioSource组件");
            return;
        }
        if (!player)
        {
            Debug.LogError("未获取到玩家物体");
            return;
        }
        if (!WitchControlDeBug())
        {
            Debug.LogError("初始化失败");
            return;
        }
#endif
    }

    private void Update()
    {
        CD += Time.deltaTime;
        //对声音音效时间间隔进行计时
        Voice_CD += Time.deltaTime;
        if (witch_dead) dead();
    }

    private void FixedUpdate()
    {
        AwayPlayer();
    }

#if DEBUG_MODE
    /// <summary>
    /// WitchControl初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool WitchControlDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(Witch_Zombie, "Witch_Zombie/女巫召唤僵尸物体未赋值");
        checkNull.Check(Witch_Voice, "Witch_Voice/音频播放器未赋值");
        checkNull.Check(Witch_Say, "Witch_say/女巫声音未赋值");
        checkNull.Check(witch_dead_say, "witch_dead_say/女巫死亡音效未赋值");
        checkNull.Check(player, "player/玩家物体未赋值");
        return checkNull.State;
    }
#endif
}