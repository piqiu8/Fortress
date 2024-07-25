//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// �ϹϿ��ƽű�
/// </summary>
public class PumpkinControl : MonoBehaviour
{
    /// <summary>
    /// �Ϲ���Ƶ���������
    /// </summary>
    private AudioSource Pumpkin_Voice;

    /// <summary>
    /// �Ϲ�������Ч
    /// </summary>
    public AudioClip[] Pump_Say;

    /// <summary>
    /// ������Ч���ŵ�ʱ����
    /// </summary>
    private float Voice_CD;

    /// <summary>
    /// �Ϲϵ��ٶȣ�Ĭ��Ϊ10
    /// </summary>
    public float pumpkin_speed = 10f;

    /// <summary>
    /// �Ϲϵĸ�֪��Χ��Ĭ��Ϊ2
    /// </summary>
    public float pump_cooleye = 2f;

    /// <summary>
    /// �Ϲϵ�������С��Ĭ��Ϊ0.3
    /// </summary>
    [Range(0, 1f)] public float pump_volum = 0.3f;

    /// <summary>
    /// �ϹϵĴ��״̬
    /// </summary>
    public bool pumpkin_dead = false;

    /// <summary>
    /// �Ϲϵ�������Ч
    /// </summary>
    public AudioClip[] pumpkin_dead_say;

    /// <summary>
    /// �������
    /// </summary>
    private GameObject player;

    /// <summary>
    /// ����ҵľ���
    /// </summary>
    private float distance = 0;

    /// <summary>
    /// �ϹϷ�������
    /// </summary>
    private void Voice()
    {
        if (Voice_CD > Random.Range(7, 12))
        {
            Voice_CD = 0;
            Pumpkin_Voice.PlayOneShot(Pump_Say[Random.Range(0, Pump_Say.Length)], pump_volum);
        }
    }

    /// <summary>
    /// �Ϲ�׷�����
    /// </summary>
    private void FindPlayer()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        //�Ƿ�����ҵľ���С��10m
        if (distance < 10)
        {
            //�ǣ�������Ч
            Voice();
            //�Ƿ�����Ҿ���С��2m
            if (distance < pump_cooleye)
            {
                //�ǣ��������
                transform.LookAt(player.transform);
                //��10m/s���ٶȽӽ����
                transform.Translate(Vector3.forward * pumpkin_speed * Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// �Ϲϻ�ɱ���
    /// </summary>
    private void GameOver()
    {
        Time.timeScale = 0;
        player.GetComponent<RestartGame>().Enemy_name = "Pumpkin";
    }

    /// <summary>
    /// �Ϲ�����ʱ��������
    /// </summary>
    private void Enemy_die_voice()
    {
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(pumpkin_dead_say[Random.Range(0, pumpkin_dead_say.Length)], maincamera.transform.position, 0.5f);
    }

    /// <summary>
    /// �Ϲ�������Ӧ����
    /// </summary>
    private void dead()
    {
        Enemy_die_voice();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        tag = "corpse";
        player.GetComponent<PlayerControl>().enemy_num--;
        GetComponent<PumpkinControl>().enabled = false;
    }

    private void Start()
    {
        //��ȡAudioSource���
        Pumpkin_Voice = GetComponent<AudioSource>();
        //��ʼ��һ����Χ�ڵ����ֵ��Voice_CD����ֹ��һ����������ʱ����ȫ��һ�𷢳���Ч
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
#if DEBUG_MODE
        if (!Pumpkin_Voice)
        {
            Debug.LogError("δ��ȡ��AudioSourec���");
            return;
        }
        if (!player)
        {
            Debug.LogError("δ��ȡ���������");
            return;
        }
        if (!PumpkinControlDeBug())
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
        if (pumpkin_dead) dead();
    }

    private void FixedUpdate()
    {
        FindPlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!pumpkin_dead)
        {
            if (collision.gameObject.name == "Knight") GameOver();
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// PumpkinControl��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool PumpkinControlDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(Pumpkin_Voice, "�Ϲ���Ƶ������δ��ֵ");
        checkNull.Check(Pump_Say, "�Ϲ�������Чδ��ֵ");
        checkNull.Check(pumpkin_dead_say, "�Ϲ�������Чδ��ֵ");
        checkNull.Check(player, "�������δ��ֵ");
        return checkNull.State;
    }
#endif
}