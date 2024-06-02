using UnityEngine;

public class WitchControl : MonoBehaviour
{
    public GameObject Witch_Zombie;
    //�ͷ�zombie CD
    private float CD = 6;
    //Witch��AudioSource���
    private AudioSource Witch_Voice;
    //Witch��������Ч����
    public AudioClip[] Witch_Say;
    //������Ч���ŵ�ʱ����
    private float Voice_CD;
    //witch���ٶ�
    public float witch_speed = 1f;
    //witch�ĸ�֪��Χ
    public float witch_cooleye = 10f;
    //witch������
    [Range(0, 1f)] public float witch_volum = 0.4f;
    //witch��״̬
    public bool witch_dead = false;
    //witch��������Ч
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
            //��������Witch��10m��Χ�ڣ��ڲ�����Ч
            Voice();
            transform.LookAt(player.transform);
            //Զ�����
            transform.Translate(Vector3.back * witch_speed * Time.deltaTime);
            if (CD > 6){//��CD����6
                CD = 0;
                //������λ������һ��witch_zombie
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
        //��ȡAudioSource���
        Witch_Voice=GetComponent<AudioSource>();
        //��ʼ��һ����Χ�ڵ����ֵ��Voice_CD����ֹ��һ����������ʱ����ȫ��һ�𷢳���Ч
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
    }
    private void Update(){
        CD += Time.deltaTime;
        //��������Чʱ�������м�ʱ
        Voice_CD += Time.deltaTime;
        if(witch_dead) dead();
    }
    private void FixedUpdate(){
        AwayPlayer();
    }
}
