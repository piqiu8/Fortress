using UnityEngine;

/// <summary>
/// 用于确认玩家存活状态
/// </summary>
public class IfDie : MonoBehaviour
{
    /// <summary>
    /// 玩家存活状态
    /// </summary>
    [HideInInspector] public bool IsDie = false;

    /// <summary>
    /// 碰撞敌人则玩家死亡
    /// </summary>
    /// <param name="collision">碰撞物体信息</param>
    private void OnCollisionEnter(Collision collision)
    {
        //若玩家碰撞到了敌人
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.transform.parent != null)
            {
                //女巫没有碰撞伤害
                if (collision.gameObject.transform.parent.name != "Witch") IsDie = true;
            }
            else IsDie = true;
        }
    }
}