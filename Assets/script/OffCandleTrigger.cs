//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// Ϩ��������򲢹رձ�������
/// </summary>
public class OffCandleTrigger : MonoBehaviour
{
    /// <summary>
    /// ��������
    /// </summary>
    public GameObject candle;

    /// <summary>
    /// ��Ƶ������
    /// </summary>
    private AudioSource player;

    /// <summary>
    /// �������ִ�����
    /// </summary>
    private GameObject MusicTrigger;

    private void Start()
    {
        MusicTrigger = GameObject.Find("MusicTrigger");
        player = MusicTrigger.GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!MusicTrigger)
        {
            Debug.LogError("δ��ȡ���������ִ�����");
            return;
        }
        if (!player)
        {
            Debug.LogError("δ��ȡ��AudioSource���");
            return;
        }
        if (!OffCandleTriggerDeBug())
        {
            Debug.LogError("��ʼ��ʧ��");
            return;
        }
#endif
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "KnightBody")
        {
            MusicTrigger.GetComponent<BackgroundMusic>().IfBackgroundMusic = false;
            player.Stop();
            candle.GetComponent<Light>().enabled = false;
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// OffCandleTrigger��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool OffCandleTriggerDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(candle, "candle/��������δ��ֵ");
        checkNull.Check(player, "player/��Ƶ������δ��ֵ");
        checkNull.Check(MusicTrigger, "MusicTrigger/�������ִ�����δ��ֵ");
        return checkNull.State;
    }
#endif
}