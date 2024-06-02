using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
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
    //创建一个游戏胜利音效
    public AudioClip audioClip;
    //创建一个音频播放器
    private AudioSource player;

    private void Start(){
        //获取Image组件
        image=GetComponent<Image>();
        //获取音频播放器
        player = GetComponent<AudioSource>();
        //播放游戏胜利音效
        player.PlayOneShot(audioClip);
        //创建最终渐变颜色
        EndColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        //将初始图片透明度和最终图片透明度赋给渐变过程，0代表是渐变最开始的颜色，1代表完成渐变后的颜色
        gradient.colorKeys = new GradientColorKey[] { new GradientColorKey(image.color, 0f), new GradientColorKey(EndColor, 1f) };
    }

    void Update(){
        // 计算当前渐变进度
        float progress = Mathf.Clamp01(timer / duration);
        // 获取当前渐变颜色
        Color color = gradient.Evaluate(progress);
        // 将渐变颜色应用到UI Text对象中
        image.color = new Color(color.r, color.g, color.b, progress);
        // 更新计时器
        timer += Time.fixedUnscaledDeltaTime;
        // 如果渐变已经完成，停止脚本
        if (progress == 1f) enabled = false;
    }
}