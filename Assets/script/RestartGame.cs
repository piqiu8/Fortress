using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // Start is called before the first frame update
    //��������UI
    public GameObject relive;
    private bool IsDie;
    private AudioSource player;
    public AudioClip GameOver;
    private AudioSource BackgroundMusic;
    //��������bool����
    private int num = 0;
    //Ϊ��ʱЧ��׼��
    private float time = 0, time2 = 0;
    //��ʱʱ��
    private float delayed_time=10f;
    //ɱ����ҵĵ�������
    [HideInInspector]public string Enemy_name;
    //����Ϸ������Ч��ʼ���ź��ʱ�ļ�ʱ���������õ���ɱ����Һ󲥷���Ч����Ϸ������Ч�м䲥��
    private float Enemy_time;
    //�Ƿ���Կ�ʼ��ʱ
    private bool if_enemy_time = false;
    //����ɱ����Һ󲥷���Ч����
    public AudioClip[] enemy_win_voice;

    private void Start(){
        player = GetComponent<AudioSource>();
        BackgroundMusic=GameObject.Find("MusicTrigger").GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update(){
        IsDie = GetComponent<IfDie>().IsDie;
        //�ڲ�����Ϸ������Ч��ʼ��ʱ
        if (if_enemy_time){//��ʼ��ʱ
            Enemy_time += Time.fixedUnscaledDeltaTime;
            //�Ƿ�ʱ�䳬��5s
            if (Enemy_time > 6){//5s�󣬸���ɱ����ҵĵ������Ʋ��Ų�ͬ����Ч
                switch (Enemy_name){
                    //����zombieɱ����Һ����Ч
                    case "Zombie": player.PlayOneShot(enemy_win_voice[0],1f); break;
                    //����witchɱ����Һ����Ч
                    case "Witch": player.PlayOneShot(enemy_win_voice[1], 1f);break;
                    //����ghostɱ����Һ����Ч
                    case "Ghost": player.PlayOneShot(enemy_win_voice[2], 1f); break;
                    //����pumpkinɱ����Һ����Ч
                    case "Pumpkin":player.PlayOneShot(enemy_win_voice[3], 1f); break;
                    //����devilɱ����Һ����Ч
                    case "Devil":player.PlayOneShot(enemy_win_voice[4], 1f); break;
                    default:break;
                }
                //ֹͣ��ʱ�������ظ�����
                if_enemy_time = false;
            }
        }
        if (IsDie){//�������
            //��ʼ��ʱ
            time += Time.fixedUnscaledDeltaTime;
            time2+= Time.fixedUnscaledDeltaTime;
            //ֹͣ��������
            BackgroundMusic.Stop();
            if (time > 1){//1s�󲥷���Ϸ������Ч
                //ʱ�����㣬Ϊ�´����¼�ʱ׼��
                time = 0;
                if (num == 0){//
                    if_enemy_time = true;
                    player.PlayOneShot(GameOver,0.5f);
                    num++;
                }
            }
            //�ر�Esc���
            GetComponent<Esc>().enabled = false;
            //����relive UI
            relive.SetActive(true);
            if (time2 > delayed_time){
                if (Input.anyKeyDown){//����������¿�ʼ
                    time2 = 0;
                    SceneManager.LoadScene(1);
                    Time.timeScale = 1;
                }
            }
        }
    }
}
