using UnityEngine;

public class IfDie : MonoBehaviour
{
    [HideInInspector] public bool IsDie = false;
    //Ĭ�����δ����
    private void OnCollisionEnter(Collision collision){//�ж�����Ƿ�����
        if(collision.gameObject.tag=="Enemy"){//�������ײ���˵���
            if(collision.gameObject.transform.parent != null){
                if(collision.gameObject.transform.parent.name != "Witch") IsDie = true;
            }
            else IsDie = true;
        }
    }
}
