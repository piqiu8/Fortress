using TMPro;
using UnityEngine;

public class FadeText2 : MonoBehaviour
{
    // ������Ҫ�����UI Text����
    private TMP_Text Text;
    // ���彥����ɫ
    public Gradient gradient;
    // ���彥�����ʱ��
    public float duration = 10f;
    // �����ʱ��
    private float timer = 0f;
    //����������ʾ����ɫ
    private Color EndColor;
    //�Ƿ���ʱ��ʾ
    public bool is_delayed = false;
    //��ʱʱ��
    private float delayed_time = 5f;
    //������
    private float time = 0;

    private void Start(){
        //��ȡTMP���
        Text = GetComponent<TMP_Text>();
        //�������ս�����ɫ
        EndColor = new Color(255 / 255f, 143 / 255f, 0 / 255f, 255 / 255f);
        //����ʼ�ı���ɫ�������ı���ɫ����������̣�0�����ǽ����ʼ����ɫ��1������ɽ�������ɫ
        gradient.colorKeys = new GradientColorKey[] { new GradientColorKey(Text.color, 0f), new GradientColorKey(EndColor, 1f) };
    }

    private void fade(){
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

    void Update(){
        time += Time.fixedUnscaledDeltaTime;
        if (is_delayed){
            if (time > delayed_time) fade();
        }
        else fade();
    }
}