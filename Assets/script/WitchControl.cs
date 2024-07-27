//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// Ů�׿��ƽű�
/// </summary>
public class WitchControl : MonoBehaviour
{
    /// <summary>
    /// Ů���ٻ���ʬ����
    /// </summary>
    public GameObject Witch_Zombie;

    /// <summary>
    /// �ٻ���ʬCD
    /// </summary>
    private float CD = 6;

    /// <summary>
    /// Ů�׶�Ӧ��Ƶ������
    /// </summary>
    private AudioSource Witch_Voice;

    /// <summary>
    /// Ů��������Ч
    /// </summary>
    public AudioClip[] Witch_Say;

    /// <summary>
    /// ������Ч���
    /// </summary>
    private float Voice_CD;

    /// <summary>
    /// ��Ӧ�ٶȣ�Ĭ��Ϊ1
    /// </summary>
    public float witch_speed = 1f;

    /// <summary>
    /// ��Ӧ��֪��Χ
    /// </summary>
    public float witch_cooleye = 10f;

    /// <summary>
    /// ��Ӧ������С
    /// </summary>
    [Range(0, 1f)] public float witch_volum = 0.4f;

    /// <summary>
    /// ��Ӧ���״̬
    /// </summary>
    public bool witch_dead = false;

    /// <summary>
    /// ��Ӧ������Ч
    /// </summary>
    public AudioClip[] witch_dead_say;

    /// <summary>
    /// �������
    /// </summary>
    private GameObject player;

    /// <summary>
    /// ����Ҿ���
    /// </summary>
    private float distance = 0;

    /// <summary>
    /// ��������
    /// </summary>
    private void Voice()
    {
        if (Voice_CD > Random.Range(7, 12))
        {
            Voice_CD = 0;
            Witch_Voice.PlayOneShot(Witch_Say[Random.Range(0, Witch_Say.Length)], witch_volum);
        }
    }

    /// <summary>
    /// Զ�����
    /// </summary>
    private void AwayPlayer()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < witch_cooleye)
        {
            //��������Witch��10m��Χ�ڣ��ڲ�����Ч
            Voice();
            transform.LookAt(player.transform);
            //Զ�����
            transform.Translate(Vector3.back * witch_speed * Time.deltaTime);
            if (CD > 6)
            {//��CD����6
                CD = 0;
                //������λ������һ��witch_zombie
                Instantiate(Witch_Zombie, transform.position, transform.rotation);
                player.GetComponent<PlayerControl>().enemy_num++;
            }
        }
    }

    /// <summary>
    /// ������������
    /// </summary>
    private void Enemy_die_voice()
    {
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(witch_dead_say[Random.Range(0, witch_dead_say.Length)], maincamera.transform.position, 0.5f);
    }

    /// <summary>
    /// ����ִ�ж�Ӧ����
    /// </summary>
    private void dead()
    {
        Enemy_die_voice();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        tag = "corpse";
        player.GetComponent<PlayerControl>().enemy_num--;
        GetComponent<WitchControl>().enabled = false;
    }

    private void Start()
    {
        //��ȡAudioSource���
        Witch_Voice = GetComponent<AudioSource>();
        //��ʼ��һ����Χ�ڵ����ֵ��Voice_CD����ֹ��һ����������ʱ����ȫ��һ�𷢳���Ч
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
#if DEBUG_MODE
        if (!Witch_Voice)
        {
            Debug.LogError("δ��ȡ��AudioSource���");
            return;
        }
        if (!player)
        {
            Debug.LogError("δ��ȡ���������");
            return;
        }
        if (!WitchControlDeBug())
        {
            Debug.LogError("��ʼ��ʧ��");
            return;
        }
#endif
    }

    private void Update()
    {
        CD += Time.deltaTime;
        //��������Чʱ�������м�ʱ
        Voice_CD += Time.deltaTime;
        if (witch_dead) dead();
    }

    private void FixedUpdate()
    {
        AwayPlayer();
    }

#if DEBUG_MODE
    /// <summary>
    /// WitchControl��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool WitchControlDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(Witch_Zombie, "Witch_Zombie/Ů���ٻ���ʬ����δ��ֵ");
        checkNull.Check(Witch_Voice, "Witch_Voice/��Ƶ������δ��ֵ");
        checkNull.Check(Witch_Say, "Witch_say/Ů������δ��ֵ");
        checkNull.Check(witch_dead_say, "witch_dead_say/Ů��������Чδ��ֵ");
        checkNull.Check(player, "player/�������δ��ֵ");
        return checkNull.State;
    }
#endif
}