using UnityEngine;

/*
 *功能：实现 恶魔 的基本行为逻辑，包括跟踪玩家，触碰玩家时杀死玩家，播放叫声，死亡，杀死玩家音效
 *实现方法：跟踪玩家通过当玩家与 恶魔 的距离小于一定值时，使 恶魔 看向玩家，并向正前方前进实现；
 *         触碰时杀死玩家通过碰撞检测方法，并检测碰撞对象名称是否为“knight”，若是则结束游戏实现；
 *         播放音效通过基本音效组件
 */

public class DevilControl : MonoBehaviour
{
    private AudioSource Devil_Voice;
    //devil的声音音效集合
    public AudioClip[] Devil_Say;
    //声音音效播放的时间间隔
    private float Voice_CD;
    //devil的速度
    public float devil_speed = 5f;
    //devil的感知范围
    public float devil_cooleye = 15f;
    //devil的音量
    [Range(0,1f)]public float devil_volum = 1f;
    //devil的状态
    public bool devil_dead = false;
    //devil的死亡音效
    public AudioClip[] devil_dead_say;
    private GameObject player;
    private float distance=0;
    private void Voice(){//播放devil的语音音效
        if (Voice_CD > Random.Range(7, 12)){//获得一个随机数使叫声随机播放
            Voice_CD = 0;
            //播放语音音效后重置CD
            Devil_Voice.PlayOneShot(Devil_Say[Random.Range(0, Devil_Say.Length)], devil_volum);
            //随机播放敌人语音音效集合中的其中一个音效
        }
    }
    private void FindPlayer(){//追踪玩家
        distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance < devil_cooleye){//当玩家进入devil的感知范围内
            Voice();
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * devil_speed * Time.deltaTime);
        }
    }
    private void GameOver(){//获取杀死玩家的敌人信息
        Time.timeScale = 0;
        //暂停游戏
        player.GetComponent<RestartGame>().Enemy_name = "Devil";
        //将杀死玩家的敌人名称进行记录
    }
    private void Enemy_die_voice(){//敌人死亡时，在摄像机位置随机生成一个死亡音效以确保音量正常，音量为1倍
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(devil_dead_say[Random.Range(0, devil_dead_say.Length)], maincamera.transform.position, 0.5f);
        //因为PlayClipAtPoint生成的音效是3D音效，会随距离衰减，不太好控制，而摄像机上挂载了音效接收器，所以干脆生成在摄像机位置就不用担心距离衰减了
    }
    private void dead(){//控制敌人的死亡行为
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
    private void Start(){
        Devil_Voice = GetComponent<AudioSource>();
        Voice_CD = Random.Range(5, 15);
        //初始化一个范围内的随机值给Voice_CD，防止第一次遇见敌人时敌人全部一起发出音效
        player = GameObject.FindWithTag("Player");
    }

    private void Update(){
        //对音效时间间隔进行计时
        Voice_CD+= Time.deltaTime;
        //执行死亡操作
        if(devil_dead) dead();
    }
    // Update is called once per frame
    private void FixedUpdate(){
        //追击玩家
        FindPlayer();
    }

    private void OnCollisionEnter(Collision collision){//检测是否碰到玩家
        if (!devil_dead){//若没死
            if (collision.gameObject.name == "Knight") GameOver();
        }
    }
}
