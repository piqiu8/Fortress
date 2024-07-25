//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// Ϩ�����ϵĻ���
/// </summary>
public class OffFireTrigger : MonoBehaviour
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
    /// ����Ϩ����Ч
    /// </summary>
    public AudioClip OffFire;

    private void Start()
    {
        player = GameObject.Find("torch_7").GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!player)
        {
            Debug.LogError("δ��ȡ��AudioSource���");
            return;
        }
        if (!OffFireTriggerDeBug())
        {
            Debug.LogError("��ʼ��ʧ��");
            return;
        }
#endif
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "KnightBody")
        {
            if (Torch1.activeInHierarchy && Torch2.activeInHierarchy)
            {
                player.PlayOneShot(OffFire, 0.1f);
                Torch1.SetActive(false);
                Torch2.SetActive(false);
            }
        }
    }

#if DEBUG_MODE
    /// <summary>
    /// OffFireTrigger��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool OffFireTriggerDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(Torch1, "Torch1/������Ч��δ��ֵ");
        checkNull.Check(Torch2, "Torch2/�Ҳ����Ч��δ��ֵ");
        checkNull.Check(player, "player/��Ƶ������δ��ֵ");
        checkNull.Check(OffFire, "OffFIre/����Ϩ��Ч��δ��ֵ");
        return checkNull.State;
    }
#endif
}