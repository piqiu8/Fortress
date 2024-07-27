//#define DEBUG_MODE

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���¿�ʼ��Ϸ
/// </summary>
public class RestartGame : MonoBehaviour
{
    /// <summary>
    /// ��������UI
    /// </summary>
    public GameObject relive;

    /// <summary>
    /// ��Ҵ��״̬
    /// </summary>
    private bool IsDie;

    /// <summary>
    /// ��Ƶ������
    /// </summary>
    private AudioSource player;

    /// <summary>
    /// ��Ϸ������Ч
    /// </summary>
    public AudioClip GameOver;

    /// <summary>
    /// �������ֲ�����
    /// </summary>
    private AudioSource BackgroundMusic;

    /// <summary>
    /// �ж�ֵ
    /// </summary>
    private int num = 0;

    /// <summary>
    /// ��ʱ��
    /// </summary>
    private float time = 0, time2 = 0;

    /// <summary>
    /// ��ʱʱ��
    /// </summary>
    private float delayed_time = 10f;

    /// <summary>
    /// ɱ����ҵĵ�������
    /// </summary>
    [HideInInspector] public string Enemy_name;

    /// <summary>
    /// ����Ϸ������Ч��ʼ���ź��ʱ�ļ�ʱ���������õ���ɱ����Һ󲥷���Ч����Ϸ������Ч�м䲥��
    /// </summary>
    private float Enemy_time;

    /// <summary>
    /// ��ʱ�ж�ֵ
    /// </summary>
    private bool if_enemy_time = false;

    /// <summary>
    /// ����ɱ����Һ󲥷���Ч
    /// </summary>
    public AudioClip[] enemy_win_voice;

    private void Start()
    {
        player = GetComponent<AudioSource>();
        BackgroundMusic = GameObject.Find("MusicTrigger").GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!player)
        {
            Debug.LogError("δ��ȡ��AudioSource");
            return;
        }
        if (!BackgroundMusic)
        {
            Debug.LogError("δ��ȡ���������ֲ�����");
            return;
        }
#endif
    }

    private void Update()
    {
        IsDie = GetComponent<IfDie>().IsDie;
        //�ڲ�����Ϸ������Ч��ʼ��ʱ
        if (if_enemy_time)
        {
            Enemy_time += Time.fixedUnscaledDeltaTime;
            //�Ƿ�ʱ�䳬��5s
            if (Enemy_time > 6)
            {
                switch (Enemy_name)
                {
                    //����zombieɱ����Һ����Ч
                    case "Zombie": player.PlayOneShot(enemy_win_voice[0], 1f); break;
                    //����witchɱ����Һ����Ч
                    case "Witch": player.PlayOneShot(enemy_win_voice[1], 1f); break;
                    //����ghostɱ����Һ����Ч
                    case "Ghost": player.PlayOneShot(enemy_win_voice[2], 1f); break;
                    //����pumpkinɱ����Һ����Ч
                    case "Pumpkin": player.PlayOneShot(enemy_win_voice[3], 1f); break;
                    //����devilɱ����Һ����Ч
                    case "Devil": player.PlayOneShot(enemy_win_voice[4], 1f); break;
                    default: break;
                }
                //ֹͣ��ʱ�������ظ�����
                if_enemy_time = false;
            }
        }
        //�������
        if (IsDie)
        {
            //��ʼ��ʱ
            time += Time.fixedUnscaledDeltaTime;
            time2 += Time.fixedUnscaledDeltaTime;
            //ֹͣ��������
            BackgroundMusic.Stop();
            //1s�󲥷���Ϸ������Ч
            if (time > 1)
            {
                //ʱ�����㣬Ϊ�´����¼�ʱ׼��
                time = 0;
                if (num == 0)
                {
                    if_enemy_time = true;
                    player.PlayOneShot(GameOver, 0.5f);
                    num++;
                }
            }
            //�ر�Esc���
            GetComponent<Esc>().enabled = false;
            //����relive UI
            relive.SetActive(true);
            if (time2 > delayed_time)
            {
                //����������¿�ʼ
                if (Input.anyKeyDown)
                {
                    time2 = 0;
                    SceneManager.LoadScene(1);
                    Time.timeScale = 1;
                }
            }
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// RestartGame��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool RestartGameDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(relive, "relive/����UIδ��ֵ");
        checkNull.Check(player, "player/��Ƶ������δ��ֵ");
        checkNull.Check(GameOver, "GameOver/��Ϸ������Чδ��ֵ");
        checkNull.Check(BackgroundMusic, "BackgroundMusic/�������ֲ�����δ��ֵ");
        checkNull.Check(enemy_win_voice, "enemy_win_voice/����ɱ����Һ󲥷���Чδ��ֵ");
        return checkNull.State;
    }
#endif
}