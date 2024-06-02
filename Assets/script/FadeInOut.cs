using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//这个脚本只使用了渐现
public class FadeInOut : MonoBehaviour
{
    public float fadeSpeed = 0.3f;
    //淡入淡出速度
    private bool sceneStarting = true;
    //是否在游戏开始UI
    private RawImage backImage;
    //淡入淡出画面

    void Start(){
        backImage = this.GetComponent<RawImage>();
        //获取画面
        backImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        //填充整个屏幕，即使分辨率不同
    }

    void Update(){
        if (sceneStarting) StartScene();
    }
    private void FadeToClear(){//渐现
        backImage.color = Color.Lerp(backImage.color, Color.clear, fadeSpeed * Time.deltaTime);
        //使用傅里叶函数做出渐变效果使画面由黑屏变为正常
    }
    private void FadeToBlack(){//渐隐
        backImage.color = Color.Lerp(backImage.color, Color.black, fadeSpeed * Time.deltaTime);
        //使画面由正常变为黑屏
    }
    // 初始化时调用
    private void StartScene(){//游戏开始界面
        backImage.enabled = true;
        //使黑屏图片出现
        FadeToClear();
        //慢慢显现UI
        if (backImage.color.a <= 0.05f){//如果黑屏图片的透明度低于该值
            backImage.color = Color.clear;
            //直接将黑屏图片整个变为透明
            backImage.enabled = false;
            //去除黑屏图片
            sceneStarting = false;
            //游戏开始界面结束，为后面再次返回开始界面做准备
        }
    }
    public void EndScene(){// 结束时调用
        backImage.enabled = true;
        FadeToBlack();
        if (backImage.color.a >= 0.95f) SceneManager.LoadScene("另一个场景");
    }
}
