using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 *功能：实现用按键或鼠标选择按钮时播放按钮选择音效并改按钮颜色
 *实现方法：导入四个关于按钮选择与鼠标选择的接口，并使用对应方法编写相应事件
 */

//OnPointerEnter,OnPointerExit,OnSelect,OnDeselect 这四个方法需要引入专门的接口，IPointerEnterHandler,IPointerExitHandler, ISelectHandler, IDeselectHandler
public class ButtonSound : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler, IPointerExitHandler
{
    //创建选择按钮时的音效
    public AudioClip soundClip;
    //创建一个音频播放器
    private AudioSource player;
    //创建一个文本组件
    private TMP_Text buttonText;
    //默认情况下按钮文字的颜色
    private Color defaultColor;
    //被选中情况下按钮文字的颜色
    private Color selectedColor;

    void Start(){
        //以RGB的方式给选中情况下的按钮文本赋一个颜色
        selectedColor = new Color(172 / 255f, 168 / 255f, 181 / 255f, 255 / 255f);
        //以RGB的方式给默认情况下的按钮文本赋一个颜色
        defaultColor = new Color(118 / 255f, 113 / 255f, 121 / 255f, 255 / 255f);
        //获取音频播放组件
        player = GetComponent<AudioSource>();
        //音量大小为一半
        player.volume = 0.5f;
        //获取子物体上的TMP_Text组件
        buttonText = GetComponentInChildren<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData eventData){//当按钮处于鼠标选中状态下
        //播放按钮音效
        player.PlayOneShot(soundClip);
        //将按钮文本颜色改为选中下的颜色
        buttonText.color = selectedColor;
    }

    public void OnPointerExit(PointerEventData eventData){//当按钮处于鼠标未选择状态
        //将按钮文本颜色改回默认颜色
        buttonText.color = defaultColor;
    }

    public void OnSelect(BaseEventData eventData){//当按钮处于按键选中状态下
        //播放按钮音效
        player.PlayOneShot(soundClip);
        //将按钮文本颜色改为选中下的颜色
        buttonText.color = selectedColor;
    }

    public void OnDeselect(BaseEventData eventData){//当按钮由按键选中恢复为未被选中状态下
        //将按钮文本颜色改回默认颜色
        buttonText.color = defaultColor;
    }
}
