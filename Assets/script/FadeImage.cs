//#define DEBUG_MODE

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 实现图片渐现
/// </summary>
public class FadeImage : MonoBehaviour
{
    /// <summary>
    /// 需要渐现的image
    /// </summary>
    private Image image;

    /// <summary>
    /// 渐变颜色
    /// </summary>
    public Gradient gradient;

    /// <summary>
    /// 渐变持续时间，默认为10s
    /// </summary>
    public float duration = 10f;

    /// <summary>
    /// 定时器
    /// </summary>
    private float timer = 0f;

    /// <summary>
    /// 渐变最终颜色
    /// </summary>
    private Color EndColor;

    /// <summary>
    /// 游戏胜利音效
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// 音频播放器
    /// </summary>
    private AudioSource player;

    private void Start()
    {
        //获取Image组件
        image = GetComponent<Image>();
        //获取音频播放器
        player = GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!image)
        {
            Debug.LogError("未获取到image组件");
            return;
        }
        if (!player)
        {
            Debug.LogError("未获取到AudioSource组件");
            return;
        }
        if (!FadeImageDeBug())
        {
            Debug.LogError("初始化失败");
            return;
        }
#endif
        //播放游戏胜利音效
        player.PlayOneShot(audioClip);
        //创建最终渐变颜色
        EndColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        //将初始图片透明度和最终图片透明度赋给渐变过程，0代表是渐变最开始的颜色，1代表完成渐变后的颜色
        gradient.colorKeys = new GradientColorKey[] { new GradientColorKey(image.color, 0f), new GradientColorKey(EndColor, 1f) };
    }

    private void Update()
    {
        // 计算当前渐变进度
        float progress = Mathf.Clamp01(timer / duration);
        // 获取当前渐变颜色
        Color color = gradient.Evaluate(progress);
        // 将渐变颜色应用到image中
        image.color = new Color(color.r, color.g, color.b, progress);
        // 更新计时器
        timer += Time.fixedUnscaledDeltaTime;
        // 如果渐变已经完成，停止脚本
        if (progress == 1f) enabled = false;
    }

#if DEBUG_MODE
    /// <summary>
    /// FadeImage初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool FadeImageDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent <CheckNull>();
        checkNull.Check(image, "image/需要渐现的图片未赋值");
        checkNull.Check(audioClip, "游戏胜利音效未赋值");
        checkNull.Check(player, "player/音频播放器未赋值");
        return checkNull.State;
    }
#endif
}