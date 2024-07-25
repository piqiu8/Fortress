//#define DEBUG_MODE

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 实现用按键或鼠标选择按钮时播放按钮选择音效并改按钮颜色 |
/// 实现方法：导入四个关于按钮选择与鼠标选择的接口，并使用对应方法编写相应事件，其中
/// OnPointerEnter,OnPointerExit,OnSelect,OnDeselect 这四个方法需要引入专门的接口，IPointerEnterHandler,IPointerExitHandler, ISelectHandler, IDeselectHandler
/// </summary>
public class ButtonSound : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler, IPointerExitHandler
{
    /// <summary>
    /// 选择按钮时的音效
    /// </summary>
    public AudioClip soundClip;

    /// <summary>
    /// 按钮音效播放组件
    /// </summary>
    private AudioSource ButtonClipPlayer;

    /// <summary>
    /// 按钮文本组件
    /// </summary>
    private TMP_Text ButtonText;

    /// <summary>
    /// 默认时按钮文字颜色
    /// </summary>
    private Color DefaultColor;

    /// <summary>
    /// 被选中时按钮文字颜色
    /// </summary>
    private Color SelectedColor;

    private void Start()
    {
        //以RGB的方式给选中情况下的按钮文本赋一个颜色
        SelectedColor = new Color(172 / 255f, 168 / 255f, 181 / 255f, 255 / 255f);
        //以RGB的方式给默认情况下的按钮文本赋一个颜色
        DefaultColor = new Color(118 / 255f, 113 / 255f, 121 / 255f, 255 / 255f);
        //获取音频播放组件
        ButtonClipPlayer = GetComponent<AudioSource>();
        //获取子物体上的TMP_Text组件
        ButtonText = GetComponentInChildren<TMP_Text>();
#if DEBUG_MODE
        if (!ButtonClipPlayer)
        {
            Debug.LogError("未获取到AudioSource组件");
            return;
        }
        if (!ButtonText)
        {
            Debug.LogError("未获取到TMP_Text组件");
            return;
        }
        if (!ButtonSoundDeBug())
        {
            Debug.LogError("初始化失败");
            return;
        }
#endif
        //音量大小为一半
        ButtonClipPlayer.volume = 0.5f;
    }

    /// <summary>
    /// 鼠标选中按钮时执行相应事件
    /// </summary>
    /// <param name="eventData">方法自带参数</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //播放按钮音效
        ButtonClipPlayer.PlayOneShot(soundClip);
        //将按钮文本颜色改为选中下的颜色
        ButtonText.color = SelectedColor;
    }

    /// <summary>
    /// 鼠标未选中按钮时执行对应事件
    /// </summary>
    /// <param name="eventData">方法自带参数</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //将按钮文本颜色改回默认颜色
        ButtonText.color = DefaultColor;
    }

    /// <summary>
    /// 按键选中按钮时执行相应事件
    /// </summary>
    /// <param name="eventData">方法自带参数</param>
    public void OnSelect(BaseEventData eventData)
    {
        //播放按钮音效
        ButtonClipPlayer.PlayOneShot(soundClip);
        //将按钮文本颜色改为选中下的颜色
        ButtonText.color = SelectedColor;
    }

    /// <summary>
    /// 按键未选中按钮时执行相应事件
    /// </summary>
    /// <param name="eventData">方法自带参数</param>
    public void OnDeselect(BaseEventData eventData)
    {
        //将按钮文本颜色改回默认颜色
        ButtonText.color = DefaultColor;
    }

#if DEBUG_MODE
    /// <summary>
    /// ButtonSound初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool ButtonSoundDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(soundClip, "SoundClip/选择按钮对应音效未赋值");
        checkNull.Check(ButtonClipPlayer, "ButtonClipPlayer/按钮音效播放器未赋值");
        checkNull.Check(ButtonText, "ButtonText/按钮文本组件未赋值");
        checkNull.Check(DefaultColor, "DefaultColor/默认按钮文字颜色未赋值");
        checkNull.Check(SelectedColor, "SelectedColor/被选中按钮文字颜色未赋值");
        return checkNull.State;
    }
#endif
}