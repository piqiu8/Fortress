using UnityEngine;

public class PumpkinControl : MonoBehaviour
{
    //pumpkin��AudioSource���
    private AudioSource Pumpkin_Voice;
    //pumpkin��������Ч����
    public AudioClip[] Pump_Say;
    //������Ч���ŵ�ʱ����
    private float Voice_CD;
    //pumpkin���ٶ�
    public float pumpkin_speed = 10f;
    //pump�ĸ�֪��Χ
    public float pump_cooleye = 2f;
    //pumpkin������
    [Range(0, 1f)]public float pump_volum = 0.3f;
    //pumpkim��״̬
    public bool pumpkin_dead = false;
    //pumpkin��������Ч
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
        //�Ƿ�����ҵľ���С��10m
        if (distance < 10){
            //�ǣ�������Ч
            Voice();
            //�Ƿ�����Ҿ���С��2m
            if (distance < pump_cooleye){
                //�ǣ��������
                transform.LookAt(player.transform);
                //��10m/s���ٶȽӽ����
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
        //��ȡAudioSource���
        Pumpkin_Voice = GetComponent<AudioSource>();
        //��ʼ��һ����Χ�ڵ����ֵ��Voice_CD����ֹ��һ����������ʱ����ȫ��һ�𷢳���Ч
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
    }
    
    private void Update(){
        //����Чʱ�������м�ʱ
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
