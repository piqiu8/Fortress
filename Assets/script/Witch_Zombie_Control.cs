//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// Ů���ٻ���ʬ���ƽű�
/// </summary>
public class Witch_Zombie_Control : MonoBehaviour
{
    /// <summary>
    /// Ů���ٻ���ʬ��Ƶ������
    /// </summary>
    private AudioSource Witch_Zombie_Voice;

    /// <summary>
    /// Ů���ٻ���ʬ������Ч
    /// </summary>
    public AudioClip[] Witch_Zombie_say;

    /// <summary>
    /// ��ЧCD
    /// </summary>
    private float Voice_CD;

    /// <summary>
    /// ��Ӧ�ٶȣ�Ĭ��Ϊ3
    /// </summary>
    public float Witch_Zombie_speed = 3f;

    /// <summary>
    /// ��Ӧ��֪��Χ��Ĭ��Ϊ10
    /// </summary>
    public float Witch_zombie_cooleye = 10f;

    /// <summary>
    /// ��Ӧ������С
    /// </summary>
    [Range(0, 1f)] public float Witch_Zombie_volum = 0.3f;

    /// <summary>
    /// ��Ӧ���״̬
    /// </summary>
    public bool witch_zombie_dead = false;

    /// <summary>
    /// ��Ӧ������Ч
    /// </summary>
    public AudioClip[] witch_zombie_dead_say;

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
            Witch_Zombie_Voice.PlayOneShot(Witch_Zombie_say[Random.Range(0, Witch_Zombie_say.Length)], Witch_Zombie_volum);
        }
    }

    /// <summary>
    /// ׷�����
    /// </summary>
    private void FindPlayer()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < Witch_zombie_cooleye)
        {
            Voice();
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * Witch_Zombie_speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// ɱ�����
    /// </summary>
    private void GameOver()
    {
        Time.timeScale = 0;
        player.GetComponent<RestartGame>().Enemy_name = "Witch";
    }

    /// <summary>
    /// ������������
    /// </summary>
    private void Enemy_die_voice()
    {
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(witch_zombie_dead_say[Random.Range(0, witch_zombie_dead_say.Length)], maincamera.transform.position, 0.5f);
    }

    /// <summary>
    /// ������Ӧ����
    /// </summary>
    private void dead()
    {
        Enemy_die_voice();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        tag = "corpse";
        player.GetComponent<PlayerControl>().enemy_num--;
        GetComponent<Witch_Zombie_Control>().enabled = false;
    }

    private void Start()
    {
        Witch_Zombie_Voice = GetComponent<AudioSource>();
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
#if DEBUG_MODE
        if (!Witch_Zombie_Voice)
        {
            Debug.LogError("δ��ȡ��AudioSource���");
            return;
        }
        if (!player)
        {
            Debug.LogError("δ��ȡ���������");
            return;
        }
        if (!WitchZombieDeBug())
        {
            Debug.LogError("��ʼ��ʧ��");
            return;
        }
#endif
    }

    private void Update()
    {
        Voice_CD += Time.deltaTime;
        if (witch_zombie_dead) dead();
    }

    private void FixedUpdate()
    {
        FindPlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!witch_zombie_dead)
        {
            if (collision.gameObject.name == "Knight") GameOver();
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// WitchZombie��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool WitchZombieDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(Witch_Zombie_Voice, "Witch_Zombie_Voice/��Ƶ������δ��ֵ");
        checkNull.Check(Witch_Zombie_say, "Witch_Zombie_say/Ů���ٻ���ʬ����δ��ֵ");
        checkNull.Check(witch_zombie_dead_say, "witch_zombie_dead_say/Ů���ٻ���ʬ������Чδ��ֵ");
        checkNull.Check(player, "player/�������δ��ֵ");
        return checkNull.State;
    }
#endif
}