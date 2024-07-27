//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// ʵ�ֱ������ֲ���
/// </summary>
public class BackgroundMusic : MonoBehaviour
{
    /// <summary>
    /// ��Ӧ��������
    /// </summary>
    public AudioClip Music;

    /// <summary>
    /// ��Ӧ�������ֲ�����
    /// </summary>
    private AudioSource BackGroundMusicPlayer;

    /// <summary>
    /// �������ֲ���״̬��Ĭ��Ϊfalse����Inspector����
    /// </summary>
    [HideInInspector] public bool IfBackgroundMusic = false;

    private void Start()
    {
        //��ȡ���������
        BackGroundMusicPlayer = GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!BackGroundMusicPlayer)
        {
            Debug.LogError("δ��ȡ��AudioSource���");
            return;
        }
        if (!BackGroundMusicDeBug())
        {
            Debug.LogError("��ʼ��ʧ��");
            return;
        }
#endif
        //���ò�����ƵΪMusic
        BackGroundMusicPlayer.clip = Music;
        //����ѭ������
        BackGroundMusicPlayer.loop = true;
        //��������Ϊ0.2��
        BackGroundMusicPlayer.volume = 0.2f;
    }

    /// <summary>
    /// ͨ�����������ű�������
    /// </summary>
    /// <param name="other">��ײ������Ϣ</param>
    private void OnTriggerEnter(Collider other)
    {
        //����ײ��������ΪKnightBody
        if (other.name == "KnightBody")
        {
            //����������Ϊ�ر�״̬
            if (!BackGroundMusicPlayer.isPlaying)
            {
                //����������״̬��Ϊ���������ű�������
                IfBackgroundMusic = true;
                BackGroundMusicPlayer.Play();
#if DEBUG_MODE
                Debug.Log("��Ϸ�������ֲ���״̬Ϊ"+IfBackgroundMusic);
#endif
            }
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// BackgroundMusic��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool BackGroundMusicDeBug()
    {
        CheckNull checkNull = gameObject.AddComponent<CheckNull>();
        checkNull.Check(Music, "Music/��������δ��ʼ��");
        checkNull.Check(BackGroundMusicPlayer, "BackGroundMusicPlayer/�������ֲ�����δ��ʼ��");
        return checkNull.State;
    }
#endif
}