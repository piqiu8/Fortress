//#define DEBUG_MODE

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 实现图片渐隐
/// </summary>
public class FadeImage2 : MonoBehaviour
{
    /// <summary>
    /// 需要渐隐的image
    /// </summary>
    private Image image;

    /// <summary>
    /// 渐变颜色
    /// </summary>
    public Gradient gradient;

    /// <summary>
    /// 渐变持续时间，默认10s
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

    private void Start()
    {
        //获取Image组件
        image = GetComponent<Image>();
#if DEBUG_MODE
        if (!image)
        {
            Debug.LogError("未获取到image组件");
            return;
        }
        if (!FadeImage2DeBug())
        {
            Debug.LogError("初始化失败");
            return;
        }
#endif
        EndColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
        //将初始图片透明度和最终图片透明度赋给渐变过程，0代表是渐变最开始的颜色，1代表完成渐变后的颜色
        gradient.colorKeys = new GradientColorKey[] { new GradientColorKey(image.color, 0f), new GradientColorKey(EndColor, 1f) };
    }

    private void Update()
    {
        // 计算当前渐变进度
        float progress = Mathf.Clamp01(timer / duration);
        // 获取当前渐变颜色
        Color color = gradient.Evaluate(progress);
        color.a = 1f - progress;
        // 将渐变颜色应用到image对象中
        image.color = color;
        // 更新计时器
        timer += Time.unscaledDeltaTime;
        // 如果渐变已经完成，停止脚本
        if (progress == 1f) enabled = false;
    }

#if DEBUG_MODE
    /// <summary>
    /// FadeImage2初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool FadeImage2DeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(image, "image/需要渐隐的image未赋值");
        return checkNull.State;
    }
#endif
}