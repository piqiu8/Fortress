using UnityEngine;

public class Witch_Zombie_Control : MonoBehaviour
{
    //Witch_zombie音频播放器
    private AudioSource Witch_Zombie_Voice;
    //Witch_zombie叫声音效
    public AudioClip[] Witch_Zombie_say;
    //音效CD
    private float Voice_CD;
    //Witch_Zombie的速度
    public float Witch_Zombie_speed = 3f;
    //Witch_Zombie的感知范围
    public float Witch_zombie_cooleye = 10f;
    //Witch_Zombie的音量
    [Range(0, 1f)] public float Witch_Zombie_volum = 0.3f;
    //witch_zombie的状态
    public bool witch_zombie_dead = false;
    //witch_zombie的死亡音效
    public AudioClip[] witch_zombie_dead_say;
    private GameObject player;
    private float distance = 0;
    private void Voice(){
        if (Voice_CD > Random.Range(7, 12)){
            Voice_CD = 0;
            Witch_Zombie_Voice.PlayOneShot(Witch_Zombie_say[Random.Range(0, Witch_Zombie_say.Length)], Witch_Zombie_volum);
        }
    }
    private void FindPlayer(){
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < Witch_zombie_cooleye){
            Voice();
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * Witch_Zombie_speed *Time.deltaTime);
        }
    }
    private void GameOver(){
        Time.timeScale = 0;
        player.GetComponent<RestartGame>().Enemy_name = "Witch";
    }
    private void Enemy_die_voice(){
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(witch_zombie_dead_say[Random.Range(0, witch_zombie_dead_say.Length)], maincamera.transform.position, 0.5f);
    }
    private void dead(){
        Enemy_die_voice();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        tag = "corpse";

        player.GetComponent<PlayerControl>().enemy_num--;

        GetComponent <Witch_Zombie_Control> ().enabled = false;
    }
    void Start(){
        Witch_Zombie_Voice = GetComponent<AudioSource>();
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
    }
    private void Update(){
        Voice_CD += Time.deltaTime;
        if(witch_zombie_dead) dead();
    }
    private void FixedUpdate(){
        FindPlayer();
    }
    private void OnCollisionEnter(Collision collision){
        if (!witch_zombie_dead){
            if (collision.gameObject.name == "Knight") GameOver();
        }
    }
}
