//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// ��ʬ���ƽű�
/// </summary>
public class ZombieControl : MonoBehaviour
{
    /// <summary>
    /// ��ʬ��Ƶ������
    /// </summary>
    private AudioSource ZombieVoice;

    /// <summary>
    /// ��ʬ������Ч
    /// </summary>
    public AudioClip[] Zombie_say;

    /// <summary>
    /// ��ЧCD
    /// </summary>
    private float Voice_CD;

    /// <summary>
    /// ��ʬ�ٶȣ�Ĭ��Ϊ3
    /// </summary>
    public float zombie_speed = 3f;

    /// <summary>
    /// ��ʬ��֪��Χ��Ĭ��Ϊ10
    /// </summary>
    public float zombie_cooleye = 10f;

    /// <summary>
    /// ��ʬ������С��Ĭ��Ϊ0.3
    /// </summary>
    [Range(0, 1f)] public float zombie_volum = 0.3f;

    /// <summary>
    /// ��ʬ���״̬
    /// </summary>
    public bool zombie_dead = false;

    /// <summary>
    /// ��ʬ��������Ч
    /// </summary>
    public AudioClip[] Zombie_dead_say;

    /// <summary>
    /// �������
    /// </summary>
    private GameObject player;

    /// <summary>
    /// ������Ҿ���
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
            ZombieVoice.PlayOneShot(Zombie_say[Random.Range(0, Zombie_say.Length)], zombie_volum);
        }
    }

    /// <summary>
    /// Ѱ�����
    /// </summary>
    private void FindPlayer()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < zombie_cooleye)
        {
            Voice();
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * zombie_speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// ɱ�����
    /// </summary>
    private void GameOver()
    {
        Time.timeScale = 0;
        player.GetComponent<RestartGame>().Enemy_name = "Zombie";
    }

    /// <summary>
    /// ������������
    /// </summary>
    private void Enemy_die_voice()
    {
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(Zombie_dead_say[Random.Range(0, Zombie_dead_say.Length)], maincamera.transform.position, 0.5f);
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
        GetComponent<ZombieControl>().enabled = false;
    }

    private void Start()
    {
        ZombieVoice = GetComponent<AudioSource>();
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
#if DEBUG_MODE
        if (!ZombieVoice)
        {
            Debug.LogError("δ��ȡ��AudioSource���");
            return;
        }
        if (!player)
        {
            Debug.LogError("δ��ȡ���������");
            return;
        }
        if (!ZombieControlDeBug())
        {
            Debug.LogError("��ʼ��ʧ��");
            return;
        }
#endif
    }

    private void Update()
    {
        Voice_CD += Time.deltaTime;
        if (zombie_dead) dead();
    }

    private void FixedUpdate()
    {
        FindPlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //������û��
        if (!zombie_dead)
        {
            if (collision.gameObject.name == "Knight") GameOver();
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// ZombieControl��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool ZombieControlDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(ZombieVoice, "ZombieVoice/��ʬ��Ƶ������δ��ֵ");
        checkNull.Check(Zombie_say, "Zombie_say/��ʬ������Чδ��ֵ");
        checkNull.Check(Zombie_dead_say, "Zombie_dead_say/��ʬ������Чδ��ֵ");
        checkNull.Check(player, "player/�������δ��ֵ");
        return checkNull.State;
    }
#endif
}