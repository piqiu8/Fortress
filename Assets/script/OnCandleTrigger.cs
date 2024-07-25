//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// ��ȼ����
/// </summary>
public class OnCandleTrigger : MonoBehaviour
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
    /// ��������Ч
    /// </summary>
    public AudioClip OpenLight;

    private void Start()
    {
        player = GameObject.Find("MusicTrigger").GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!player)
        {
            Debug.LogError("δ��ȡ��AudioSource���");
            return;
        }
        if (!OnCandleTriggerDeBug())
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
            if (!candle.GetComponent<Light>().enabled)
            {
                player.PlayOneShot(OpenLight);
                candle.GetComponent<Light>().enabled = true;
            }
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// OnCandleTrigger��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool OnCandleTriggerDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(candle, "candle/��������δ��ֵ");
        checkNull.Check(player, "player/��Ƶ������δ��ֵ");
        checkNull.Check(OpenLight, "OpenLight/��ȼ������Чδ��ֵ");
        return checkNull.State;
    }
#endif
}