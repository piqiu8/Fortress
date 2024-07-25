//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// 熄灭火炬上的火焰
/// </summary>
public class OffFireTrigger : MonoBehaviour
{
    /// <summary>
    /// 左侧火焰效果
    /// </summary>
    public GameObject Torch1;

    /// <summary>
    /// 右侧火焰效果
    /// </summary>
    public GameObject Torch2;

    /// <summary>
    /// 音频播放器
    /// </summary>
    private AudioSource player;

    /// <summary>
    /// 火焰熄灭音效
    /// </summary>
    public AudioClip OffFire;

    private void Start()
    {
        player = GameObject.Find("torch_7").GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!player)
        {
            Debug.LogError("未获取到AudioSource组件");
            return;
        }
        if (!OffFireTriggerDeBug())
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
            if (Torch1.activeInHierarchy && Torch2.activeInHierarchy)
            {
                player.PlayOneShot(OffFire, 0.1f);
                Torch1.SetActive(false);
                Torch2.SetActive(false);
            }
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// OffFireTrigger初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool OffFireTriggerDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(Torch1, "Torch1/左侧火焰效果未赋值");
        checkNull.Check(Torch2, "Torch2/右侧火焰效果未赋值");
        checkNull.Check(player, "player/音频播放器未赋值");
        checkNull.Check(OffFire, "OffFIre/火焰熄灭效果未赋值");
        return checkNull.State;
    }
#endif
}