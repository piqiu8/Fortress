//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// 点亮火炬
/// </summary>
public class FireTrigger : MonoBehaviour
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
    /// 产生火焰时的音效
    /// </summary>
    public AudioClip Fire;

    private void Start()
    {
        //获取火炬的音频播放器
        player = GameObject.Find("torch_7").GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!player)
        {
            Debug.LogError("未获取到AudioSource组件");
            return;
        }
        if (!FireTriggerDeBug())
        {
            Debug.LogError("初始化失败");
            return;
        }
#endif
    }

    /// <summary>
    /// 使用触发器点亮火炬
    /// </summary>
    /// <param name="other">碰撞物体信息</param>
    private void OnTriggerEnter(Collider other)
    {
        //若触发对象为玩家
        if (other.name == "KnightBody")
        {
            //且左右火焰都未激活
            if (!Torch1.activeInHierarchy && !Torch2.activeInHierarchy)
            {
                //播放火焰产生音效
                player.PlayOneShot(Fire, 0.1f);
                //将两个火焰激活
                Torch1.SetActive(true);
                Torch2.SetActive(true);
            }
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// FireTrigger初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool FireTriggerDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(Torch1, "Torch1/左侧火焰效果未赋值");
        checkNull.Check(Torch2, "Torch2/右侧火焰效果未赋值");
        checkNull.Check(player, "player/音频播放器未赋值");
        checkNull.Check(Fire, "Fire/产生火焰时的音效未赋值");
        return checkNull.State;
    }
#endif
}