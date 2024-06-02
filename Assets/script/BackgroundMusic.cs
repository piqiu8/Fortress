using UnityEngine;

/*
 *���ܣ�ʵ�ֱ������ֵĲ���
 *ʵ�ַ�����ͨ���������鿴����Ƿ�ﵽָ��Ŀ�ĵأ������򲥷ű�������
 */

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip Music;
    //������������
    private AudioSource player;
    //������Ƶ������
    [HideInInspector]public bool IfBackgroundMusic = false;
    //�����жϱ��������Ƿ���
    void Start(){
        player = GetComponent<AudioSource>();
        //��ȡ���������
        player.clip = Music;
        //���ò�����ƵΪMusic
        player.loop = true;
        //����ѭ������
        player.volume = 0.2f;
        //��������Ϊ0.2��
    }

    private void OnTriggerEnter(Collider other){//����Ҵ���������ʱ
        if (other.name == "KnightBody"){
            //����ײ��������ΪKnightBody
            if (!player.isPlaying){
                //����������Ϊ�ر�״̬
                IfBackgroundMusic = true;
                //����������״̬��Ϊ���������ű�������
                player.Play();
            }
        }
    }
}
