using UnityEngine;
using UnityEngine.SceneManagement;

/*
 *这是游戏暂停脚本，
 */
public class Esc : MonoBehaviour
{
    //创建esc UI游戏物体
    public GameObject ESC;
    //创建一个状态值，用于确定游戏是否在运行，默认设置为true
    private bool IsGameRunning = true;
    //创建一个音乐触发器
    private GameObject MusicTrigger;
    //创建一个音频播放器
    private AudioSource player;
    //创建一个状态值，用于确定背景音乐是否开启
    private bool IfBackgroundMusic;
    //创建一个2号音频播放器
    private AudioSource player2;
    //创建一个暂停界面音效
    public AudioClip clip_01;

    private void Start(){
        //获取音乐触发器
        MusicTrigger=GameObject.Find("MusicTrigger");
        //获取音频播放器
        player = MusicTrigger.GetComponent<AudioSource>();
        //获取2号音频播放器
        player2 = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update(){
        //随时获取背景音乐的播放状态
        IfBackgroundMusic = MusicTrigger.GetComponent<BackgroundMusic>().IfBackgroundMusic;
        //是否按下空格键
        if (Input.GetKeyDown(KeyCode.Space)){
            //是
            //播放暂停界面音效
            player2.PlayOneShot(clip_01);
            //游戏是否在运行
            if(IsGameRunning){//游戏运行中
                if(IfBackgroundMusic) player.Pause();
                //若背景音乐正在播放，则暂停背景音乐
                Time.timeScale = 0;
                ESC.SetActive(true);
                //将ESC UI的激活状态设置为true
            }
            else{//游戏未运行
                if(IfBackgroundMusic) player.UnPause();
                //将背景音乐恢复播放
                Time.timeScale = 1;
                //将游戏继续运行
                ESC.SetActive(false);
                //将ESC 激活状态设置为false
            }
            IsGameRunning = !IsGameRunning;
            //将游戏运行状态置反
        }
        if (ESC.activeInHierarchy){//若Esc为激活状态
            if (Input.GetKeyDown(KeyCode.Return)){//若按下回车键
                SceneManager.LoadScene(0);
                //同步加载0号场景
                Time.timeScale = 1;
                //让游戏继续运行
            }
        }
    }
}
