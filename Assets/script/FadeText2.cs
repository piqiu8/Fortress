//#define DEBUG_MODE

using TMPro;
using UnityEngine;

/// <summary>
/// 用于玩家胜利画面的实现文本渐隐
/// </summary>
public class FadeText2 : MonoBehaviour
{
    /// <summary>
    /// 需要渐变的文本
    /// </summary>
    private TMP_Text Text;

    /// <summary>
    /// 渐变颜色
    /// </summary>
    public Gradient gradient;

    /// <summary>
    /// 渐变持续时间，默认为10s
    /// </summary>
    public float duration = 10f;

    /// <summary>
    /// 渐变用的计时器
    /// </summary>
    private float timer = 0f;

    /// <summary>
    /// 最终渐变颜色
    /// </summary>
    private Color EndColor;

    /// <summary>
    /// 延迟显示状态值，用于控制是否延迟渐变效果
    /// </summary>
    public bool is_delayed = false;

    /// <summary>
    /// 延迟时间
    /// </summary>
    private float delayed_time = 5f;

    /// <summary>
    /// 延时用的计时器
    /// </summary>
    private float time = 0;

    private void Start()
    {
        //获取TMP组件
        Text = GetComponent<TMP_Text>();
#if DEBUG_MODE
        if(!Text)
        {
            Debug.LogError("未获取到TMP_Text");
            return;
        }
#endif
        //创建最终渐变颜色
        EndColor = new Color(255 / 255f, 143 / 255f, 0 / 255f, 255 / 255f);
        //将初始文本颜色和最终文本颜色赋给渐变过程，0代表是渐变最开始的颜色，1代表完成渐变后的颜色
        gradient.colorKeys = new GradientColorKey[] { new GradientColorKey(Text.color, 0f), new GradientColorKey(EndColor, 1f) };
    }

    private void fade()
    {
        // 计算当前渐变进度
        float progress = Mathf.Clamp01(timer / duration);
        // 获取当前渐变颜色
        Color color = gradient.Evaluate(progress);
        // 将渐变颜色应用到UI Text对象中
        Text.color = new Color(color.r, color.g, color.b, progress);
        // 更新计时器
        timer += Time.fixedUnscaledDeltaTime;
        // 如果渐变已经完成，停止脚本
        if (progress == 1f) enabled = false;
    }

    private void Update()
    {
        time += Time.fixedUnscaledDeltaTime;
        if (is_delayed)
        {
            if (time > delayed_time) fade();
        }
        else fade();
    }
}