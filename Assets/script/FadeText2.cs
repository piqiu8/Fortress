//#define DEBUG_MODE

using TMPro;
using UnityEngine;

/// <summary>
/// �������ʤ�������ʵ���ı�����
/// </summary>
public class FadeText2 : MonoBehaviour
{
    /// <summary>
    /// ��Ҫ������ı�
    /// </summary>
    private TMP_Text Text;

    /// <summary>
    /// ������ɫ
    /// </summary>
    public Gradient gradient;

    /// <summary>
    /// �������ʱ�䣬Ĭ��Ϊ10s
    /// </summary>
    public float duration = 10f;

    /// <summary>
    /// �����õļ�ʱ��
    /// </summary>
    private float timer = 0f;

    /// <summary>
    /// ���ս�����ɫ
    /// </summary>
    private Color EndColor;

    /// <summary>
    /// �ӳ���ʾ״ֵ̬�����ڿ����Ƿ��ӳٽ���Ч��
    /// </summary>
    public bool is_delayed = false;

    /// <summary>
    /// �ӳ�ʱ��
    /// </summary>
    private float delayed_time = 5f;

    /// <summary>
    /// ��ʱ�õļ�ʱ��
    /// </summary>
    private float time = 0;

    private void Start()
    {
        //��ȡTMP���
        Text = GetComponent<TMP_Text>();
#if DEBUG_MODE
        if(!Text)
        {
            Debug.LogError("δ��ȡ��TMP_Text");
            return;
        }
#endif
        //�������ս�����ɫ
        EndColor = new Color(255 / 255f, 143 / 255f, 0 / 255f, 255 / 255f);
        //����ʼ�ı���ɫ�������ı���ɫ����������̣�0�����ǽ����ʼ����ɫ��1������ɽ�������ɫ
        gradient.colorKeys = new GradientColorKey[] { new GradientColorKey(Text.color, 0f), new GradientColorKey(EndColor, 1f) };
    }

    private void fade()
    {
        // ���㵱ǰ�������
        float progress = Mathf.Clamp01(timer / duration);
        // ��ȡ��ǰ������ɫ
        Color color = gradient.Evaluate(progress);
        // ��������ɫӦ�õ�UI Text������
        Text.color = new Color(color.r, color.g, color.b, progress);
        // ���¼�ʱ��
        timer += Time.fixedUnscaledDeltaTime;
        // ��������Ѿ���ɣ�ֹͣ�ű�
        if (progress == 1f) enabled = false;
    }

    private void Update()
    {
        time += Time.fixedUnscaledDeltaTime;
        if (is_delayed)
        {
            if (time > delayed_time) fade();
        }
        else fade();
    }
}