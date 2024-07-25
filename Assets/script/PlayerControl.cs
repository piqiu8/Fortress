//#define DEBUG_MODE

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ��������ж�
/// </summary>
public class PlayerControl : MonoBehaviour
{
    /// <summary>
    /// ը������
    /// </summary>
    public GameObject bomb;

    /// <summary>
    /// ը������λ��
    /// </summary>
    public GameObject bumb;

    /// <summary>
    /// ը���ͷ�CD��Ĭ��Ϊ2
    /// </summary>
    //private Rigidbody mmRigidbody;
    private float CD = 2;

    /// <summary>
    /// ��ʱ����������ʱ����Ĭ��Ϊ1
    /// </summary>
    private float CD2 = 1;

    /// <summary>
    /// �Ų���ЧCD
    /// </summary>
    private float time = 0f;

    /// <summary>
    /// ��ҷ������������ڿ�������ƶ�����
    /// </summary>
    private Vector3 dir;

    /// <summary>
    /// ը����ը״̬
    /// </summary>
    private bool isbumb;

    /// <summary>
    /// ��Ϸʤ��UI����
    /// </summary>
    public GameObject win;

    /// <summary>
    /// ��Ƶ������
    /// </summary>
    private AudioSource player;

    /// <summary>
    /// �ݵ���·��Ч
    /// </summary>
    [Header("��·��Ч�趨")]
    public AudioClip[] terrain_audio_clips;

    /// <summary>
    /// ʯ����·��Ч
    /// </summary>
    public AudioClip[] floor_audio_clips;

    /// <summary>
    /// ľ����·��Ч�趨
    /// </summary>
    public AudioClip[] wood_audio_clips;

    /// <summary>
    /// �������ֲ�����
    /// </summary>
    private AudioSource BackgroundMusic;

    /// <summary>
    /// ����ٶȣ�Ĭ��Ϊ6
    /// </summary>
    [Header("����ٶ��趨")]
    public float player_speed = 6f;

    /// <summary>
    /// ��������
    /// </summary>
    [HideInInspector] public int enemy_num;

    /// <summary>
    /// ��ȡ�������Ը�����ҷ���
    /// </summary>
    private void GetAxis()
    {
        //��ȡˮƽ������
        float horizontal = Input.GetAxis("Horizontal");
        //��ȡ��ֱ������
        float vertical = Input.GetAxis("Vertical");
        //���ݴ�ֱ��ˮƽ������ȷ����ҷ���
        dir = new Vector3(horizontal, 0, vertical);
    }

    /// <summary>
    /// ���F�������ڷ�ը��
    /// </summary>
    private void GetKey()
    {
        //ͨ��GetKeyDowm��ʽ��ⰴ��F
        if (Input.GetKeyDown(KeyCode.F)) isbumb = true;
        //GetKey()����ͨ������ָ���İ������û���סʱ����true����ס���ǰ�ס�����ǳ�������ʶ������������ƽ�ɫ���㰴ס�����ʱ�ƶ�����ô������GetKey()
        //GetKeyDown()����ͨ����������ָ�����Ƶİ���ʱ����һ֡ʱ����true����ס������һ֡����һ�µ����飬�����㰴��ã�ֻ�����㰴�µ���һ˲��
        //GetKeyUp()����ͨ�����ͷţ���������ʱ���������ֵİ�������һ֡����true����ס������һ֡����һ�µ����顣
    }

    /// <summary>
    /// ���Ʒ�ը��
    /// </summary>
    private void PutBomb()
    {
        if (isbumb)
        {
            //�Ѿ�����F���ͷ�ը������˽�״̬����
            isbumb = false;
            if (CD > 2)
            {
                //CD����ʱ�����2ʱը����ȴ�����������ٴ��ͷ�ը��������CDʱ����տ�ʼ���ȼ���CD
                CD = 0;
                //��bumb��λ������ը��
                Instantiate(bomb, bumb.transform.position, bumb.transform.rotation);
            }
        }
    }

    /// <summary>
    /// ����CD
    /// </summary>
    private void CDtime()
    {
        //����ը��CDʱ��
        CD += Time.deltaTime;
        //������·��ЧCDʱ��
        CD2 += Time.deltaTime;
        //����ʱ��ʹ�õ�������ʱ�䣬��ͬ�豸��֡���ǲ�һ���ģ���unity��ķ�����ÿ֡����һ�εģ�������ͨ�������㣬�뵼�¼��㲻׼ȷ�Ҳ�ͬ�豸�ļ�����Ҳ��̫ͬ
        //��֡��Ϊ1��30֡��������ʱ��Ϊ1/֡�� s���������ܱ�֤��ͬ�豸�����ʱ�䶼��һ���ģ���Ϊ��Ȼ֡�ʲ�ͬ��������1���֡��
    }

    /// <summary>
    /// ��������ƶ�
    /// </summary>
    private void Move()
    {
        if (dir != Vector3.zero)
        {
            //����һ����Ԫ�������濴��dir������Ҫ����ת��
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            //Ҳ����������ת��������Ȼ��transform.rotation = Quaternion.LookRotation(dir);
            //��Ϊ����ת�ƶ�������ͨ�����β�ֵ������ת��ʹ��ת����ƽ�������ٶ�Ϊ10��/s
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            //��ײ����Ȼ���˶���ʽ������Ҫ���������rigidbody.MovePosition(rigidbody.position + speed * Time.deltaTime);
            //mmRigidbody.MovePosition(transform.position + dir * 0.15f);
            //��ǰ��������ƶ����ٶ�Ϊ6m/s������˵6����λ/s
            transform.Translate(Vector3.forward * player_speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// ������Ϸ�Ƿ�ʤ��
    /// </summary>
    private void GameWin()
    {
        //��ȡ���е���
        //GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        //����������Ϊ0�������Ϸʤ��
        if (enemy_num == 0)
        {
            //��ʼ��ʱ
            time += Time.fixedUnscaledDeltaTime;
            //��ͣ��Ϸʱ��
            Time.timeScale = 0;
            //ֹͣ��������
            BackgroundMusic.Stop();
            //ʹEsc���ʧЧ
            GetComponent<Esc>().enabled = false;
            //����ʤ��UI
            win.SetActive(true);
            //�ȴ�10s�ſɰ�������������
            if (time > 10)
            {
                //���������ʹ������ת����ʼ����
                if (Input.anyKey)
                {
                    time = 0;
                    SceneManager.LoadScene(0);
                    Time.timeScale = 1;
                }
            }
        }
    }

    private void Start()
    {
        player = GetComponent<AudioSource>();
        //mmRigidbody = gameObject.GetComponent<Rigidbody>();
        BackgroundMusic = GameObject.Find("MusicTrigger").GetComponent<AudioSource>();
        enemy_num = GameObject.FindGameObjectsWithTag("Enemy").Length;
#if DEBUG_MODE
        if (!player)
        {
            Debug.LogError("δ��ȡ��AudioSource���");
            return;
        }
        if (!BackgroundMusic)
        {
            Debug.LogError("δ��ȡ���������ֲ�����");
            return;
        }
        if (!PlayerControlDeBug())
        {
            Debug.LogError("��ʼ��ʧ��");
            return;
        }
#endif
    }

    private void Update()
    {
        GetAxis();
        GetKey();
        CDtime();
        GameWin();
    }

    private void FixedUpdate()
    {
        PutBomb();
        Move();
        //GameWin();
    }

    /// <summary>
    /// ������ŽŲ���Ч
    /// </summary>
    /// <param name="s">�Ӵ�����������</param>
    private void play_clip(string s)
    {
        //��Ϊterrain�ؿ飬�򲥷Ŷ�Ӧ�������·��Ч
        if (s == "Terrain") player.PlayOneShot(terrain_audio_clips[Random.Range(0, terrain_audio_clips.Length)]);
        else
        {
            if (s == "floor") player.PlayOneShot(floor_audio_clips[Random.Range(0, floor_audio_clips.Length)]);
            else player.PlayOneShot(wood_audio_clips[Random.Range(0, wood_audio_clips.Length)]);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (CD2 > 0.5)
        {
            //�Ų���ЧCD��ȴ����򲥷ŽŲ���Ч�����������0�ٴν�����ȴ
            CD2 = 0;
            //�������������������Ƿ�ΪTerrain�����򲥷Ŷ�Ӧ��Ч
            if (collision.gameObject.name == "Terrain") play_clip("Terrain");
            else
            {
                if (collision.gameObject.transform.parent != null)
                {
                    if (collision.gameObject.transform.parent.gameObject.tag == "floor") play_clip("floor");
                    else if (collision.gameObject.transform.tag == "wood") play_clip("wood");
                }
            }
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// PlayerControl��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool PlayerControlDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(bomb, "bomb/ը������δ��ʼ��");
        checkNull.Check(bumb, "bumb/����λ��δ��ʼ��");
        checkNull.Check(win, "win/ʤ��UIδ��ʼ��");
        checkNull.Check(player, "player/��Ƶ������δ��ʼ��");
        checkNull.Check(terrain_audio_clips, "terrain_audio_clips/�ݵ���·��Чδ��ʼ��");
        checkNull.Check(floor_audio_clips, "floor_audio_clips/ʯ����·��Чδ��ʼ��");
        checkNull.Check(wood_audio_clips, "wood_audio_clips/ľ����·��Чδ��ʼ��");
        checkNull.Check(BackgroundMusic, "BackgroundMusic/�������ֲ�����δ��ʼ��");
        return checkNull.State;
    }
#endif
}