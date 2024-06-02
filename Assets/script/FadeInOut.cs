using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//����ű�ֻʹ���˽���
public class FadeInOut : MonoBehaviour
{
    public float fadeSpeed = 0.3f;
    //���뵭���ٶ�
    private bool sceneStarting = true;
    //�Ƿ�����Ϸ��ʼUI
    private RawImage backImage;
    //���뵭������

    void Start(){
        backImage = this.GetComponent<RawImage>();
        //��ȡ����
        backImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        //���������Ļ����ʹ�ֱ��ʲ�ͬ
    }

    void Update(){
        if (sceneStarting) StartScene();
    }
    private void FadeToClear(){//����
        backImage.color = Color.Lerp(backImage.color, Color.clear, fadeSpeed * Time.deltaTime);
        //ʹ�ø���Ҷ������������Ч��ʹ�����ɺ�����Ϊ����
    }
    private void FadeToBlack(){//����
        backImage.color = Color.Lerp(backImage.color, Color.black, fadeSpeed * Time.deltaTime);
        //ʹ������������Ϊ����
    }
    // ��ʼ��ʱ����
    private void StartScene(){//��Ϸ��ʼ����
        backImage.enabled = true;
        //ʹ����ͼƬ����
        FadeToClear();
        //��������UI
        if (backImage.color.a <= 0.05f){//�������ͼƬ��͸���ȵ��ڸ�ֵ
            backImage.color = Color.clear;
            //ֱ�ӽ�����ͼƬ������Ϊ͸��
            backImage.enabled = false;
            //ȥ������ͼƬ
            sceneStarting = false;
            //��Ϸ��ʼ���������Ϊ�����ٴη��ؿ�ʼ������׼��
        }
    }
    public void EndScene(){// ����ʱ����
        backImage.enabled = true;
        FadeToBlack();
        if (backImage.color.a >= 0.95f) SceneManager.LoadScene("��һ������");
    }
}
