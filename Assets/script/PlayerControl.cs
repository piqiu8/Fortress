using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bomb;
    //ը������
    public GameObject bumb;
    //ը������λ��
    //private Rigidbody mmRigidbody;
    private float CD = 2;
    //ը���ͷ�CDʱ��
    private float CD2 = 1;
    //��ʱ���������ӳٴ���
    private float time = 0f;
    //�Ų���ЧCDʱ��
    private Vector3 dir;
    //��ҷ������������ڿ�������ƶ�����
    private bool isbumb;
    //��������ը���ͷ�
    public GameObject win;
    //��Ϸʤ��UI����
    private AudioSource player;
    [Header("��·��Ч�趨")]
    //��ҽ�ɫ����Ƶ���
    public AudioClip[] terrain_audio_clips;
    //�ݵ���·��Ч
    public AudioClip[] floor_audio_clips;
    //ʯ�ذ���·��Ч
    public AudioClip[] wood_audio_clips;
    //ľ����·��Ч
    private AudioSource BackgroundMusic;
    //�����������ֲ�����
    [Header("����ٶ��趨")]
    public float player_speed = 6f;

    [HideInInspector]public int enemy_num;

    private void GetAxis(){//��ȡ�������Ը�����ҷ���
        float horizontal = Input.GetAxis("Horizontal");
        //��ȡˮƽ������
        float vertical = Input.GetAxis("Vertical");
        //��ȡ��ֱ������
        dir = new Vector3(horizontal, 0, vertical);
        //���ݴ�ֱ��������ȷ����ҷ���
    }
    private void GetKey(){//���F�����������ͷ�ը��
        if (Input.GetKeyDown(KeyCode.F)) isbumb = true;
        //ͨ��GetKeyDowm��ʽ��ⰴ��F
        //GetKey()����ͨ������ָ���İ������û���סʱ����true����ס���ǰ�ס�����ǳ�������ʶ������������ƽ�ɫ���㰴ס�����ʱ�ƶ�����ô������GetKey()
        //GetKeyDown()����ͨ����������ָ�����Ƶİ���ʱ����һ֡ʱ����true����ס������һ֡����һ�µ����飬�����㰴��ã�ֻ�����㰴�µ���һ˲��
        //GetKeyUp()����ͨ�����ͷţ���������ʱ���������ֵİ�������һ֡����true����ס������һ֡����һ�µ����顣
    }

    private void PutBomb(){//����ը���ͷ�
        if (isbumb){
            isbumb = false;
            //�Ѿ�����F���ͷ�ը������˽�״̬����
            if (CD > 2){
                CD = 0;
                //CD����ʱ�����2ʱը����ȴ�����������ٴ��ͷ�ը��������CDʱ����տ�ʼ���ȼ���CD
                Instantiate(bomb, bumb.transform.position, bumb.transform.rotation);
                //��bumb��λ������ը��
            }
        }
    }

    private void CDtime(){//����CDʱ��
        CD += Time.deltaTime;
        //����ը��CDʱ��
        CD2 += Time.deltaTime;
        //������·��ЧCDʱ��
        //����ʱ��ʹ�õ�������ʱ�䣬��ͬ�豸��֡���ǲ�һ���ģ���unity��ķ�����ÿ֡����һ�εģ�������ͨ�������㣬�뵼�¼��㲻׼ȷ�Ҳ�ͬ�豸�ļ�����Ҳ��̫ͬ
        //��֡��Ϊ1��30֡��������ʱ��Ϊ1/֡�� s���������ܱ�֤��ͬ�豸�����ʱ�䶼��һ���ģ���Ϊ��Ȼ֡�ʲ�ͬ��������1���֡��
    }

    private void Move(){//��������ƶ�
        if (dir != Vector3.zero){
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            //Ҳ����������ת��������Ȼ��transform.rotation = Quaternion.LookRotation(dir);
            //����һ����Ԫ�������濴��dir������Ҫ����ת��
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            //��ײ����Ȼ���˶���ʽ������Ҫ���������rigidbody.MovePosition(rigidbody.position + speed * Time.deltaTime);
            //mmRigidbody.MovePosition(transform.position + dir * 0.15f);
            //��Ϊ����ת�ƶ�������ͨ�����β�ֵ������ת��ʹ��ת����ƽ�������ٶ�Ϊ10��/s
            transform.Translate(Vector3.forward * player_speed * Time.deltaTime);
            //��ǰ��������ƶ����ٶ�Ϊ6m/s������˵6����λ/s
        }
    }

    private void GameWin(){//������Ϸ�Ƿ�ʤ��
        //GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        //��ȡ���е���
        if (enemy_num == 0){
            //��ʼ��ʱ
            time += Time.fixedUnscaledDeltaTime;
            //����������Ϊ0�������Ϸʤ��
            Time.timeScale = 0;
            //ֹͣ��������
            BackgroundMusic.Stop();
            //��ͣ��Ϸʱ��
            GetComponent<Esc>().enabled = false;
            //ʹEsc���ʧЧ
            win.SetActive(true);
            //����ʤ��UI
            if (time > 10){//�ȴ�10s�ſɰ�������������
                if (Input.anyKey){
                    time = 0;
                    SceneManager.LoadScene(0);
                    //���������ʹ������ת����ʼ����
                    Time.timeScale = 1;
                    //ʹ��Ϸʱ��ָ�����
                }
            }
        }
    }
    void Start(){//��Ϸ��ʼ�ͻ����һ��
        player = GetComponent<AudioSource>();
        //mmRigidbody = gameObject.GetComponent<Rigidbody>();
        //��ȡ��ҵ���Ƶ�����ʹ����ҽ�ɫ�������������Լ�������Ч
        BackgroundMusic = GameObject.Find("MusicTrigger").GetComponent<AudioSource>();
        //��ȡ�������ֲ�����
        enemy_num= GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    // Update is called once per frame
    void Update(){//��Ϸ��ʼ��ÿ֡�������һ�Σ��Ա�ʵʱ�����Ϸ״̬
        GetAxis();
        GetKey();
        CDtime();
        GameWin();
    }
    private void FixedUpdate(){//�̶�ʱ�����һ�Σ�����֡�ʸı䣬����ʹ����ģ������ȶ�������������ô���������ײ�ķ���
        PutBomb();
        Move();
        //GameWin();
    }

    private void play_clip(string s){//������ŽŲ���Ч
        if (s == "Terrain") player.PlayOneShot(terrain_audio_clips[Random.Range(0, terrain_audio_clips.Length)]);
        //��Ϊterrain�ؿ飬�򲥷Ŷ�Ӧ�������·��Ч
        else{
            if (s == "floor") player.PlayOneShot(floor_audio_clips[Random.Range(0, floor_audio_clips.Length)]);
            else player.PlayOneShot(wood_audio_clips[Random.Range(0, wood_audio_clips.Length)]);
        }
    }
    private void OnCollisionStay(Collision collision){//�Դ�����ײ��ⷽ����collision��ʾ����������
        if (CD2 > 0.5){
            CD2 = 0;
            //�Ų���ЧCD��ȴ����򲥷ŽŲ���Ч�����������0�ٴν�����ȴ
            if (collision.gameObject.name == "Terrain") play_clip("Terrain");
            //�������������������Ƿ�ΪTerrain�����򲥷Ŷ�Ӧ��Ч
            else{
                if (collision.gameObject.transform.parent != null){
                    if (collision.gameObject.transform.parent.gameObject.tag == "floor") play_clip("floor");
                    else if (collision.gameObject.transform.tag == "wood") play_clip("wood");
                }
            }
        }
    }
}
