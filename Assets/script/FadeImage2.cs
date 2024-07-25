//#define DEBUG_MODE

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ʵ��ͼƬ����
/// </summary>
public class FadeImage2 : MonoBehaviour
{
    /// <summary>
    /// ��Ҫ������image
    /// </summary>
    private Image image;

    /// <summary>
    /// ������ɫ
    /// </summary>
    public Gradient gradient;

    /// <summary>
    /// �������ʱ�䣬Ĭ��10s
    /// </summary>
    public float duration = 10f;

    /// <summary>
    /// ��ʱ��
    /// </summary>
    private float timer = 0f;

    /// <summary>
    /// ����������ɫ
    /// </summary>
    private Color EndColor;

    private void Start()
    {
        //��ȡImage���
        image = GetComponent<Image>();
#if DEBUG_MODE
        if (!image)
        {
            Debug.LogError("δ��ȡ��image���");
            return;
        }
        if (!FadeImage2DeBug())
        {
            Debug.LogError("��ʼ��ʧ��");
            return;
        }
#endif
        EndColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
        //����ʼͼƬ͸���Ⱥ�����ͼƬ͸���ȸ���������̣�0�����ǽ����ʼ����ɫ��1������ɽ�������ɫ
        gradient.colorKeys = new GradientColorKey[] { new GradientColorKey(image.color, 0f), new GradientColorKey(EndColor, 1f) };
    }

    private void Update()
    {
        // ���㵱ǰ�������
        float progress = Mathf.Clamp01(timer / duration);
        // ��ȡ��ǰ������ɫ
        Color color = gradient.Evaluate(progress);
        color.a = 1f - progress;
        // ��������ɫӦ�õ�image������
        image.color = color;
        // ���¼�ʱ��
        timer += Time.unscaledDeltaTime;
        // ��������Ѿ���ɣ�ֹͣ�ű�
        if (progress == 1f) enabled = false;
    }

#if DEBUG_MODE
    /// <summary>
    /// FadeImage2��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool FadeImage2DeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(image, "image/��Ҫ������imageδ��ֵ");
        return checkNull.State;
    }
#endif
}