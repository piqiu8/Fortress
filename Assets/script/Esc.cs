//#define DEBUG_MODE

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ʵ����Ϸ��ͣ
/// </summary>
public class Esc : MonoBehaviour
{
    /// <summary>
    /// ��ͣʱ���ֵ�UI
    /// </summary>
    public GameObject ESC;

    /// <summary>
    /// ��Ϸ����״̬������ȷ����Ϸ�Ƿ�������
    /// </summary>
    private bool IsGameRunning = true;

    /// <summary>
    /// ���ִ�����
    /// </summary>
    private GameObject MusicTrigger;

    /// <summary>
    /// ��Ƶ������
    /// </summary>
    private AudioSource player;

    /// <summary>
    /// �������ֲ���״̬������ȷ�ϱ��������Ƿ���
    /// </summary>
    private bool IfBackgroundMusic;

    /// <summary>
    /// ��Ƶ������2��
    /// </summary>
    private AudioSource player2;

    /// <summary>
    /// ��ͣ���ŵ���Ч
    /// </summary>
    public AudioClip clip_01;

    private void Start()
    {
        //��ȡ���ִ�����
        MusicTrigger = GameObject.Find("MusicTrigger");
        //��ȡ��Ƶ������
        player = MusicTrigger.GetComponent<AudioSource>();
        //��ȡ2����Ƶ������
        player2 = GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!MusicTrigger)
        {
            Debug.LogError("δ��ȡ�����ִ�����");
            return;
        }
        if (!player)
        {
            Debug.LogError("δ��ȡ��AudioSource���");
            return;
        }
        if(!player2)
        {
            Debug.LogError("δ��ȡ��AudioSource���2��");
            return;
        }
        if (!EscDeBug())
        {
            Debug.LogError("��ʼ��ʧ��");
            return;
        }
#endif
    }

    private void Update()
    {
        //��ʱ��ȡ�������ֵĲ���״̬
        IfBackgroundMusic = MusicTrigger.GetComponent<BackgroundMusic>().IfBackgroundMusic;
        //�����¿ո��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //������ͣ������Ч
            player2.PlayOneShot(clip_01);
            //����Ϸ������
            if (IsGameRunning)
            {
                //�������������ڲ��ţ�����ͣ��������
                if (IfBackgroundMusic) player.Pause();
                Time.timeScale = 0;
                //��ESC UI�ļ���״̬����Ϊtrue
                ESC.SetActive(true);
            }
            //����Ϸδ����
            else
            {
                //���������ָֻ�����
                if (IfBackgroundMusic) player.UnPause();
                //����Ϸ��������
                Time.timeScale = 1;
                //��ESC ����״̬����Ϊfalse
                ESC.SetActive(false);
            }
            //����Ϸ����״̬�÷�
            IsGameRunning = !IsGameRunning;
        }
        //��EscΪ����״̬
        if (ESC.activeInHierarchy)
        {
            //�����»س���
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //ͬ������0�ų���
                SceneManager.LoadScene(0);
                Time.timeScale = 1;
            }
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// Esc��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool EscDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(ESC, "ESC/��ͣʱ��ʾ��UIδ��ֵ");
        checkNull.Check(MusicTrigger, "MusicTrigger/���ִ�����δ��ֵ");
        checkNull.Check(player, "player/��Ƶ������δ��ֵ");
        checkNull.Check(player2, "player2/��Ƶ������2��δ��ֵ");
        checkNull.Check(clip_01, "clip_01/��ͣ���ŵ���Чδ��ֵ");
        return checkNull.State;
    }
#endif
}