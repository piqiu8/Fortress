//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// 实现玩家投掷炸弹用于攻击敌人，杀死敌人则播放敌人死亡音效 |
/// 实现思路：当执行该脚本时自动生成一个炸弹，并在2s后引爆，通过检测炸弹与敌人的距离是否小于3m来判断是否杀死敌人，杀死敌人与炸弹爆炸都在原位置
/// 生成一个敌人死亡音效和炸弹爆炸音效，用生成音效的原因是若直接用播放的方式播放音效，在敌人与炸弹物体被销毁时会导致音效没播放完就因
/// 为物体被销毁被停掉了
/// </summary>
public class BombControl : MonoBehaviour
{
    /// <summary>
    /// 炸弹爆炸特效
    /// </summary>
    public GameObject EffectPre = null;

    /// <summary>
    /// 对应炸弹音效播放器
    /// </summary>
    private AudioSource ClipPlayer = null;

    /// <summary>
    /// 炸弹投掷音效
    /// </summary>
    public AudioClip Throw = null;

    /// <summary>
    /// 炸弹爆炸音效
    /// </summary>
    public AudioClip Booming = null;

    /// <summary>
    /// 实现炸弹爆炸杀死敌人
    /// </summary>
    private void Boom()
    {
        //生成一个炸弹爆炸特效在炸弹位置
        GameObject effect = Instantiate(EffectPre, transform.position, transform.rotation);
        //2s后销毁爆炸特效
        Destroy(effect, 2f);
        //获取所有敌人数组
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        //对所有敌人进行遍历
        foreach (GameObject enemy in enemys)
        {
            //若敌人距离玩家的距离小于3m
            if (Vector3.Distance(transform.position, enemy.transform.position) < 3f)
            {
                //若杀死的敌人有父物体，获取其父物体名称
                //因同种敌人的名称不可相同，但又是同种敌人，因此把同种敌人放到一个父物体以便统一识别
                if (enemy.transform.parent != null)
                {
                    //对不同名称采取不同行为
                    string name = enemy.transform.parent.gameObject.name;
                    switch (name)
                    {
                        //若名称为zombie，则将其状态改为死亡
                        case "Zombie":
                            enemy.GetComponent<ZombieControl>().zombie_dead = true; break;
                        case "Witch":
                            enemy.GetComponent<WitchControl>().witch_dead = true; break;
                        case "Ghost":
                            enemy.GetComponent<GhostControl>().ghost_dead = true; break;
                        case "Pumpkin":
                            enemy.GetComponent<PumpkinControl>().pumpkin_dead = true; break;
                        case "Devil":
                            enemy.GetComponent<DevilControl>().devil_dead = true; break;
                        default: break;
                    }
                }
                //无父物体，说明是Witch生成的Zombie
                else enemy.GetComponent<Witch_Zombie_Control>().witch_zombie_dead = true;
                //对敌人施加一个瞬间的力，来模拟炸飞效果，方向由炸弹指向敌人
                if (enemy.GetComponent<Rigidbody>() != null) enemy.GetComponent<Rigidbody>().AddForce((enemy.transform.position - transform.position) * 5f, ForceMode.Impulse);
            }
        }
        GameObject maincamera = GameObject.Find("Main Camera");
#if DEBUG_MODE
        if (!maincamera)
        {
            Debug.LogError("未获取到Main Camera");
            return;
        }
#endif
        //生成一个爆炸音效在摄像机位置，音量为0.5倍
        AudioSource.PlayClipAtPoint(Booming, maincamera.transform.position, 0.5f);
        //销毁炸弹物体
        Destroy(gameObject);
#if DEBUG_MODE
        Debug.Log("炸弹爆炸");
#endif
    }

    private void Start()
    {
        ClipPlayer = GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!ClipPlayer)
        {
            Debug.LogError("未获取到AudioSource组件");
            return;
        }
        //初始化检查
        if (!BombDeBug())
        {
            Debug.LogError("初始化失败");
            return;
        }
#endif
        //播放放炸弹音效，音量为0.1倍
        ClipPlayer.PlayOneShot(Throw, 0.3f);
        //2s后执行Boom方法
        Invoke("Boom", 2f);
    }

#if DEBUG_MODE
    /// <summary>
    /// BombControl初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool BombDeBug()
    {
        CheckNull checkNull = gameObject.AddComponent<CheckNull>();
        checkNull.Check(EffectPre, "EffectPre/爆炸效果未赋值");
        checkNull.Check(ClipPlayer, "ClipPlayer/音效播放器未赋值");
        checkNull.Check(Throw, "Throw/炸弹投掷音效未赋值");
        checkNull.Check(Booming, "Booming/炸弹爆炸音效未赋值");
        return checkNull.State;
    }
#endif
}