#define DEBUG_MODE

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ʵ���𽥺���Ч��
/// </summary>
public class FadeToBlack : MonoBehaviour
{
    /// <summary>
    /// �����ٶȣ�Ĭ��Ϊ0.45
    /// </summary>
    private float fadeSpeed = 0.45f;

    /// <summary>
    /// �������״̬
    /// </summary>
    //private bool sceneStarting = true;
    private bool IsDie;

    /// <summary>
    /// ����ͼƬ
    /// </summary>
    private RawImage backImage;

    private void Start()
    {
        //��ȡ����ͼƬ
        backImage = this.GetComponent<RawImage>();
#if DEBUG_MODE
        if(!backImage)
        {
            Debug.LogError("δ��ȡ����ͼƬ");
            return;
        }
#endif
        //����������棬�������ڲ�ͬ�ֱ���
        backImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        //�ú���ͼƬһ��ʼ͸��
        backImage.color = Color.clear;
    }

    private void Update()
    {
        //��ȡ�������״̬
        IsDie = GameObject.Find("Knight").GetComponent<IfDie>().IsDie;
        //�����������������
        if (IsDie) EndScene();
    }

    /// <summary>
    /// ������������͸����Ϊ��ɫ
    /// </summary>
    private void FadeBlack()
    {
        backImage.color = Color.Lerp(backImage.color, Color.black, fadeSpeed * Time.fixedUnscaledDeltaTime);
    }

    /// <summary>
    /// ��ʼ�������
    /// </summary>
    public void EndScene()
    {
        backImage.enabled = true;
        if (backImage.color.a <= 1f) FadeBlack();
    }
}