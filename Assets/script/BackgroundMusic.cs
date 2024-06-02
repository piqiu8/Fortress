using UnityEngine;

/*
 *功能：实现背景音乐的播放
 *实现方法：通过触发器查看玩家是否达到指定目的地，到达则播放背景音乐
 */

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip Music;
    //创建背景音乐
    private AudioSource player;
    //创建音频播放器
    [HideInInspector]public bool IfBackgroundMusic = false;
    //用于判断背景音乐是否开启
    void Start(){
        player = GetComponent<AudioSource>();
        //获取播放器组件
        player.clip = Music;
        //设置播放音频为Music
        player.loop = true;
        //设置循环播放
        player.volume = 0.2f;
        //设置音量为0.2倍
    }

    private void OnTriggerEnter(Collider other){//当玩家触发触发器时
        if (other.name == "KnightBody"){
            //若碰撞物体名称为KnightBody
            if (!player.isPlaying){
                //若背景音乐为关闭状态
                IfBackgroundMusic = true;
                //将背景音乐状态改为开启，播放背景音乐
                player.Play();
            }
        }
    }
}
