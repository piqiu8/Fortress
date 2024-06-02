using UnityEngine;

public class FireTrigger : MonoBehaviour
{
    public GameObject Torch1;
    //左侧火焰
    public GameObject Torch2;
    //右侧火焰
    private AudioSource player;
    //音频播放器
    public AudioClip Fire;
    //产生火焰音效
    // Start is called before the first frame update
    void Start(){
        player = GameObject.Find("torch_7").GetComponent<AudioSource>();
        //获取火炬的音频播放器
    }

    private void OnTriggerEnter(Collider other){//触发器进行触发检测
        if (other.name == "KnightBody"){//若触发对象为玩家
            if (!Torch1.activeInHierarchy && !Torch2.activeInHierarchy){//且左右火焰都未激活
                player.PlayOneShot(Fire,0.1f);
                //播放火焰产生音效
                Torch1.SetActive(true);
                Torch2.SetActive(true);
                //将两个火焰激活
            }
        }
    }
}
