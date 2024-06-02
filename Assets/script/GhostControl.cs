using UnityEngine;
public class GhostControl : MonoBehaviour
{
    //ghost的AudioSource组件
    private AudioSource Ghost_Voice;
    //ghost的声音音效集合
    public AudioClip[] Ghost_Say;
    //声音音效播放的时间间隔
    private float Voice_CD;
    //ghost的速度
    public float ghost_speed = 2f;
    //ghost的感知范围
    public float ghost_cooleye = 20f;
    //ghost的音量
    [Range(0, 1f)]public float ghost_volum = 1f;
    //ghost的状态
    public bool ghost_dead = false;
    //ghost的死亡音效
    public AudioClip[] ghost_dead_say;
    private float distance = 0;
    private GameObject player;
    private void Voice(){
        if (Voice_CD > Random.Range(7, 12)){
            Voice_CD = 0;
            Ghost_Voice.PlayOneShot(Ghost_Say[Random.Range(0, Ghost_Say.Length)], ghost_volum);
        }
    }
    private void FindPlayer(){
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < ghost_cooleye){
            Voice();
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * ghost_speed *Time.deltaTime);
        }
    }
    private void GameOver(){
        if (distance < 1){
            //因ghost没有碰撞检测，所以需要手动更改玩家死亡状态
            player.GetComponent<IfDie>().IsDie = true;
            //表明杀死玩家的敌人名字
            Time.timeScale = 0;
            player.GetComponent<RestartGame>().Enemy_name = "Ghost";
        }
    }
    private void Enemy_die_voice(){
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(ghost_dead_say[Random.Range(0, ghost_dead_say.Length)], maincamera.transform.position, 0.5f);
    }
    private void dead(){
        Enemy_die_voice();

        player.GetComponent<PlayerControl>().enemy_num--;

        Destroy(gameObject);
    }
    void Start(){
        //获取AudioSource组件
        Ghost_Voice = GetComponent<AudioSource>();
        //初始化一个范围内的随机值给Voice_CD，防止第一次遇见敌人时敌人全部一起发出音效
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update(){
        if (ghost_dead) dead();
        Voice_CD += Time.deltaTime;
        FindPlayer();
        GameOver();
    }
}
