using UnityEngine;

public class WitchControl : MonoBehaviour
{
    public GameObject Witch_Zombie;
    //释放zombie CD
    private float CD = 6;
    //Witch的AudioSource组件
    private AudioSource Witch_Voice;
    //Witch的声音音效集合
    public AudioClip[] Witch_Say;
    //声音音效播放的时间间隔
    private float Voice_CD;
    //witch的速度
    public float witch_speed = 1f;
    //witch的感知范围
    public float witch_cooleye = 10f;
    //witch的音量
    [Range(0, 1f)] public float witch_volum = 0.4f;
    //witch的状态
    public bool witch_dead = false;
    //witch的死亡音效
    public AudioClip[] witch_dead_say;
    private GameObject player;
    private float distance = 0;
    private void Voice(){
        if (Voice_CD > Random.Range(7, 12)){
            Voice_CD = 0;
            Witch_Voice.PlayOneShot(Witch_Say[Random.Range(0, Witch_Say.Length)], witch_volum);
        }
    }
    private void AwayPlayer(){
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < witch_cooleye){
            //如果玩家在Witch的10m范围内，内产生音效
            Voice();
            transform.LookAt(player.transform);
            //远离玩家
            transform.Translate(Vector3.back * witch_speed * Time.deltaTime);
            if (CD > 6){//若CD大于6
                CD = 0;
                //在自身位置生成一个witch_zombie
                Instantiate(Witch_Zombie, transform.position, transform.rotation);

                player.GetComponent<PlayerControl>().enemy_num++;

            }
        }
    }
    private void Enemy_die_voice(){
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(witch_dead_say[Random.Range(0, witch_dead_say.Length)], maincamera.transform.position, 0.5f);
    }
    private void dead(){
        Enemy_die_voice();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        tag = "corpse";

        player.GetComponent<PlayerControl>().enemy_num--;

        GetComponent <WitchControl> ().enabled = false;
    }
    void Start(){
        //获取AudioSource组件
        Witch_Voice=GetComponent<AudioSource>();
        //初始化一个范围内的随机值给Voice_CD，防止第一次遇见敌人时敌人全部一起发出音效
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
    }
    private void Update(){
        CD += Time.deltaTime;
        //对声音音效时间间隔进行计时
        Voice_CD += Time.deltaTime;
        if(witch_dead) dead();
    }
    private void FixedUpdate(){
        AwayPlayer();
    }
}
