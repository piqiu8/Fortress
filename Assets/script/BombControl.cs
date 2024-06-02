using UnityEngine;
/*
 *���ܣ�ʵ�����Ͷ��ը�����ڹ������ˣ�ɱ�������򲥷ŵ���������Ч
 *ʵ�ַ�ʽ����ִ�иýű�ʱ�Զ�����һ��ը��������2s��������ͨ�����ը������˵ľ����Ƿ�С��3m���ж��Ƿ�ɱ�����ˣ�ɱ��������ը����ը����ԭλ��
 *         ����һ������������Ч��ը����ը��Ч����������Ч��ԭ������ֱ���ò��ŵķ�ʽ������Ч���ڵ�����ը�����屻����ʱ�ᵼ����Чû���������
 *         Ϊ���屻���ٱ�ͣ����
 */

public class BombControl : MonoBehaviour
{
    public GameObject EffectPre;
    //����һ����ը��Ч
    private AudioSource player;
    //����һ����Ƶ������
    public AudioClip Throw;
    //����һ����ը����Ч
    public AudioClip Booming;
    //����һ��ը����ը��Ч

    void Boom(){//ը����ը����
        GameObject effect=Instantiate(EffectPre,transform.position,transform.rotation);
        //����һ��ը����ը��Ч��ը��λ��
        Destroy(effect, 2f);
        //2s�����ٱ�ը��Ч
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        //��ȡ���е�������
        foreach (GameObject enemy in enemys){
            //�����е��˽��б���
            if (Vector3.Distance(transform.position, enemy.transform.position) < 3f){
                //�����˾�����ҵľ���С��3m
                if (enemy.transform.parent != null){
                    //��ɱ���ĵ����и����壬��ȡ�丸��������
                    //��ͬ�ֵ��˵����Ʋ�����ͬ��������ͬ�ֵ��ˣ���˰�ͬ�ֵ��˷ŵ�һ���������Ա�ͳһʶ��
                    string name = enemy.transform.parent.gameObject.name;
                    //�Բ�ͬ���Ʋ�ȡ��ͬ��Ϊ
                    switch (name){
                        case "Zombie":
                            //������Ϊzombie������״̬��Ϊ����
                            enemy.GetComponent<ZombieControl>().zombie_dead=true;break;
                        case "Witch":
                            enemy.GetComponent<WitchControl>().witch_dead = true; break;
                        case "Ghost":
                            enemy.GetComponent<GhostControl>().ghost_dead = true; break;
                        case "Pumpkin":
                            enemy.GetComponent<PumpkinControl>().pumpkin_dead=true;break;
                        case "Devil":
                            enemy.GetComponent<DevilControl>().devil_dead = true; break;
                        default: break;
                    }
                }
                else enemy.GetComponent<Witch_Zombie_Control>().witch_zombie_dead = true;
                //�޸����壬˵����Witch���ɵ�Zombie
                if (enemy.GetComponent<Rigidbody>()!=null) enemy.GetComponent<Rigidbody>().AddForce((enemy.transform.position-transform.position)*5f, ForceMode.Impulse);
                //�Ե���ʩ��һ��˲���������ģ��ը��Ч����������ը��ָ�����
            }
        }
        GameObject maincamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(Booming, maincamera.transform.position, 0.5f);
        //����һ����ը��Ч�������λ�ã�����Ϊ1��
        Destroy(gameObject);
        //����ը������
    }
    void Start(){
        player = GetComponent<AudioSource>();
        //��ȡ��Ƶ���������
        player.PlayOneShot(Throw, 0.3f);
        //���ŷ�ը����Ч������Ϊ0.1��
        Invoke("Boom", 2f);
        //2s��ִ��Boom����
    }
}
