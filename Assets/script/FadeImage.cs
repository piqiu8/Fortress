using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    // ������Ҫ�����Image����
    private Image image;
    // ���彥����ɫ
    public Gradient gradient;
    // ���彥�����ʱ��
    public float duration = 10f;
    // �����ʱ��
    private float timer = 0f;
    //����������ʾ����ɫ
    private Color EndColor;
    //����һ����Ϸʤ����Ч
    public AudioClip audioClip;
    //����һ����Ƶ������
    private AudioSource player;

    private void Start(){
        //��ȡImage���
        image=GetComponent<Image>();
        //��ȡ��Ƶ������
        player = GetComponent<AudioSource>();
        //������Ϸʤ����Ч
        player.PlayOneShot(audioClip);
        //�������ս�����ɫ
        EndColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        //����ʼͼƬ͸���Ⱥ�����ͼƬ͸���ȸ���������̣�0�����ǽ����ʼ����ɫ��1������ɽ�������ɫ
        gradient.colorKeys = new GradientColorKey[] { new GradientColorKey(image.color, 0f), new GradientColorKey(EndColor, 1f) };
    }

    void Update(){
        // ���㵱ǰ�������
        float progress = Mathf.Clamp01(timer / duration);
        // ��ȡ��ǰ������ɫ
        Color color = gradient.Evaluate(progress);
        // ��������ɫӦ�õ�UI Text������
        image.color = new Color(color.r, color.g, color.b, progress);
        // ���¼�ʱ��
        timer += Time.fixedUnscaledDeltaTime;
        // ��������Ѿ���ɣ�ֹͣ�ű�
        if (progress == 1f) enabled = false;
    }
}