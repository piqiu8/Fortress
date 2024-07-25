//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// 女巫召唤僵尸控制脚本
/// </summary>
public class Witch_Zombie_Control : MonoBehaviour
{
    /// <summary>
    /// 女巫召唤僵尸音频播放器
    /// </summary>
    private AudioSource Witch_Zombie_Voice;

    /// <summary>
    /// 女巫召唤僵尸叫声音效
    /// </summary>
    public AudioClip[] Witch_Zombie_say;

    /// <summary>
    /// 音效CD
    /// </summary>
    private float Voice_CD;

    /// <summary>
    /// 对应速度，默认为3
    /// </summary>
    public float Witch_Zombie_speed = 3f;

    /// <summary>
    /// 对应感知范围，默认为10
    /// </summary>
    public float Witch_zombie_cooleye = 10f;

    /// <summary>
    /// 对应声音大小
    /// </summary>
    [Range(0, 1f)] public float Witch_Zombie_volum = 0.3f;

    /// <summary>
    /// 对应存活状态
    /// </summary>
    public bool witch_zombie_dead = false;

    /// <summary>
    /// 对应死亡音效
    /// </summary>
    public AudioClip[] witch_zombie_dead_say;

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
            Witch_Zombie_Voice.PlayOneShot(Witch_Zombie_say[Random.Range(0, Witch_Zombie_say.Length)], Witch_Zombie_volum);
        }
    }

    /// <summary>
    /// 追击玩家
    /// </summary>
    private void FindPlayer()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < Witch_zombie_cooleye)
        {
            Voice();
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * Witch_Zombie_speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 杀死玩家
    /// </summary>
    private void GameOver()
    {
        Time.timeScale = 0;
        player.GetComponent<RestartGame>().Enemy_name = "Witch";
    }

    /// <summary>
    /// 死亡发出声音
    /// </summary>
    private void Enemy_die_voice()
    {
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(witch_zombie_dead_say[Random.Range(0, witch_zombie_dead_say.Length)], maincamera.transform.position, 0.5f);
    }

    /// <summary>
    /// 死亡对应操作
    /// </summary>
    private void dead()
    {
        Enemy_die_voice();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        tag = "corpse";
        player.GetComponent<PlayerControl>().enemy_num--;
        GetComponent<Witch_Zombie_Control>().enabled = false;
    }

    private void Start()
    {
        Witch_Zombie_Voice = GetComponent<AudioSource>();
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
#if DEBUG_MODE
        if (!Witch_Zombie_Voice)
        {
            Debug.LogError("未获取到AudioSource组件");
            return;
        }
        if (!player)
        {
            Debug.LogError("未获取到玩家物体");
            return;
        }
        if (!WitchZombieDeBug())
        {
            Debug.LogError("初始化失败");
            return;
        }
#endif
    }

    private void Update()
    {
        Voice_CD += Time.deltaTime;
        if (witch_zombie_dead) dead();
    }

    private void FixedUpdate()
    {
        FindPlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!witch_zombie_dead)
        {
            if (collision.gameObject.name == "Knight") GameOver();
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// WitchZombie初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool WitchZombieDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(Witch_Zombie_Voice, "Witch_Zombie_Voice/音频播放器未赋值");
        checkNull.Check(Witch_Zombie_say, "Witch_Zombie_say/女巫召唤僵尸声音未赋值");
        checkNull.Check(witch_zombie_dead_say, "witch_zombie_dead_say/女巫召唤僵尸死亡音效未赋值");
        checkNull.Check(player, "player/玩家物体未赋值");
        return checkNull.State;
    }
#endif
}