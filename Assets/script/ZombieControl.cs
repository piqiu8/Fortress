using UnityEngine;
public class ZombieControl : MonoBehaviour
{
    //zombie��Ƶ������
    private AudioSource ZombieVoice;
    //zombie������Ч
    public AudioClip[] Zombie_say;
    //��ЧCD
    private float Voice_CD;
    //zombie���ٶ�
    public float zombie_speed = 3f;
    //zombie�ĸ�֪��Χ
    public float zombie_cooleye = 10f;
    //zombie������
    [Range(0, 1f)] public float zombie_volum = 0.3f;
    //zombie��״̬
    public bool zombie_dead = false;
    //zombie��������Ч
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
        if (!zombie_dead){//������û��
            if (collision.gameObject.name == "Knight") GameOver();
        }
    }
}
