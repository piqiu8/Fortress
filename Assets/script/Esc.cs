using UnityEngine;
using UnityEngine.SceneManagement;

/*
 *������Ϸ��ͣ�ű���
 */
public class Esc : MonoBehaviour
{
    //����esc UI��Ϸ����
    public GameObject ESC;
    //����һ��״ֵ̬������ȷ����Ϸ�Ƿ������У�Ĭ������Ϊtrue
    private bool IsGameRunning = true;
    //����һ�����ִ�����
    private GameObject MusicTrigger;
    //����һ����Ƶ������
    private AudioSource player;
    //����һ��״ֵ̬������ȷ�����������Ƿ���
    private bool IfBackgroundMusic;
    //����һ��2����Ƶ������
    private AudioSource player2;
    //����һ����ͣ������Ч
    public AudioClip clip_01;

    private void Start(){
        //��ȡ���ִ�����
        MusicTrigger=GameObject.Find("MusicTrigger");
        //��ȡ��Ƶ������
        player = MusicTrigger.GetComponent<AudioSource>();
        //��ȡ2����Ƶ������
        player2 = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update(){
        //��ʱ��ȡ�������ֵĲ���״̬
        IfBackgroundMusic = MusicTrigger.GetComponent<BackgroundMusic>().IfBackgroundMusic;
        //�Ƿ��¿ո��
        if (Input.GetKeyDown(KeyCode.Space)){
            //��
            //������ͣ������Ч
            player2.PlayOneShot(clip_01);
            //��Ϸ�Ƿ�������
            if(IsGameRunning){//��Ϸ������
                if(IfBackgroundMusic) player.Pause();
                //�������������ڲ��ţ�����ͣ��������
                Time.timeScale = 0;
                ESC.SetActive(true);
                //��ESC UI�ļ���״̬����Ϊtrue
            }
            else{//��Ϸδ����
                if(IfBackgroundMusic) player.UnPause();
                //���������ָֻ�����
                Time.timeScale = 1;
                //����Ϸ��������
                ESC.SetActive(false);
                //��ESC ����״̬����Ϊfalse
            }
            IsGameRunning = !IsGameRunning;
            //����Ϸ����״̬�÷�
        }
        if (ESC.activeInHierarchy){//��EscΪ����״̬
            if (Input.GetKeyDown(KeyCode.Return)){//�����»س���
                SceneManager.LoadScene(0);
                //ͬ������0�ų���
                Time.timeScale = 1;
                //����Ϸ��������
            }
        }
    }
}
