using UnityEngine;

/*
 *���ܣ�ʵ�� ��ħ �Ļ�����Ϊ�߼�������������ң��������ʱɱ����ң����Ž�����������ɱ�������Ч
 *ʵ�ַ������������ͨ��������� ��ħ �ľ���С��һ��ֵʱ��ʹ ��ħ ������ң�������ǰ��ǰ��ʵ�֣�
 *         ����ʱɱ�����ͨ����ײ��ⷽ�����������ײ���������Ƿ�Ϊ��knight���������������Ϸʵ�֣�
 *         ������Чͨ��������Ч���
 */

public class DevilControl : MonoBehaviour
{
    private AudioSource Devil_Voice;
    //devil��������Ч����
    public AudioClip[] Devil_Say;
    //������Ч���ŵ�ʱ����
    private float Voice_CD;
    //devil���ٶ�
    public float devil_speed = 5f;
    //devil�ĸ�֪��Χ
    public float devil_cooleye = 15f;
    //devil������
    [Range(0,1f)]public float devil_volum = 1f;
    //devil��״̬
    public bool devil_dead = false;
    //devil��������Ч
    public AudioClip[] devil_dead_say;
    private GameObject player;
    private float distance=0;
    private void Voice(){//����devil��������Ч
        if (Voice_CD > Random.Range(7, 12)){//���һ�������ʹ�����������
            Voice_CD = 0;
            //����������Ч������CD
            Devil_Voice.PlayOneShot(Devil_Say[Random.Range(0, Devil_Say.Length)], devil_volum);
            //������ŵ���������Ч�����е�����һ����Ч
        }
    }
    private void FindPlayer(){//׷�����
        distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance < devil_cooleye){//����ҽ���devil�ĸ�֪��Χ��
            Voice();
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * devil_speed * Time.deltaTime);
        }
    }
    private void GameOver(){//��ȡɱ����ҵĵ�����Ϣ
        Time.timeScale = 0;
        //��ͣ��Ϸ
        player.GetComponent<RestartGame>().Enemy_name = "Devil";
        //��ɱ����ҵĵ������ƽ��м�¼
    }
    private void Enemy_die_voice(){//��������ʱ���������λ���������һ��������Ч��ȷ����������������Ϊ1��
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(devil_dead_say[Random.Range(0, devil_dead_say.Length)], maincamera.transform.position, 0.5f);
        //��ΪPlayClipAtPoint���ɵ���Ч��3D��Ч���������˥������̫�ÿ��ƣ���������Ϲ�������Ч�����������Ըɴ������������λ�þͲ��õ��ľ���˥����
    }
    private void dead(){//���Ƶ��˵�������Ϊ
        Enemy_die_voice();
        //���������x��y��z��ʹʬ��ӵ����������Ч��
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //��tag��Ϊcorpse
        tag = "corpse";
        //ʹ����������һ
        player.GetComponent<PlayerControl>().enemy_num--;
        //�ر��ж��ű�
        GetComponent<DevilControl>().enabled = false;
    }
    private void Start(){
        Devil_Voice = GetComponent<AudioSource>();
        Voice_CD = Random.Range(5, 15);
        //��ʼ��һ����Χ�ڵ����ֵ��Voice_CD����ֹ��һ����������ʱ����ȫ��һ�𷢳���Ч
        player = GameObject.FindWithTag("Player");
    }

    private void Update(){
        //����Чʱ�������м�ʱ
        Voice_CD+= Time.deltaTime;
        //ִ����������
        if(devil_dead) dead();
    }
    // Update is called once per frame
    private void FixedUpdate(){
        //׷�����
        FindPlayer();
    }

    private void OnCollisionEnter(Collision collision){//����Ƿ��������
        if (!devil_dead){//��û��
            if (collision.gameObject.name == "Knight") GameOver();
        }
    }
}
