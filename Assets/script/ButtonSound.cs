//#define DEBUG_MODE

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ʵ���ð��������ѡ��ťʱ���Ű�ťѡ����Ч���İ�ť��ɫ |
/// ʵ�ַ����������ĸ����ڰ�ťѡ�������ѡ��Ľӿڣ���ʹ�ö�Ӧ������д��Ӧ�¼�������
/// OnPointerEnter,OnPointerExit,OnSelect,OnDeselect ���ĸ�������Ҫ����ר�ŵĽӿڣ�IPointerEnterHandler,IPointerExitHandler, ISelectHandler, IDeselectHandler
/// </summary>
public class ButtonSound : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler, IPointerExitHandler
{
    /// <summary>
    /// ѡ��ťʱ����Ч
    /// </summary>
    public AudioClip soundClip;

    /// <summary>
    /// ��ť��Ч�������
    /// </summary>
    private AudioSource ButtonClipPlayer;

    /// <summary>
    /// ��ť�ı����
    /// </summary>
    private TMP_Text ButtonText;

    /// <summary>
    /// Ĭ��ʱ��ť������ɫ
    /// </summary>
    private Color DefaultColor;

    /// <summary>
    /// ��ѡ��ʱ��ť������ɫ
    /// </summary>
    private Color SelectedColor;

    private void Start()
    {
        //��RGB�ķ�ʽ��ѡ������µİ�ť�ı���һ����ɫ
        SelectedColor = new Color(172 / 255f, 168 / 255f, 181 / 255f, 255 / 255f);
        //��RGB�ķ�ʽ��Ĭ������µİ�ť�ı���һ����ɫ
        DefaultColor = new Color(118 / 255f, 113 / 255f, 121 / 255f, 255 / 255f);
        //��ȡ��Ƶ�������
        ButtonClipPlayer = GetComponent<AudioSource>();
        //��ȡ�������ϵ�TMP_Text���
        ButtonText = GetComponentInChildren<TMP_Text>();
#if DEBUG_MODE
        if (!ButtonClipPlayer)
        {
            Debug.LogError("δ��ȡ��AudioSource���");
            return;
        }
        if (!ButtonText)
        {
            Debug.LogError("δ��ȡ��TMP_Text���");
            return;
        }
        if (!ButtonSoundDeBug())
        {
            Debug.LogError("��ʼ��ʧ��");
            return;
        }
#endif
        //������СΪһ��
        ButtonClipPlayer.volume = 0.5f;
    }

    /// <summary>
    /// ���ѡ�а�ťʱִ����Ӧ�¼�
    /// </summary>
    /// <param name="eventData">�����Դ�����</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //���Ű�ť��Ч
        ButtonClipPlayer.PlayOneShot(soundClip);
        //����ť�ı���ɫ��Ϊѡ���µ���ɫ
        ButtonText.color = SelectedColor;
    }

    /// <summary>
    /// ���δѡ�а�ťʱִ�ж�Ӧ�¼�
    /// </summary>
    /// <param name="eventData">�����Դ�����</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //����ť�ı���ɫ�Ļ�Ĭ����ɫ
        ButtonText.color = DefaultColor;
    }

    /// <summary>
    /// ����ѡ�а�ťʱִ����Ӧ�¼�
    /// </summary>
    /// <param name="eventData">�����Դ�����</param>
    public void OnSelect(BaseEventData eventData)
    {
        //���Ű�ť��Ч
        ButtonClipPlayer.PlayOneShot(soundClip);
        //����ť�ı���ɫ��Ϊѡ���µ���ɫ
        ButtonText.color = SelectedColor;
    }

    /// <summary>
    /// ����δѡ�а�ťʱִ����Ӧ�¼�
    /// </summary>
    /// <param name="eventData">�����Դ�����</param>
    public void OnDeselect(BaseEventData eventData)
    {
        //����ť�ı���ɫ�Ļ�Ĭ����ɫ
        ButtonText.color = DefaultColor;
    }

#if DEBUG_MODE
    /// <summary>
    /// ButtonSound��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool ButtonSoundDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(soundClip, "SoundClip/ѡ��ť��Ӧ��Чδ��ֵ");
        checkNull.Check(ButtonClipPlayer, "ButtonClipPlayer/��ť��Ч������δ��ֵ");
        checkNull.Check(ButtonText, "ButtonText/��ť�ı����δ��ֵ");
        checkNull.Check(DefaultColor, "DefaultColor/Ĭ�ϰ�ť������ɫδ��ֵ");
        checkNull.Check(SelectedColor, "SelectedColor/��ѡ�а�ť������ɫδ��ֵ");
        return checkNull.State;
    }
#endif
}