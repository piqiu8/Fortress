//#define DEBUG_MODE

using TMPro;
using UnityEngine;

/// <summary>
/// 用于玩家死亡画面的实现文本渐现
/// </summary>
public class FadeText : MonoBehaviour
{
    /// <summary>
    /// 需要渐变的文本
    /// </summary>
    // 定义需要渐变的UI Text对象
    private TMP_Text Text;

    /// <summary>
    /// 渐变颜色
    /// </summary>
    public Gradient gradient;

    /// <summary>
    /// 渐变持续时间，默认10s
    /// </summary>
    public float duration = 10f;

    /// <summary>
    /// 渐变用的计时器
    /// </summary>
    private float timer = 0f;

    /// <summary>
    /// 渐变最终颜色
    /// </summary>
    private Color EndColor;

    /// <summary>
    /// 杀死玩家的敌人名称
    /// </summary>
    private string Enemy_name;

    /// <summary>
    /// 延迟显示状态值，用于控制是否延迟渐变效果
    /// </summary>
    public bool is_delayed = false;

    /// <summary>
    /// 延迟时间
    /// </summary>
    private float delayed_time = 7f;

    /// <summary>
    /// 延时用的计时器
    /// </summary>
    private float time = 0;

    private void Start()
    {
        //获取杀死玩家的敌人名称
        Enemy_name = GameObject.Find("Knight").GetComponent<RestartGame>().Enemy_name;
        //获取TMP组件
        Text = GetComponent<TMP_Text>();
#if DEBUG_MODE
        if (Enemy_name == null)
        {
            Debug.LogError("未获取到敌人名称");
            return;
        }
        if (!Text)
        {
            Debug.LogError("未获取到TMP_Text组件");
            return;
        }
#endif
        //对不同敌人显示不同游戏结束的字体颜色
        switch (Enemy_name)
        {
            //若敌人名称为Zombie，则将初始文本颜色换为绿色透明(更改最初文本颜色是为了在文字颜色渐变时达成颜色统一)，最终显示颜色赋值为绿色不透明
            case "Zombie":
                Text.color = new Color(28 / 255f, 161 / 255f, 31 / 255f, 0 / 255f);
                EndColor = new Color(28 / 255f, 161 / 255f, 31 / 255f, 255 / 255f);
                break;
            //同上，颜色为白色
            case "Ghost":
                Text.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
                EndColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
                break;
            //红色
            case "Devil":
                Text.color = new Color(183 / 255f, 0 / 255f, 0 / 255f, 0 / 255f);
                EndColor = new Color(183 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);
                break;
            //橙色
            case "Pumpkin":
                Text.color = new Color(188 / 255f, 97 / 255f, 0 / 255f, 0 / 255f);
                EndColor = new Color(188 / 255f, 97 / 255f, 0 / 255f, 255 / 255f);
                break;
            //紫色
            case "Witch":
                Text.color = new Color(140 / 255f, 0 / 255f, 159 / 255f, 0 / 255f);
                EndColor = new Color(140 / 255f, 0 / 255f, 159 / 255f, 255 / 255f);
                break;

            default: break;
        }
        //将初始文本颜色和最终文本颜色赋给渐变过程，0代表是渐变最开始的颜色，1代表完成渐变后的颜色
        gradient.colorKeys = new GradientColorKey[] { new GradientColorKey(Text.color, 0f), new GradientColorKey(EndColor, 1f) };
    }

    /// <summary>
    /// 实现渐现
    /// </summary>
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