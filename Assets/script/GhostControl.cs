//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// ������ƽű�
/// </summary>
public class GhostControl : MonoBehaviour
{
    /// <summary>
    /// �����AudioSource���
    /// </summary>
    private AudioSource Ghost_Voice;

    /// <summary>
    /// �����������Ч
    /// </summary>
    public AudioClip[] Ghost_Say;

    /// <summary>
    /// ������Ч���ŵ�ʱ����
    /// </summary>
    private float Voice_CD;

    /// <summary>
    /// ������ٶȣ�Ĭ��Ϊ2
    /// </summary>
    public float ghost_speed = 2f;

    /// <summary>
    /// ����ĸ�֪��Χ��Ĭ��Ϊ20
    /// </summary>
    public float ghost_cooleye = 20f;

    /// <summary>
    /// ����������������Ĭ��Ϊ1��
    /// </summary>
    [Range(0, 1f)] public float ghost_volum = 1f;

    /// <summary>
    /// ����Ĵ��״̬
    /// </summary>
    public bool ghost_dead = false;

    /// <summary>
    /// �����������Ч
    /// </summary>
    public AudioClip[] ghost_dead_say;

    /// <summary>
    /// ����ҵľ���
    /// </summary>
    private float distance = 0;

    /// <summary>
    /// �������
    /// </summary>
    private GameObject player;

    /// <summary>
    /// ʹ���鷢������
    /// </summary>
    private void Voice()
    {
        if (Voice_CD > Random.Range(7, 12))
        {
            Voice_CD = 0;
            Ghost_Voice.PlayOneShot(Ghost_Say[Random.Range(0, Ghost_Say.Length)], ghost_volum);
        }
    }

    /// <summary>
    /// ʹ����׷�����
    /// </summary>
    private void FindPlayer()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < ghost_cooleye)
        {
            Voice();
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * ghost_speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// ɱ�����
    /// </summary>
    private void GameOver()
    {
        if (distance < 1)
        {
            //��ghostû����ײ��⣬������Ҫ�ֶ������������״̬
            player.GetComponent<IfDie>().IsDie = true;
            //����ɱ����ҵĵ�������
            Time.timeScale = 0;
            player.GetComponent<RestartGame>().Enemy_name = "Ghost";
        }
    }

    /// <summary>
    /// ������������
    /// </summary>
    private void Enemy_die_voice()
    {
        GameObject maincamera = GameObject.Find("Main Camera");
#if DEBUG_MODE
        if (!maincamera)
        {
            Debug.LogError("δ��ȡ���������");
            return;
        }
#endif
        AudioSource.PlayClipAtPoint(ghost_dead_say[Random.Range(0, ghost_dead_say.Length)], maincamera.transform.position, 0.5f);
    }

    /// <summary>
    /// ����������Ӧ����
    /// </summary>
    private void Dead()
    {
        Enemy_die_voice();
        player.GetComponent<PlayerControl>().enemy_num--;
        Destroy(gameObject);
    }

    private void Start()
    {
        //��ȡAudioSource���
        Ghost_Voice = GetComponent<AudioSource>();
        //��ʼ��һ����Χ�ڵ����ֵ��Voice_CD����ֹ��һ����������ʱ����ȫ��һ�𷢳���Ч
        Voice_CD = Random.Range(5, 15);
        player = GameObject.FindWithTag("Player");
#if DEBUG_MODE
        if (!Ghost_Voice)
        {
            Debug.LogError("δ��ȡ��AudioSource���");
            return;
        }
        if (!player)
        {
            Debug.LogError("δ��ȡ���������");
            return;
        }
        if (!GhostControlDeBug())
        {
            Debug.LogError("��ʼ��ʧ��");
            return;
        }
#endif
    }

    private void Update()
    {
        if (ghost_dead) Dead();
        Voice_CD += Time.deltaTime;
        FindPlayer();
        GameOver();
    }

#if DEBUG_MODE
    /// <summary>
    /// GhostControl��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool GhostControlDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(Ghost_Voice, "Ghost_Voice/������Ƶ������δ��ʼ��");
        checkNull.Check(Ghost_Say, "Ghost_Say/����������Чδ��ʼ��");
        checkNull.Check(ghost_dead_say, "ghost_dead_say/����������Чδ��ʼ��");
        checkNull.Check(player, "player/�������δ��ʼ��");
        return checkNull.State;
    }
#endif
}