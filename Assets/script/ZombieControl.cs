using UnityEngine;
public class ZombieControl : MonoBehaviour
{
    //zombie音频播放器
    private AudioSource ZombieVoice;
    //zombie叫声音效
    public AudioClip[] Zombie_say;
    //音效CD
    private float Voice_CD;
    //zombie的速度
    public float zombie_speed = 3f;
    //zombie的感知范围
    public float zombie_cooleye = 10f;
    //zombie的音量
    [Range(0, 1f)] public float zombie_volum = 0.3f;
    //zombie的状态
    public bool zombie_dead = false;
    //zombie的死亡音效
    public AudioClip[] Zombie_dead_say;
    private GameObject player;
    private float distance = 0;
    private void Voice(){
        if (Voice_CD > Random.Range(7, 12)){
            Voice_CD = 0;
            ZombieVoice.PlayOneShot(Zombie_say[Random.Range(0, Zombie_say.Length)], zombie_volum);
        }
    }
    private void FindPlayer(){
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < zombie_cooleye){
            Voice();
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * zombie_speed * Time.deltaTime);
        }
    }
    private void GameOver(){
        Time.timeScale = 0;
        player.GetComponent<RestartGame>().Enemy_name ="Zombie";
    }
    private void Enemy_die_voice(){
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(Zombie_dead_say[Random.Range(0, Zombie_dead_say.Length)], maincamera.transform.position, 0.5f);
    }
    private void dead(){
        Enemy_die_voice();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        tag = "corpse";

        player.GetComponent<PlayerControl>().enemy_num--;

        GetComponent <ZombieControl> ().enabled = false;
    }
    void Start(){
        ZombieVoice = GetComponent<AudioSource>();
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
    }
    private void Update(){
        Voice_CD += Time.deltaTime;
        if(zombie_dead) dead();
    }
    private void FixedUpdate(){
        FindPlayer();
    }
    private void OnCollisionEnter(Collision collision){
        if (!zombie_dead){//若自身没死
            if (collision.gameObject.name == "Knight") GameOver();
        }
    }
}
