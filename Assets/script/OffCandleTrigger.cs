//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// 熄灭玩家蜡烛并关闭背景音乐
/// </summary>
public class OffCandleTrigger : MonoBehaviour
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
    /// 背景音乐触发器
    /// </summary>
    private GameObject MusicTrigger;

    private void Start()
    {
        MusicTrigger = GameObject.Find("MusicTrigger");
        player = MusicTrigger.GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!MusicTrigger)
        {
            Debug.LogError("未获取到背景音乐触发器");
            return;
        }
        if (!player)
        {
            Debug.LogError("未获取到AudioSource组件");
            return;
        }
        if (!OffCandleTriggerDeBug())
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
            MusicTrigger.GetComponent<BackgroundMusic>().IfBackgroundMusic = false;
            player.Stop();
            candle.GetComponent<Light>().enabled = false;
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// OffCandleTrigger初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool OffCandleTriggerDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(candle, "candle/蜡烛物体未赋值");
        checkNull.Check(player, "player/音频播放器未赋值");
        checkNull.Check(MusicTrigger, "MusicTrigger/背景音乐触发器未赋值");
        return checkNull.State;
    }
#endif
}