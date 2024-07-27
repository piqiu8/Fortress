//#define DEBUG_MODE

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ��ʼ����ĺ������֣�����ű�ֻʹ���˽���
/// </summary>
public class FadeInOut : MonoBehaviour
{
    /// <summary>
    /// ���뵭���ٶȣ�Ĭ��Ϊ0.3
    /// </summary>
    public float fadeSpeed = 0.3f;

    /// <summary>
    /// �Ƿ�����Ϸ��ʼUI������ж�ֵ
    /// </summary>
    private bool sceneStarting = true;

    /// <summary>
    /// ���뵭��ͼƬ
    /// </summary>
    private RawImage backImage;

    private void Start()
    {
        //��ȡ����
        backImage = this.GetComponent<RawImage>();
#if DEBUG_MODE
        if (!backImage)
        {
            Debug.LogError("δ��ȡ��RawImage���");
            return;
        }
        if (!FadeInOutDeBug())
        {
            Debug.LogError("��ʼ��ʧ��");
            return;
        }
#endif
        //���������Ļ����ʹ�ֱ��ʲ�ͬ
        backImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
    }

    private void Update()
    {
        if (sceneStarting) StartScene();
    }

    /// <summary>
    /// ʹͼƬ����
    /// </summary>
    private void FadeToClear()
    {
        //ʹ�ø���Ҷ������������Ч��ʹ�����ɺ�����Ϊ����
        backImage.color = Color.Lerp(backImage.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    /// <summary>
    /// ʹͼƬ����
    /// </summary>
    private void FadeToBlack()
    {
        //ʹ������������Ϊ����
        backImage.color = Color.Lerp(backImage.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    /// <summary>
    /// ִ����Ϸ��ʼ�����Ӧ����
    /// </summary>
    private void StartScene()
    {
        //ʹ����ͼƬ����
        backImage.enabled = true;
        //��������UI
        FadeToClear();
        //�������ͼƬ��͸���ȵ��ڸ�ֵ
        if (backImage.color.a <= 0.05f)
        {
            //ֱ�ӽ�����ͼƬ������Ϊ͸��
            backImage.color = Color.clear;
            //ȥ������ͼƬ
            backImage.enabled = false;
            //��Ϸ��ʼ���������Ϊ�����ٴη��ؿ�ʼ������׼��
            sceneStarting = false;
        }
    }

    /// <summary>
    /// δʹ�õķ�������������
    /// </summary>
    public void EndScene()
    {
        backImage.enabled = true;
        FadeToBlack();
        if (backImage.color.a >= 0.95f) SceneManager.LoadScene("��һ������");
    }

#if DEBUG_MODE
    /// <summary>
    /// FadeInOut��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool FadeInOutDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(backImage, "backImage/���뵭��ͼƬδ��ֵ");
        return checkNull.State;
    }
#endif
}