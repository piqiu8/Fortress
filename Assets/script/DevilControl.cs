//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// ʵ�� ��ħ �Ļ�����Ϊ�߼�������������ң��������ʱɱ����ң����Ž�����������ɱ�������Ч |
/// ʵ�ַ������������ͨ��������� ��ħ �ľ���С��һ��ֵʱ��ʹ ��ħ ������ң�������ǰ��ǰ��ʵ�֣�
/// ����ʱɱ�����ͨ����ײ��ⷽ�����������ײ���������Ƿ�Ϊ��knight���������������Ϸʵ�֣�
/// ������Чͨ��������Ч���
/// </summary>
public class DevilControl : MonoBehaviour
{
    /// <summary>
    /// ��ħ��Ч������
    /// </summary>
    private AudioSource Devil_Voice;

    /// <summary>
    /// ��ħ��Ч����
    /// </summary>
    public AudioClip[] Devil_Say;

    /// <summary>
    /// ������Ч���ŵ�ʱ����
    /// </summary>
    private float Voice_CD;

    /// <summary>
    /// ��ħ���ٶȣ�Ĭ��Ϊ5
    /// </summary>
    public float devil_speed = 5f;

    /// <summary>
    /// ��ħ�ĸ�֪��Χ��Ĭ��Ϊ15
    /// </summary>
    public float devil_cooleye = 15f;

    /// <summary>
    /// ��ħ��������Ĭ��Ϊ1��
    /// </summary>
    [Range(0, 1f)] public float devil_volum = 1f;

    /// <summary>
    /// ��ħ�Ĵ��״̬
    /// </summary>
    public bool devil_dead = false;

    /// <summary>
    /// ��ħ��������Ч����
    /// </summary>
    public AudioClip[] devil_dead_say;

    /// <summary>
    /// �������
    /// </summary>
    private GameObject player;

    /// <summary>
    /// ����ҵľ���
    /// </summary>
    private float distance = 0;

    /// <summary>
    /// ���Ŷ�ħ��������Ч
    /// </summary>
    private void Voice()
    {
        //���һ�������ʹ�����������
        if (Voice_CD > Random.Range(7, 12))
        {
            //����������Ч������CD
            Voice_CD = 0;
            //������ŵ���������Ч�����е�����һ����Ч
            Devil_Voice.PlayOneShot(Devil_Say[Random.Range(0, Devil_Say.Length)], devil_volum);
        }
    }

    /// <summary>
    /// ׷�����
    /// </summary>
    private void FindPlayer()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        //����ҽ���devil�ĸ�֪��Χ��
        if (distance < devil_cooleye)
        {
            Voice();
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * devil_speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// ��ȡɱ����ҵĵ�����Ϣ
    /// </summary>
    private void GameOver()
    {
        //��ͣ��Ϸ
        Time.timeScale = 0;
        //��ɱ����ҵĵ������ƽ��м�¼
        player.GetComponent<RestartGame>().Enemy_name = "Devil";
    }

    /// <summary>
    /// ���ŵ���������Ч
    /// </summary>
    private void Enemy_die_voice()
    {
        GameObject maincamera = GameObject.Find("Main Camera");
#if DEBUG_MODE
        if (!maincamera)
        {
            Debug.LogError("δ��ȡ�������");
            return;
        }
#endif
        //��������ʱ���������λ���������һ��������Ч��ȷ����������������Ϊ1��
        AudioSource.PlayClipAtPoint(devil_dead_say[Random.Range(0, devil_dead_say.Length)], maincamera.transform.position, 0.5f);
        //��ΪPlayClipAtPoint���ɵ���Ч��3D��Ч���������˥������̫�ÿ��ƣ���������Ϲ�������Ч�����������Ըɴ������������λ�þͲ��õ��ľ���˥����
    }

    /// <summary>
    /// ���Ƶ���������Ϊ
    /// </summary>
    private void dead()
    {
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

    private void Start()
    {
        Devil_Voice = GetComponent<AudioSource>();
        Voice_CD = Random.Range(5, 15);
        //��ʼ��һ����Χ�ڵ����ֵ��Voice_CD����ֹ��һ����������ʱ����ȫ��һ�𷢳���Ч
        player = GameObject.FindWithTag("Player");
#if DEBUG_MODE
        if (!Devil_Voice)
        {
            Debug.LogError("δ��ȡ��AudioSource���");
            return;
        }
        if (!player)
        {
            Debug.LogError("δ��ȡ���������");
            return;
        }
        if (!DevilControlDeBug())
        {
            Debug.LogError("��ʼ��ʧ��");
            return;
        }
#endif
    }

    private void Update()
    {
        //����Чʱ�������м�ʱ
        Voice_CD += Time.deltaTime;
        //ִ����������
        if (devil_dead) dead();
    }

    private void FixedUpdate()
    {
        //׷�����
        FindPlayer();
    }

    /// <summary>
    /// ����Ƿ��������
    /// </summary>
    /// <param name="collision">��ײ����Ϣ</param>
    private void OnCollisionEnter(Collision collision)
    {
        //��û��
        if (!devil_dead)
        {
            if (collision.gameObject.name == "Knight") GameOver();
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// DevilControl��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool DevilControlDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(Devil_Voice, "Devil_Voice/��ħ��Ч������δ��ֵ");
        checkNull.Check(Devil_Say, "Devil_Say/��ħ��Ч����δ��ֵ");
        checkNull.Check(devil_dead_say, "devil_dead_say/��ħ������Ч����δ��ֵ");
        checkNull.Check(player, "player/�������δ��ֵ");
        return checkNull.State;
    }
#endif
}