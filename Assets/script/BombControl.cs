using UnityEngine;
/*
 *功能：实现玩家投掷炸弹用于攻击敌人，杀死敌人则播放敌人死亡音效
 *实现方式：当执行该脚本时自动生成一个炸弹，并在2s后引爆，通过检测炸弹与敌人的距离是否小于3m来判断是否杀死敌人，杀死敌人与炸弹爆炸都在原位置
 *         生成一个敌人死亡音效和炸弹爆炸音效，用生成音效的原因是若直接用播放的方式播放音效，在敌人与炸弹物体被销毁时会导致音效没播放完就因
 *         为物体被销毁被停掉了
 */

public class BombControl : MonoBehaviour
{
    public GameObject EffectPre;
    //创建一个爆炸特效
    private AudioSource player;
    //创建一个音频播放器
    public AudioClip Throw;
    //创建一个放炸弹音效
    public AudioClip Booming;
    //创建一个炸弹爆炸音效

    void Boom(){//炸弹爆炸方法
        GameObject effect=Instantiate(EffectPre,transform.position,transform.rotation);
        //生成一个炸弹爆炸特效在炸弹位置
        Destroy(effect, 2f);
        //2s后销毁爆炸特效
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        //获取所有敌人数组
        foreach (GameObject enemy in enemys){
            //对所有敌人进行遍历
            if (Vector3.Distance(transform.position, enemy.transform.position) < 3f){
                //若敌人距离玩家的距离小于3m
                if (enemy.transform.parent != null){
                    //若杀死的敌人有父物体，获取其父物体名称
                    //因同种敌人的名称不可相同，但又是同种敌人，因此把同种敌人放到一个父物体以便统一识别
                    string name = enemy.transform.parent.gameObject.name;
                    //对不同名称采取不同行为
                    switch (name){
                        case "Zombie":
                            //若名称为zombie，则将其状态改为死亡
                            enemy.GetComponent<ZombieControl>().zombie_dead=true;break;
                        case "Witch":
                            enemy.GetComponent<WitchControl>().witch_dead = true; break;
                        case "Ghost":
                            enemy.GetComponent<GhostControl>().ghost_dead = true; break;
                        case "Pumpkin":
                            enemy.GetComponent<PumpkinControl>().pumpkin_dead=true;break;
                        case "Devil":
                            enemy.GetComponent<DevilControl>().devil_dead = true; break;
                        default: break;
                    }
                }
                else enemy.GetComponent<Witch_Zombie_Control>().witch_zombie_dead = true;
                //无父物体，说明是Witch生成的Zombie
                if (enemy.GetComponent<Rigidbody>()!=null) enemy.GetComponent<Rigidbody>().AddForce((enemy.transform.position-transform.position)*5f, ForceMode.Impulse);
                //对敌人施加一个瞬间的力，来模拟炸飞效果，方向由炸弹指向敌人
            }
        }
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(Booming, maincamera.transform.position, 0.5f);
        //生成一个爆炸音效在摄像机位置，音量为1倍
        Destroy(gameObject);
        //销毁炸弹物体
    }
    void Start(){
        player = GetComponent<AudioSource>();
        //获取音频播放器组件
        player.PlayOneShot(Throw, 0.3f);
        //播放放炸弹音效，音量为0.1倍
        Invoke("Boom", 2f);
        //2s后执行Boom方法
    }
}
