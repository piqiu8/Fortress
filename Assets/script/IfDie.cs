using UnityEngine;

public class IfDie : MonoBehaviour
{
    [HideInInspector] public bool IsDie = false;
    //默认玩家未死亡
    private void OnCollisionEnter(Collision collision){//判定玩家是否死亡
        if(collision.gameObject.tag=="Enemy"){//若玩家碰撞到了敌人
            if(collision.gameObject.transform.parent != null){
                if(collision.gameObject.transform.parent.name != "Witch") IsDie = true;
            }
            else IsDie = true;
        }
    }
}
