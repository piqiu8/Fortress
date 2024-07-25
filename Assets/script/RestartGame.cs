//#define DEBUG_MODE

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 重新开始游戏
/// </summary>
public class RestartGame : MonoBehaviour
{
    /// <summary>
    /// 死亡界面UI
    /// </summary>
    public GameObject relive;

    /// <summary>
    /// 玩家存活状态
    /// </summary>
    private bool IsDie;

    /// <summary>
    /// 音频播放器
    /// </summary>
    private AudioSource player;

    /// <summary>
    /// 游戏结束音效
    /// </summary>
    public AudioClip GameOver;

    /// <summary>
    /// 背景音乐播放器
    /// </summary>
    private AudioSource BackgroundMusic;

    /// <summary>
    /// 判断值
    /// </summary>
    private int num = 0;

    /// <summary>
    /// 延时器
    /// </summary>
    private float time = 0, time2 = 0;

    /// <summary>
    /// 延时时间
    /// </summary>
    private float delayed_time = 10f;

    /// <summary>
    /// 杀死玩家的敌人名称
    /// </summary>
    [HideInInspector] public string Enemy_name;

    /// <summary>
    /// 在游戏结束音效开始播放后计时的计时器，用来让敌人杀死玩家后播放音效在游戏结束音效中间播放
    /// </summary>
    private float Enemy_time;

    /// <summary>
    /// 计时判断值
    /// </summary>
    private bool if_enemy_time = false;

    /// <summary>
    /// 敌人杀死玩家后播放音效
    /// </summary>
    public AudioClip[] enemy_win_voice;

    private void Start()
    {
        player = GetComponent<AudioSource>();
        BackgroundMusic = GameObject.Find("MusicTrigger").GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!player)
        {
            Debug.LogError("未获取到AudioSource");
            return;
        }
        if (!BackgroundMusic)
        {
            Debug.LogError("未获取到背景音乐播放器");
            return;
        }
#endif
    }

    private void Update()
    {
        IsDie = GetComponent<IfDie>().IsDie;
        //在播放游戏结束音效后开始计时
        if (if_enemy_time)
        {
            Enemy_time += Time.fixedUnscaledDeltaTime;
            //是否时间超过5s
            if (Enemy_time > 6)
            {
                switch (Enemy_name)
                {
                    //播放zombie杀死玩家后的音效
                    case "Zombie": player.PlayOneShot(enemy_win_voice[0], 1f); break;
                    //播放witch杀死玩家后的音效
                    case "Witch": player.PlayOneShot(enemy_win_voice[1], 1f); break;
                    //播放ghost杀死玩家后的音效
                    case "Ghost": player.PlayOneShot(enemy_win_voice[2], 1f); break;
                    //播放pumpkin杀死玩家后的音效
                    case "Pumpkin": player.PlayOneShot(enemy_win_voice[3], 1f); break;
                    //播放devil杀死玩家后的音效
                    case "Devil": player.PlayOneShot(enemy_win_voice[4], 1f); break;
                    default: break;
                }
                //停止计时，避免重复播放
                if_enemy_time = false;
            }
        }
        //玩家死亡
        if (IsDie)
        {
            //开始计时
            time += Time.fixedUnscaledDeltaTime;
            time2 += Time.fixedUnscaledDeltaTime;
            //停止背景音乐
            BackgroundMusic.Stop();
            //1s后播放游戏结束音效
            if (time > 1)
            {
                //时间清零，为下次重新计时准备
                time = 0;
                if (num == 0)
                {
                    if_enemy_time = true;
                    player.PlayOneShot(GameOver, 0.5f);
                    num++;
                }
            }
            //关闭Esc组件
            GetComponent<Esc>().enabled = false;
            //开启relive UI
            relive.SetActive(true);
            if (time2 > delayed_time)
            {
                //按任意键重新开始
                if (Input.anyKeyDown)
                {
                    time2 = 0;
                    SceneManager.LoadScene(1);
                    Time.timeScale = 1;
                }
            }
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// RestartGame初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool RestartGameDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(relive, "relive/死亡UI未赋值");
        checkNull.Check(player, "player/音频播放器未赋值");
        checkNull.Check(GameOver, "GameOver/游戏结束音效未赋值");
        checkNull.Check(BackgroundMusic, "BackgroundMusic/背景音乐播放器未赋值");
        checkNull.Check(enemy_win_voice, "enemy_win_voice/敌人杀死玩家后播放音效未赋值");
        return checkNull.State;
    }
#endif
}