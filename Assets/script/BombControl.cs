//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// ʵ�����Ͷ��ը�����ڹ������ˣ�ɱ�������򲥷ŵ���������Ч |
/// ʵ��˼·����ִ�иýű�ʱ�Զ�����һ��ը��������2s��������ͨ�����ը������˵ľ����Ƿ�С��3m���ж��Ƿ�ɱ�����ˣ�ɱ��������ը����ը����ԭλ��
/// ����һ������������Ч��ը����ը��Ч����������Ч��ԭ������ֱ���ò��ŵķ�ʽ������Ч���ڵ�����ը�����屻����ʱ�ᵼ����Чû���������
/// Ϊ���屻���ٱ�ͣ����
/// </summary>
public class BombControl : MonoBehaviour
{
    /// <summary>
    /// ը����ը��Ч
    /// </summary>
    public GameObject EffectPre = null;

    /// <summary>
    /// ��Ӧը����Ч������
    /// </summary>
    private AudioSource ClipPlayer = null;

    /// <summary>
    /// ը��Ͷ����Ч
    /// </summary>
    public AudioClip Throw = null;

    /// <summary>
    /// ը����ը��Ч
    /// </summary>
    public AudioClip Booming = null;

    /// <summary>
    /// ʵ��ը����ըɱ������
    /// </summary>
    private void Boom()
    {
        //����һ��ը����ը��Ч��ը��λ��
        GameObject effect = Instantiate(EffectPre, transform.position, transform.rotation);
        //2s�����ٱ�ը��Ч
        Destroy(effect, 2f);
        //��ȡ���е�������
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        //�����е��˽��б���
        foreach (GameObject enemy in enemys)
        {
            //�����˾�����ҵľ���С��3m
            if (Vector3.Distance(transform.position, enemy.transform.position) < 3f)
            {
                //��ɱ���ĵ����и����壬��ȡ�丸��������
                //��ͬ�ֵ��˵����Ʋ�����ͬ��������ͬ�ֵ��ˣ���˰�ͬ�ֵ��˷ŵ�һ���������Ա�ͳһʶ��
                if (enemy.transform.parent != null)
                {
                    //�Բ�ͬ���Ʋ�ȡ��ͬ��Ϊ
                    string name = enemy.transform.parent.gameObject.name;
                    switch (name)
                    {
                        //������Ϊzombie������״̬��Ϊ����
                        case "Zombie":
                            enemy.GetComponent<ZombieControl>().zombie_dead = true; break;
                        case "Witch":
                            enemy.GetComponent<WitchControl>().witch_dead = true; break;
                        case "Ghost":
                            enemy.GetComponent<GhostControl>().ghost_dead = true; break;
                        case "Pumpkin":
                            enemy.GetComponent<PumpkinControl>().pumpkin_dead = true; break;
                        case "Devil":
                            enemy.GetComponent<DevilControl>().devil_dead = true; break;
                        default: break;
                    }
                }
                //�޸����壬˵����Witch���ɵ�Zombie
                else enemy.GetComponent<Witch_Zombie_Control>().witch_zombie_dead = true;
                //�Ե���ʩ��һ��˲���������ģ��ը��Ч����������ը��ָ�����
                if (enemy.GetComponent<Rigidbody>() != null) enemy.GetComponent<Rigidbody>().AddForce((enemy.transform.position - transform.position) * 5f, ForceMode.Impulse);
            }
        }
        GameObject maincamera = GameObject.Find("Main Camera");
#if DEBUG_MODE
        if (!maincamera)
        {
            Debug.LogError("δ��ȡ��Main Camera");
            return;
        }
#endif
        //����һ����ը��Ч�������λ�ã�����Ϊ0.5��
        AudioSource.PlayClipAtPoint(Booming, maincamera.transform.position, 0.5f);
        //����ը������
        Destroy(gameObject);
#if DEBUG_MODE
        Debug.Log("ը����ը");
#endif
    }

    private void Start()
    {
        ClipPlayer = GetComponent<AudioSource>();
#if DEBUG_MODE
        if (!ClipPlayer)
        {
            Debug.LogError("δ��ȡ��AudioSource���");
            return;
        }
        //��ʼ�����
        if (!BombDeBug())
        {
            Debug.LogError("��ʼ��ʧ��");
            return;
        }
#endif
        //���ŷ�ը����Ч������Ϊ0.1��
        ClipPlayer.PlayOneShot(Throw, 0.3f);
        //2s��ִ��Boom����
        Invoke("Boom", 2f);
    }

#if DEBUG_MODE
    /// <summary>
    /// BombControl��ʼ�����
    /// </summary>
    /// <returns>��ʼ��״̬</returns>
    private bool BombDeBug()
    {
        CheckNull checkNull = gameObject.AddComponent<CheckNull>();
        checkNull.Check(EffectPre, "EffectPre/��ըЧ��δ��ֵ");
        checkNull.Check(ClipPlayer, "ClipPlayer/��Ч������δ��ֵ");
        checkNull.Check(Throw, "Throw/ը��Ͷ����Чδ��ֵ");
        checkNull.Check(Booming, "Booming/ը����ը��Чδ��ֵ");
        return checkNull.State;
    }
#endif
}