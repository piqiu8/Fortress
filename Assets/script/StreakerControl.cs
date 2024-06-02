using UnityEngine;

public class StreakerControl : MonoBehaviour
{
    private GameObject player;
    //����Streaker�����֮��ľ���
    private float distance;

    void Start(){
        //��ȡ�������
        player = GameObject.FindWithTag("Player");
    }
    //Զ�����
    private void AwayPlayer(){
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < 10){//�Ƿ����С��10m
            //��
            //���һ������������෴������
            Vector3 lookDir = transform.position + (transform.position - player.transform.position);
            //ʹstreaker�����������
            transform.LookAt(lookDir);
            //��ǰ���ƶ����ٶ�Ϊ5m/s
            transform.Translate(Vector3.forward * 5f * Time.deltaTime);
        }
    }
    private void FixedUpdate(){
        AwayPlayer();
    }
}
