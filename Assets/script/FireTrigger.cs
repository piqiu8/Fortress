//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// �������
/// </summary>
public class FireTrigger : MonoBehaviour
{
    /// <summary>
    /// ������Ч��
    /// </summary>
    public GameObject Torch1;

    /// <summary>
    /// �Ҳ����Ч��
    /// </summary>
    public GameObject Torch2;

    /// <summary>
    /// ��Ƶ������
    /// </summary>
    private AudioSource player;

    /// <summary>
    /// ��������ʱ����Ч
    /// </summary>
    public AudioClip Fire;

    private void Start()
    {
        //��ȡ������Ƶ������
        player = GameObject.Find("torch_7").GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!player)
        {
            Debug.LogError("δ��ȡ��AudioSource���");
            return;
        }
        if (!FireTriggerDeBug())
        {
            Debug.LogError("��ʼ��ʧ��");
            return;
        }
#endif
    }

    /// <summary>
    /// ʹ�ô������������
    /// </summary>
    /// <param name="other">��ײ������Ϣ</param>
    private void OnTriggerEnter(Collider other)
    {
        //����������Ϊ���
        if (other.name == "KnightBody")
        {
            //�����һ��涼δ����
            if (!Torch1.activeInHierarchy && !Torch2.activeInHierarchy)
            {
                //���Ż��������Ч
                player.PlayOneShot(Fire, 0.1f);
                //���������漤��
                Torch1.SetActive(true);
                Torch2.SetActive(true);
            }
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// FireTrigger��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool FireTriggerDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(Torch1, "Torch1/������Ч��δ��ֵ");
        checkNull.Check(Torch2, "Torch2/�Ҳ����Ч��δ��ֵ");
        checkNull.Check(player, "player/��Ƶ������δ��ֵ");
        checkNull.Check(Fire, "Fire/��������ʱ����Чδ��ֵ");
        return checkNull.State;
    }
#endif
}