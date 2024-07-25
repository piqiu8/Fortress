//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// 点燃蜡烛
/// </summary>
public class OnCandleTrigger : MonoBehaviour
{
    /// <summary>
    /// 蜡烛物体
    /// </summary>
    public GameObject candle;

    /// <summary>
    /// 音频播放器
    /// </summary>
    private AudioSource player;

    /// <summary>
    /// 点蜡烛音效
    /// </summary>
    public AudioClip OpenLight;

    private void Start()
    {
        player = GameObject.Find("MusicTrigger").GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!player)
        {
            Debug.LogError("未获取到AudioSource组件");
            return;
        }
        if (!OnCandleTriggerDeBug())
        {
            Debug.LogError("初始化失败");
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
    /// OnCandleTrigger初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool OnCandleTriggerDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(candle, "candle/蜡烛物体未赋值");
        checkNull.Check(player, "player/音频播放器未赋值");
        checkNull.Check(OpenLight, "OpenLight/点燃蜡烛音效未赋值");
        return checkNull.State;
    }
#endif
}