using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // Start is called before the first frame update
    //死亡界面UI
    public GameObject relive;
    private bool IsDie;
    private AudioSource player;
    public AudioClip GameOver;
    private AudioSource BackgroundMusic;
    //单纯当做bool来用
    private int num = 0;
    //为延时效果准备
    private float time = 0, time2 = 0;
    //延时时间
    private float delayed_time=10f;
    //杀死玩家的敌人名字
    [HideInInspector]public string Enemy_name;
    //在游戏结束音效开始播放后计时的计时器，用来让敌人杀死玩家后播放音效在游戏结束音效中间播放
    private float Enemy_time;
    //是否可以开始计时
    private bool if_enemy_time = false;
    //敌人杀死玩家后播放音效集合
    public AudioClip[] enemy_win_voice;

    private void Start(){
        player = GetComponent<AudioSource>();
        BackgroundMusic=GameObject.Find("MusicTrigger").GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update(){
        IsDie = GetComponent<IfDie>().IsDie;
        //在播放游戏结束音效后开始计时
        if (if_enemy_time){//开始计时
            Enemy_time += Time.fixedUnscaledDeltaTime;
            //是否时间超过5s
            if (Enemy_time > 6){//5s后，根据杀死玩家的敌人名称播放不同的音效
                switch (Enemy_name){
                    //播放zombie杀死玩家后的音效
                    case "Zombie": player.PlayOneShot(enemy_win_voice[0],1f); break;
                    //播放witch杀死玩家后的音效
                    case "Witch": player.PlayOneShot(enemy_win_voice[1], 1f);break;
                    //播放ghost杀死玩家后的音效
                    case "Ghost": player.PlayOneShot(enemy_win_voice[2], 1f); break;
                    //播放pumpkin杀死玩家后的音效
                    case "Pumpkin":player.PlayOneShot(enemy_win_voice[3], 1f); break;
                    //播放devil杀死玩家后的音效
                    case "Devil":player.PlayOneShot(enemy_win_voice[4], 1f); break;
                    default:break;
                }
                //停止计时，避免重复播放
                if_enemy_time = false;
            }
        }
        if (IsDie){//玩家死亡
            //开始计时
            time += Time.fixedUnscaledDeltaTime;
            time2+= Time.fixedUnscaledDeltaTime;
            //停止背景音乐
            BackgroundMusic.Stop();
            if (time > 1){//1s后播放游戏结束音效
                //时间清零，为下次重新计时准备
                time = 0;
                if (num == 0){//
                    if_enemy_time = true;
                    player.PlayOneShot(GameOver,0.5f);
                    num++;
                }
            }
            //关闭Esc组件
            GetComponent<Esc>().enabled = false;
            //开启relive UI
            relive.SetActive(true);
            if (time2 > delayed_time){
                if (Input.anyKeyDown){//按任意键重新开始
                    time2 = 0;
                    SceneManager.LoadScene(1);
                    Time.timeScale = 1;
                }
            }
        }
    }
}
