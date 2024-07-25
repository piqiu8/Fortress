//#define DEBUG_MODE

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 实现游戏暂停
/// </summary>
public class Esc : MonoBehaviour
{
    /// <summary>
    /// 暂停时显现的UI
    /// </summary>
    public GameObject ESC;

    /// <summary>
    /// 游戏运行状态，用于确认游戏是否在运行
    /// </summary>
    private bool IsGameRunning = true;

    /// <summary>
    /// 音乐触发器
    /// </summary>
    private GameObject MusicTrigger;

    /// <summary>
    /// 音频播放器
    /// </summary>
    private AudioSource player;

    /// <summary>
    /// 背景音乐播放状态，用于确认背景音乐是否开启
    /// </summary>
    private bool IfBackgroundMusic;

    /// <summary>
    /// 音频播放器2号
    /// </summary>
    private AudioSource player2;

    /// <summary>
    /// 暂停播放的音效
    /// </summary>
    public AudioClip clip_01;

    private void Start()
    {
        //获取音乐触发器
        MusicTrigger = GameObject.Find("MusicTrigger");
        //获取音频播放器
        player = MusicTrigger.GetComponent<AudioSource>();
        //获取2号音频播放器
        player2 = GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!MusicTrigger)
        {
            Debug.LogError("未获取到音乐触发器");
            return;
        }
        if (!player)
        {
            Debug.LogError("未获取到AudioSource组件");
            return;
        }
        if(!player2)
        {
            Debug.LogError("未获取到AudioSource组件2号");
            return;
        }
        if (!EscDeBug())
        {
            Debug.LogError("初始化失败");
            return;
        }
#endif
    }

    private void Update()
    {
        //随时获取背景音乐的播放状态
        IfBackgroundMusic = MusicTrigger.GetComponent<BackgroundMusic>().IfBackgroundMusic;
        //若按下空格键
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //播放暂停界面音效
            player2.PlayOneShot(clip_01);
            //若游戏在运行
            if (IsGameRunning)
            {
                //若背景音乐正在播放，则暂停背景音乐
                if (IfBackgroundMusic) player.Pause();
                Time.timeScale = 0;
                //将ESC UI的激活状态设置为true
                ESC.SetActive(true);
            }
            //若游戏未运行
            else
            {
                //将背景音乐恢复播放
                if (IfBackgroundMusic) player.UnPause();
                //将游戏继续运行
                Time.timeScale = 1;
                //将ESC 激活状态设置为false
                ESC.SetActive(false);
            }
            //将游戏运行状态置反
            IsGameRunning = !IsGameRunning;
        }
        //若Esc为激活状态
        if (ESC.activeInHierarchy)
        {
            //若按下回车键
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //同步加载0号场景
                SceneManager.LoadScene(0);
                Time.timeScale = 1;
            }
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// Esc初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool EscDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(ESC, "ESC/暂停时显示的UI未赋值");
        checkNull.Check(MusicTrigger, "MusicTrigger/音乐触发器未赋值");
        checkNull.Check(player, "player/音频播放器未赋值");
        checkNull.Check(player2, "player2/音频播放器2号未赋值");
        checkNull.Check(clip_01, "clip_01/暂停播放的音效未赋值");
        return checkNull.State;
    }
#endif
}