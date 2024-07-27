using UnityEngine;

/// <summary>
/// ����ȷ����Ҵ��״̬
/// </summary>
public class IfDie : MonoBehaviour
{
    /// <summary>
    /// ��Ҵ��״̬
    /// </summary>
    [HideInInspector] public bool IsDie = false;

    /// <summary>
    /// ��ײ�������������
    /// </summary>
    /// <param name="collision">��ײ������Ϣ</param>
    private void OnCollisionEnter(Collision collision)
    {
        //�������ײ���˵���
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.transform.parent != null)
            {
                //Ů��û����ײ�˺�
                if (collision.gameObject.transform.parent.name != "Witch") IsDie = true;
            }
            else IsDie = true;
        }
    }
}