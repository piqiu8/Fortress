using UnityEngine;

public class StreakerControl : MonoBehaviour
{
    private GameObject player;
    //创建Streaker与玩家之间的距离
    private float distance;

    void Start(){
        //获取玩家物体
        player = GameObject.FindWithTag("Player");
    }
    //远离玩家
    private void AwayPlayer(){
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < 10){//是否距离小于10m
            //是
            //获得一个与玩家视线相反的向量
            Vector3 lookDir = transform.position + (transform.position - player.transform.position);
            //使streaker看向这个向量
            transform.LookAt(lookDir);
            //向前方移动，速度为5m/s
            transform.Translate(Vector3.forward * 5f * Time.deltaTime);
        }
    }
    private void FixedUpdate(){
        AwayPlayer();
    }
}
