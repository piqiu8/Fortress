using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    private float fadeSpeed = 0.45f;
    //�����ٶ�
    //private bool sceneStarting = true;
    private bool IsDie;
    //�����ж��Ƿ�����
    private RawImage backImage;
    //����ͼƬ

    void Start(){
        backImage = this.GetComponent<RawImage>();
        //��ȡ����ͼƬ
        backImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        //����������棬�������ڲ�ͬ�ֱ���
        backImage.color = Color.clear;
        //�ú���ͼƬһ��ʼ͸��
    }

    void Update(){
        IsDie = GameObject.Find("Knight").GetComponent<IfDie>().IsDie;
        //��ȡ�������״̬
        if (IsDie) EndScene();
        //�����������������
    }
    private void FadeBlack(){//������������͸����Ϊ��ɫ
        backImage.color = Color.Lerp(backImage.color, Color.black, fadeSpeed * Time.fixedUnscaledDeltaTime);
    }
    public void EndScene(){// ����ʱ����
        backImage.enabled = true;
        if (backImage.color.a <= 1f) FadeBlack();
    }
}
