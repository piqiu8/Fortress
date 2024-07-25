#define DEBUG_MODE

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 实现逐渐黑屏效果
/// </summary>
public class FadeToBlack : MonoBehaviour
{
    /// <summary>
    /// 渐变速度，默认为0.45
    /// </summary>
    private float fadeSpeed = 0.45f;

    /// <summary>
    /// 玩家死亡状态
    /// </summary>
    //private bool sceneStarting = true;
    private bool IsDie;

    /// <summary>
    /// 黑屏图片
    /// </summary>
    private RawImage backImage;

    private void Start()
    {
        //获取黑屏图片
        backImage = this.GetComponent<RawImage>();
#if DEBUG_MODE
        if(!backImage)
        {
            Debug.LogError("未获取到到图片");
            return;
        }
#endif
        //填充整个画面，并适用于不同分辨率
        backImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        //让黑屏图片一开始透明
        backImage.color = Color.clear;
    }

    private void Update()
    {
        //获取玩家死亡状态
        IsDie = GameObject.Find("Knight").GetComponent<IfDie>().IsDie;
        //死亡则进入死亡画面
        if (IsDie) EndScene();
    }

    /// <summary>
    /// 将黑屏画面由透明变为黑色
    /// </summary>
    private void FadeBlack()
    {
        backImage.color = Color.Lerp(backImage.color, Color.black, fadeSpeed * Time.fixedUnscaledDeltaTime);
    }

    /// <summary>
    /// 开始进入黑屏
    /// </summary>
    public void EndScene()
    {
        backImage.enabled = true;
        if (backImage.color.a <= 1f) FadeBlack();
    }
}