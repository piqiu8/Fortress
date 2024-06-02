using UnityEngine;

public class LightRotationTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Light;
    //��������
    private Quaternion source_quaternion =Quaternion.Euler(91f,-90f,0f), rotate_quaternion = Quaternion.Euler(269f, -90f, 0f);
    //�������ճ�ʼ�Ƕ�����ת��Ƕ�
    private bool x = true;
    //�����ж������Ǹ�ԭ������ת
    public float rotate_speed = 0.3f;
    //������ת�ٶ�
    private int rotate_tag=0;
    //���ձ�ǣ�����ȷ�ϸ�ԭ������ת

    void Start(){
        Light = GameObject.Find("Directional Light");
        //��ȡ����
    }
    void Update(){
        if (rotate_tag == 1) Light.transform.rotation = Quaternion.Lerp(Light.transform.rotation, rotate_quaternion, Time.deltaTime * rotate_speed);
        else Light.transform.rotation = Quaternion.Lerp(Light.transform.rotation, source_quaternion, Time.deltaTime * rotate_speed);
    }
    private void OnTriggerEnter(Collider other){//ȷ�ϱ��
        if (other.name == "KnightBody"){
            if (x){
                rotate_tag = 1;
                x = !x;
            }
            else{
                rotate_tag = 2;
                x = !x;
            }
        }
        //��ҵ�һ�δ������Ϊ1����������������
    }
}
