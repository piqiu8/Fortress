using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    private float fadeSpeed = 0.45f;
    //渐变速度
    //private bool sceneStarting = true;
    private bool IsDie;
    //用于判断是否死亡
    private RawImage backImage;
    //黑屏图片

    void Start(){
        backImage = this.GetComponent<RawImage>();
        //获取黑屏图片
        backImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        //填充整个画面，并适用于不同分辨率
        backImage.color = Color.clear;
        //让黑屏图片一开始透明
    }

    void Update(){
        IsDie = GameObject.Find("Knight").GetComponent<IfDie>().IsDie;
        //获取玩家死亡状态
        if (IsDie) EndScene();
        //死亡则进入死亡画面
    }
    private void FadeBlack(){//将黑屏画面由透明变为黑色
        backImage.color = Color.Lerp(backImage.color, Color.black, fadeSpeed * Time.fixedUnscaledDeltaTime);
    }
    public void EndScene(){// 结束时调用
        backImage.enabled = true;
        if (backImage.color.a <= 1f) FadeBlack();
    }
}
