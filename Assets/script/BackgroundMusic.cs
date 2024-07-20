//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// 实现背景音乐播放
/// </summary>
public class BackgroundMusic : MonoBehaviour
{
    /// <summary>
    /// 对应背景音乐
    /// </summary>
    public AudioClip Music;

    /// <summary>
    /// 对应背景音乐播放器
    /// </summary>
    private AudioSource BackGroundMusicPlayer;

    /// <summary>
    /// 背景音乐播放状态，默认为false并在Inspector隐藏
    /// </summary>
    [HideInInspector] public bool IfBackgroundMusic = false;

    private void Start()
    {
        //获取播放器组件
        BackGroundMusicPlayer = GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!BackGroundMusicPlayer)
        {
            Debug.LogError("未获取到AudioSource组件");
            return;
        }
        if (!BackGroundMusicDeBug())
        {
            Debug.LogError("初始化失败");
            return;
        }
#endif
        //设置播放音频为Music
        BackGroundMusicPlayer.clip = Music;
        //设置循环播放
        BackGroundMusicPlayer.loop = true;
        //设置音量为0.2倍
        BackGroundMusicPlayer.volume = 0.2f;
    }

    /// <summary>
    /// 通过触发器播放背景音乐
    /// </summary>
    /// <param name="other">碰撞物体信息</param>
    private void OnTriggerEnter(Collider other)
    {
        //若碰撞物体名称为KnightBody
        if (other.name == "KnightBody")
        {
            //若背景音乐为关闭状态
            if (!BackGroundMusicPlayer.isPlaying)
            {
                //将背景音乐状态改为开启，播放背景音乐
                IfBackgroundMusic = true;
                BackGroundMusicPlayer.Play();
#if DEBUG_MODE
                Debug.Log("游戏背景音乐播放状态为"+IfBackgroundMusic);
#endif
            }
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// BackgroundMusic初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool BackGroundMusicDeBug()
    {
        CheckNull checkNull = gameObject.AddComponent<CheckNull>();
        checkNull.Check(Music, "Music/背景音乐未初始化");
        checkNull.Check(BackGroundMusicPlayer, "BackGroundMusicPlayer/背景音乐播放器未初始化");
        return checkNull.State;
    }
#endif
}