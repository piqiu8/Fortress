//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// ʵ����ҹת��
/// </summary>
public class LightRotationTrigger : MonoBehaviour
{
    /// <summary>
    /// ȫ����Ϸ����
    /// </summary>
    private GameObject Light;

    /// <summary>
    /// ��ʼ���սǶ�
    /// </summary>
    private Quaternion source_quaternion = Quaternion.Euler(91f, -90f, 0f);

    /// <summary>
    /// ��ת����սǶ�
    /// </summary>
    private Quaternion rotate_quaternion = Quaternion.Euler(269f, -90f, 0f);

    /// <summary>
    /// ����״̬�������ж�����ʱ��ԭ������ת
    /// </summary>
    private bool x = true;

    /// <summary>
    /// ������ת�ٶȣ�Ĭ��Ϊ0.3
    /// </summary>
    public float rotate_speed = 0.3f;

    /// <summary>
    /// ���ձ�ǣ�����ȷ�ϸ�ԭ������ת
    /// </summary>
    private int rotate_tag = 0;

    private void Start()
    {
        Light = GameObject.Find("Directional Light");
#if DEBUG_MODE
        if (!Light)
        {
            Debug.LogError("δ��ȡ������");
            return;
        }
#endif
    }

    private void Update()
    {
        if (rotate_tag == 1) Light.transform.rotation = Quaternion.Lerp(Light.transform.rotation, rotate_quaternion, Time.deltaTime * rotate_speed);
        else Light.transform.rotation = Quaternion.Lerp(Light.transform.rotation, source_quaternion, Time.deltaTime * rotate_speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        //��ҵ�һ�δ������Ϊ1����������������
        if (other.name == "KnightBody")
        {
            if (x)
            {
                rotate_tag = 1;
                x = !x;
            }
            else
            {
                rotate_tag = 2;
                x = !x;
            }
        }
    }
}