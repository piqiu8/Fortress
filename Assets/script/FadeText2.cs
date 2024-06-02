using TMPro;
using UnityEngine;

public class FadeText2 : MonoBehaviour
{
    // 定义需要渐变的UI Text对象
    private TMP_Text Text;
    // 定义渐变颜色
    public Gradient gradient;
    // 定义渐变持续时间
    public float duration = 10f;
    // 定义计时器
    private float timer = 0f;
    //渐变最终显示的颜色
    private Color EndColor;
    //是否延时显示
    public bool is_delayed = false;
    //延时时间
    private float delayed_time = 5f;
    //计数器
    private float time = 0;

    private void Start(){
        //获取TMP组件
        Text = GetComponent<TMP_Text>();
        //创建最终渐变颜色
        EndColor = new Color(255 / 255f, 143 / 255f, 0 / 255f, 255 / 255f);
        //将初始文本颜色和最终文本颜色赋给渐变过程，0代表是渐变最开始的颜色，1代表完成渐变后的颜色
        gradient.colorKeys = new GradientColorKey[] { new GradientColorKey(Text.color, 0f), new GradientColorKey(EndColor, 1f) };
    }

    private void fade(){
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

    void Update(){
        time += Time.fixedUnscaledDeltaTime;
        if (is_delayed){
            if (time > delayed_time) fade();
        }
        else fade();
    }
}