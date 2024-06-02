using UnityEngine;
public class GhostControl : MonoBehaviour
{
    //ghost��AudioSource���
    private AudioSource Ghost_Voice;
    //ghost��������Ч����
    public AudioClip[] Ghost_Say;
    //������Ч���ŵ�ʱ����
    private float Voice_CD;
    //ghost���ٶ�
    public float ghost_speed = 2f;
    //ghost�ĸ�֪��Χ
    public float ghost_cooleye = 20f;
    //ghost������
    [Range(0, 1f)]public float ghost_volum = 1f;
    //ghost��״̬
    public bool ghost_dead = false;
    //ghost��������Ч
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
            //��ghostû����ײ��⣬������Ҫ�ֶ������������״̬
            player.GetComponent<IfDie>().IsDie = true;
            //����ɱ����ҵĵ�������
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
        //��ȡAudioSource���
        Ghost_Voice = GetComponent<AudioSource>();
        //��ʼ��һ����Χ�ڵ����ֵ��Voice_CD����ֹ��һ����������ʱ����ȫ��һ�𷢳���Ч
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
