using UnityEngine;

public class FireTrigger : MonoBehaviour
{
    public GameObject Torch1;
    //������
    public GameObject Torch2;
    //�Ҳ����
    private AudioSource player;
    //��Ƶ������
    public AudioClip Fire;
    //����������Ч
    // Start is called before the first frame update
    void Start(){
        player = GameObject.Find("torch_7").GetComponent<AudioSource>();
        //��ȡ������Ƶ������
    }

    private void OnTriggerEnter(Collider other){//���������д������
        if (other.name == "KnightBody"){//����������Ϊ���
            if (!Torch1.activeInHierarchy && !Torch2.activeInHierarchy){//�����һ��涼δ����
                player.PlayOneShot(Fire,0.1f);
                //���Ż��������Ч
                Torch1.SetActive(true);
                Torch2.SetActive(true);
                //���������漤��
            }
        }
    }
}
