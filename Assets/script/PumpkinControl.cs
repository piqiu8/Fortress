using UnityEngine;

public class PumpkinControl : MonoBehaviour
{
    //pumpkin的AudioSource组件
    private AudioSource Pumpkin_Voice;
    //pumpkin的声音音效集合
    public AudioClip[] Pump_Say;
    //声音音效播放的时间间隔
    private float Voice_CD;
    //pumpkin的速度
    public float pumpkin_speed = 10f;
    //pump的感知范围
    public float pump_cooleye = 2f;
    //pumpkin的音量
    [Range(0, 1f)]public float pump_volum = 0.3f;
    //pumpkim的状态
    public bool pumpkin_dead = false;
    //pumpkin的死亡音效
    public AudioClip[] pumpkin_dead_say;
    private GameObject player;
    private float distance = 0;
    private void Voice(){
        if (Voice_CD > Random.Range(7, 12)){
            Voice_CD = 0;
            Pumpkin_Voice.PlayOneShot(Pump_Say[Random.Range(0, Pump_Say.Length)], pump_volum);
        }
    }
    private void FindPlayer(){
        distance = Vector3.Distance(player.transform.position, transform.position);
        //是否与玩家的距离小于10m
        if (distance < 10){
            //是，产生音效
            Voice();
            //是否与玩家距离小于2m
            if (distance < pump_cooleye){
                //是，看向玩家
                transform.LookAt(player.transform);
                //以10m/s的速度接近玩家
                transform.Translate(Vector3.forward * pumpkin_speed * Time.deltaTime);
            }
        }
    }
    private void GameOver(){
        Time.timeScale = 0;
        player.GetComponent<RestartGame>().Enemy_name = "Pumpkin";
    }
    private void Enemy_die_voice(){
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(pumpkin_dead_say[Random.Range(0, pumpkin_dead_say.Length)], maincamera.transform.position, 0.5f);
    }
    private void dead(){
        Enemy_die_voice();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        tag = "corpse";

        player.GetComponent<PlayerControl>().enemy_num--;

        GetComponent <PumpkinControl> ().enabled = false;
    }
    void Start(){
        //获取AudioSource组件
        Pumpkin_Voice = GetComponent<AudioSource>();
        //初始化一个范围内的随机值给Voice_CD，防止第一次遇见敌人时敌人全部一起发出音效
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
    }
    
    private void Update(){
        //对音效时间间隔进行计时
        Voice_CD += Time.deltaTime;
        if(pumpkin_dead) dead();
    }
    private void FixedUpdate(){
        FindPlayer();
    }

    private void OnCollisionEnter(Collision collision){
        if (!pumpkin_dead){
            if (collision.gameObject.name == "Knight") GameOver();
        }
    }
}
