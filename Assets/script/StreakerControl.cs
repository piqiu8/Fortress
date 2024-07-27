//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// Ұ�˿��ƽű�
/// </summary>
public class StreakerControl : MonoBehaviour
{
    /// <summary>
    /// �������
    /// </summary>
    private GameObject player;

    /// <summary>
    /// ����ҵľ���
    /// </summary>
    private float distance;

    private void Start()
    {
        //��ȡ�������
        player = GameObject.FindWithTag("Player");
#if DEBUG_MODE
        if (!player)
        {
            Debug.LogError("δ��ȡ���������");
            return;
        }
#endif
    }

    /// <summary>
    /// Զ�����
    /// </summary>
    private void AwayPlayer()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        //�Ƿ����С��10m
        if (distance < 10)
        {
            //���һ������������෴������
            Vector3 lookDir = transform.position + (transform.position - player.transform.position);
            //ʹstreaker�����������
            transform.LookAt(lookDir);
            //��ǰ���ƶ����ٶ�Ϊ5m/s
            transform.Translate(Vector3.forward * 5f * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        AwayPlayer();
    }
}