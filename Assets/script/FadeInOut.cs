//#define DEBUG_MODE

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 开始界面的黑屏渐现，这个脚本只使用了渐隐
/// </summary>
public class FadeInOut : MonoBehaviour
{
    /// <summary>
    /// 淡入淡出速度，默认为0.3
    /// </summary>
    public float fadeSpeed = 0.3f;

    /// <summary>
    /// 是否在游戏开始UI界面的判断值
    /// </summary>
    private bool sceneStarting = true;

    /// <summary>
    /// 淡入淡出图片
    /// </summary>
    private RawImage backImage;

    private void Start()
    {
        //获取画面
        backImage = this.GetComponent<RawImage>();
#if DEBUG_MODE
        if (!backImage)
        {
            Debug.LogError("未获取到RawImage组件");
            return;
        }
        if (!FadeInOutDeBug())
        {
            Debug.LogError("初始化失败");
            return;
        }
#endif
        //填充整个屏幕，即使分辨率不同
        backImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
    }

    private void Update()
    {
        if (sceneStarting) StartScene();
    }

    /// <summary>
    /// 使图片渐隐
    /// </summary>
    private void FadeToClear()
    {
        //使用傅里叶函数做出渐变效果使画面由黑屏变为正常
        backImage.color = Color.Lerp(backImage.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 使图片渐现
    /// </summary>
    private void FadeToBlack()
    {
        //使画面由正常变为黑屏
        backImage.color = Color.Lerp(backImage.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 执行游戏开始界面对应操作
    /// </summary>
    private void StartScene()
    {
        //使黑屏图片出现
        backImage.enabled = true;
        //慢慢显现UI
        FadeToClear();
        //如果黑屏图片的透明度低于该值
        if (backImage.color.a <= 0.05f)
        {
            //直接将黑屏图片整个变为透明
            backImage.color = Color.clear;
            //去除黑屏图片
            backImage.enabled = false;
            //游戏开始界面结束，为后面再次返回开始界面做准备
            sceneStarting = false;
        }
    }

    /// <summary>
    /// 未使用的方法，不用在意
    /// </summary>
    public void EndScene()
    {
        backImage.enabled = true;
        FadeToBlack();
        if (backImage.color.a >= 0.95f) SceneManager.LoadScene("另一个场景");
    }

#if DEBUG_MODE
    /// <summary>
    /// FadeInOut初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool FadeInOutDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(backImage, "backImage/淡入淡出图片未赋值");
        return checkNull.State;
    }
#endif
}