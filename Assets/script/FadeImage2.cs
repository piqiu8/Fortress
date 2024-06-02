using UnityEngine;
using UnityEngine.UI;

public class FadeImage2 : MonoBehaviour
{
    // 定义需要渐变的Image对象
    private Image image;
    // 定义渐变颜色
    public Gradient gradient;
    // 定义渐变持续时间
    public float duration = 10f;
    // 定义计时器
    private float timer = 0f;
    //渐变最终显示的颜色
    private Color EndColor;

    private void Start(){
        //获取Image组件
        image = GetComponent<Image>();
        EndColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
        //将初始图片透明度和最终图片透明度赋给渐变过程，0代表是渐变最开始的颜色，1代表完成渐变后的颜色
        gradient.colorKeys = new GradientColorKey[] { new GradientColorKey(image.color, 0f), new GradientColorKey(EndColor, 1f) };
    }

    void Update(){
        // 计算当前渐变进度
        float progress = Mathf.Clamp01(timer / duration);
        // 获取当前渐变颜色
        Color color = gradient.Evaluate(progress);
        color.a = 1f - progress;
        // 将渐变颜色应用到UI Text对象中
        image.color = color;
        // 更新计时器
        timer += Time.unscaledDeltaTime;
        // 如果渐变已经完成，停止脚本
        if (progress == 1f) enabled = false;
    }
}