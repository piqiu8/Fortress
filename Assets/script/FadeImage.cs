//#define DEBUG_MODE

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ʵ��ͼƬ����
/// </summary>
public class FadeImage : MonoBehaviour
{
    /// <summary>
    /// ��Ҫ���ֵ�image
    /// </summary>
    private Image image;

    /// <summary>
    /// ������ɫ
    /// </summary>
    public Gradient gradient;

    /// <summary>
    /// �������ʱ�䣬Ĭ��Ϊ10s
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

    /// <summary>
    /// ��Ϸʤ����Ч
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// ��Ƶ������
    /// </summary>
    private AudioSource player;

    private void Start()
    {
        //��ȡImage���
        image = GetComponent<Image>();
        //��ȡ��Ƶ������
        player = GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!image)
        {
            Debug.LogError("δ��ȡ��image���");
            return;
        }
        if (!player)
        {
            Debug.LogError("δ��ȡ��AudioSource���");
            return;
        }
        if (!FadeImageDeBug())
        {
            Debug.LogError("��ʼ��ʧ��");
            return;
        }
#endif
        //������Ϸʤ����Ч
        player.PlayOneShot(audioClip);
        //�������ս�����ɫ
        EndColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        //����ʼͼƬ͸���Ⱥ�����ͼƬ͸���ȸ���������̣�0�����ǽ����ʼ����ɫ��1������ɽ�������ɫ
        gradient.colorKeys = new GradientColorKey[] { new GradientColorKey(image.color, 0f), new GradientColorKey(EndColor, 1f) };
    }

    private void Update()
    {
        // ���㵱ǰ�������
        float progress = Mathf.Clamp01(timer / duration);
        // ��ȡ��ǰ������ɫ
        Color color = gradient.Evaluate(progress);
        // ��������ɫӦ�õ�image��
        image.color = new Color(color.r, color.g, color.b, progress);
        // ���¼�ʱ��
        timer += Time.fixedUnscaledDeltaTime;
        // ��������Ѿ���ɣ�ֹͣ�ű�
        if (progress == 1f) enabled = false;
    }

#if DEBUG_MODE
    /// <summary>
    /// FadeImage��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool FadeImageDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent <CheckNull>();
        checkNull.Check(image, "image/��Ҫ���ֵ�ͼƬδ��ֵ");
        checkNull.Check(audioClip, "��Ϸʤ����Чδ��ֵ");
        checkNull.Check(player, "player/��Ƶ������δ��ֵ");
        return checkNull.State;
    }
#endif
}