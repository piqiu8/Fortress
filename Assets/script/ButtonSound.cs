using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 *���ܣ�ʵ���ð��������ѡ��ťʱ���Ű�ťѡ����Ч���İ�ť��ɫ
 *ʵ�ַ����������ĸ����ڰ�ťѡ�������ѡ��Ľӿڣ���ʹ�ö�Ӧ������д��Ӧ�¼�
 */

//OnPointerEnter,OnPointerExit,OnSelect,OnDeselect ���ĸ�������Ҫ����ר�ŵĽӿڣ�IPointerEnterHandler,IPointerExitHandler, ISelectHandler, IDeselectHandler
public class ButtonSound : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler, IPointerExitHandler
{
    //����ѡ��ťʱ����Ч
    public AudioClip soundClip;
    //����һ����Ƶ������
    private AudioSource player;
    //����һ���ı����
    private TMP_Text buttonText;
    //Ĭ������°�ť���ֵ���ɫ
    private Color defaultColor;
    //��ѡ������°�ť���ֵ���ɫ
    private Color selectedColor;

    void Start(){
        //��RGB�ķ�ʽ��ѡ������µİ�ť�ı���һ����ɫ
        selectedColor = new Color(172 / 255f, 168 / 255f, 181 / 255f, 255 / 255f);
        //��RGB�ķ�ʽ��Ĭ������µİ�ť�ı���һ����ɫ
        defaultColor = new Color(118 / 255f, 113 / 255f, 121 / 255f, 255 / 255f);
        //��ȡ��Ƶ�������
        player = GetComponent<AudioSource>();
        //������СΪһ��
        player.volume = 0.5f;
        //��ȡ�������ϵ�TMP_Text���
        buttonText = GetComponentInChildren<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData eventData){//����ť�������ѡ��״̬��
        //���Ű�ť��Ч
        player.PlayOneShot(soundClip);
        //����ť�ı���ɫ��Ϊѡ���µ���ɫ
        buttonText.color = selectedColor;
    }

    public void OnPointerExit(PointerEventData eventData){//����ť�������δѡ��״̬
        //����ť�ı���ɫ�Ļ�Ĭ����ɫ
        buttonText.color = defaultColor;
    }

    public void OnSelect(BaseEventData eventData){//����ť���ڰ���ѡ��״̬��
        //���Ű�ť��Ч
        player.PlayOneShot(soundClip);
        //����ť�ı���ɫ��Ϊѡ���µ���ɫ
        buttonText.color = selectedColor;
    }

    public void OnDeselect(BaseEventData eventData){//����ť�ɰ���ѡ�лָ�Ϊδ��ѡ��״̬��
        //����ť�ı���ɫ�Ļ�Ĭ����ɫ
        buttonText.color = defaultColor;
    }
}
