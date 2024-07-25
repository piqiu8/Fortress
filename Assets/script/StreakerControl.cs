//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// 野人控制脚本
/// </summary>
public class StreakerControl : MonoBehaviour
{
    /// <summary>
    /// 玩家物体
    /// </summary>
    private GameObject player;

    /// <summary>
    /// 距玩家的距离
    /// </summary>
    private float distance;

    private void Start()
    {
        //获取玩家物体
        player = GameObject.FindWithTag("Player");
#if DEBUG_MODE
        if (!player)
        {
            Debug.LogError("未获取到玩家物体");
            return;
        }
#endif
    }

    /// <summary>
    /// 远离玩家
    /// </summary>
    private void AwayPlayer()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        //是否距离小于10m
        if (distance < 10)
        {
            //获得一个与玩家视线相反的向量
            Vector3 lookDir = transform.position + (transform.position - player.transform.position);
            //使streaker看向这个向量
            transform.LookAt(lookDir);
            //向前方移动，速度为5m/s
            transform.Translate(Vector3.forward * 5f * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        AwayPlayer();
    }
}