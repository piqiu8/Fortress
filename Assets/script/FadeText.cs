//#define DEBUG_MODE

using TMPro;
using UnityEngine;

/// <summary>
/// ����������������ʵ���ı�����
/// </summary>
public class FadeText : MonoBehaviour
{
    /// <summary>
    /// ��Ҫ������ı�
    /// </summary>
    // ������Ҫ�����UI Text����
    private TMP_Text Text;

    /// <summary>
    /// ������ɫ
    /// </summary>
    public Gradient gradient;

    /// <summary>
    /// �������ʱ�䣬Ĭ��10s
    /// </summary>
    public float duration = 10f;

    /// <summary>
    /// �����õļ�ʱ��
    /// </summary>
    private float timer = 0f;

    /// <summary>
    /// ����������ɫ
    /// </summary>
    private Color EndColor;

    /// <summary>
    /// ɱ����ҵĵ�������
    /// </summary>
    private string Enemy_name;

    /// <summary>
    /// �ӳ���ʾ״ֵ̬�����ڿ����Ƿ��ӳٽ���Ч��
    /// </summary>
    public bool is_delayed = false;

    /// <summary>
    /// �ӳ�ʱ��
    /// </summary>
    private float delayed_time = 7f;

    /// <summary>
    /// ��ʱ�õļ�ʱ��
    /// </summary>
    private float time = 0;

    private void Start()
    {
        //��ȡɱ����ҵĵ�������
        Enemy_name = GameObject.Find("Knight").GetComponent<RestartGame>().Enemy_name;
        //��ȡTMP���
        Text = GetComponent<TMP_Text>();
#if DEBUG_MODE
        if (Enemy_name == null)
        {
            Debug.LogError("δ��ȡ����������");
            return;
        }
        if (!Text)
        {
            Debug.LogError("δ��ȡ��TMP_Text���");
            return;
        }
#endif
        //�Բ�ͬ������ʾ��ͬ��Ϸ������������ɫ
        switch (Enemy_name)
        {
            //����������ΪZombie���򽫳�ʼ�ı���ɫ��Ϊ��ɫ͸��(��������ı���ɫ��Ϊ����������ɫ����ʱ�����ɫͳһ)��������ʾ��ɫ��ֵΪ��ɫ��͸��
            case "Zombie":
                Text.color = new Color(28 / 255f, 161 / 255f, 31 / 255f, 0 / 255f);
                EndColor = new Color(28 / 255f, 161 / 255f, 31 / 255f, 255 / 255f);
                break;
            //ͬ�ϣ���ɫΪ��ɫ
            case "Ghost":
                Text.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
                EndColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
                break;
            //��ɫ
            case "Devil":
                Text.color = new Color(183 / 255f, 0 / 255f, 0 / 255f, 0 / 255f);
                EndColor = new Color(183 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);
                break;
            //��ɫ
            case "Pumpkin":
                Text.color = new Color(188 / 255f, 97 / 255f, 0 / 255f, 0 / 255f);
                EndColor = new Color(188 / 255f, 97 / 255f, 0 / 255f, 255 / 255f);
                break;
            //��ɫ
            case "Witch":
                Text.color = new Color(140 / 255f, 0 / 255f, 159 / 255f, 0 / 255f);
                EndColor = new Color(140 / 255f, 0 / 255f, 159 / 255f, 255 / 255f);
                break;

            default: break;
        }
        //����ʼ�ı���ɫ�������ı���ɫ����������̣�0�����ǽ����ʼ����ɫ��1������ɽ�������ɫ
        gradient.colorKeys = new GradientColorKey[] { new GradientColorKey(Text.color, 0f), new GradientColorKey(EndColor, 1f) };
    }

    /// <summary>
    /// ʵ�ֽ���
    /// </summary>
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