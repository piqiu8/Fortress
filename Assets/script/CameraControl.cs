using System.Collections.Generic;
using UnityEngine;

/*
 *���ܣ��ýű����ڽ�����������λ�ù̶������γ���ҹ̶��ӽǣ������ڵ������Ұ���ϰ���͸����
 *ʵ�ַ�����
 *�ӽǰ�ͨ���̶���Һ����֮�����������
 *�ڵ���͸�������򴴽��������飬һ����¼��ǰ�ڵ��һ����¼��һ���ڵ������ǰ�ڵ������͸�������ٶԱ��������Ƿ������ͬ���壬������˵������ƶ����������Ȼ�赲�����Ұ
 *������ʹ��͸��������������ͬ��������˵���Ѿ������赲��ң���ָ������
 *
 */

public class CameraControl : MonoBehaviour
{
    //����һ������������ȷ�����ߵķ���
    private Vector3 vector;
    //����һ��transform���
    private Transform player;
    //����Դ������͸�����ʣ������໥�滻
    public Material trans_material,source_material;
    //����������ײ��Ϣ�������ж������Ƿ��⵽�ڵ���
    RaycastHit hitinfo;
    //�������ڴ洢��ǰ�ڵ��������
    private List<GameObject> trans=new List<GameObject>();
    //�������ڴ洢��һ���ڵ��������
    private List<GameObject> last_trans=new List<GameObject>();
    //������ߵ���ײ��Ϣ
    private RaycastHit[] hits;
    //�涨����ִ֡��һ�εļ�����
    private int excute_num = 3;

    void cleartransparentObjects(){//����ʹ������͸����ԭ��ԭ��
        //�����������飬һһ���жԱ�
        for(int i = 0; i < last_trans.Count; ++i){
            for (int j = 0;j < trans.Count; ++j){
                //���ڷ�ֹ�������
                if (trans[j] != null){
                    if (last_trans[i] == trans[j]){
                        //���������д�����ͬ�����壬˵����������Ȼ���赲�����Ұ�����������Ϊ���
                        last_trans[i]=null;
                        //��Ϊ���ֻ����һ����ͬ����û��Ҫ�������±���
                        break;
                    }
                }
            }
        }
        //��ʼ��ԭδ�ڵ�����Ĳ���
        for(int i=0; i < last_trans.Count; ++i){
            //��Ϊnull��˵���������Ѿ�û���赲�����Ұ�ˣ����Ի�ԭ
            if (last_trans[i] != null) last_trans[i].transform.GetComponent<MeshRenderer>().material = source_material;
        }
        //������������ٿռ�����
        last_trans.Clear();
    }

    private void settrans(){//���ڽ��ڵ������Ұ�������Ϊ͸��
        //����һ�����ߣ��������λ������ҷ�������������߽Ӵ������壬���������ײ��Ϣ���ظ�hitinfo��������true
        if(Physics.Raycast(transform.position,vector,out hitinfo)){
            //����ǰ�ڵ������鸳����һ���ڵ�������
            for (int i = 0; i < trans.Count; ++i) last_trans.Add(trans[i]);
            //��յ�ǰ�ڵ������飬�Ա��ȡ��һ����ײ��Ϣ����
            trans.Clear();
            //��������ײ����ң�˵��û���ڵ����������ײ��Ϣ¼�룬ֱ�ӻ�ԭ����
            if (hitinfo.transform.tag != "Player"){
                //��ȡ���߳���100�ڵ�����ͼ��ΪDeault��������ײ��Ϣ
                hits = Physics.RaycastAll(transform.position, vector, 100, 1 << LayerMask.NameToLayer("Default"));
                //���б�������ʼ¼������
                for(int i = 0; i < hits.Length; i++){
                    var hit = hits[i];
                    //����ڵ����tagΪPlayer��˵������ң���Һ�������嵱Ȼ�����ڵ���ң���˿���ֹͣ¼����
                    if (hit.transform.tag == "Player") break;
                    else{
                        //������ң����޸������Ϊ͸������
                        hit.transform.GetComponent<MeshRenderer>().material=trans_material;
                        //��������
                        trans.Add(hit.transform.gameObject);
                    }
                }
                //��������Աȣ����л�ԭ����
                cleartransparentObjects();
            }
            else cleartransparentObjects();
        }
    }

    void Start(){
        //��ȡtagΪ"Player"����Ϸ�����transform���������ȡ��ҵ�transform���
        player = GameObject.FindWithTag("Player").transform;
        //��ȡ����������֮�����������ڽ����������ҽ�ɫ���а�
        vector = player.transform.position-transform.position;
    }

    void Update(){
        //ͨ���������㣬�������ʱʱ����Ұ󶨣������ӽ�������ƶ�
        transform.position = player.transform.position - vector;
        //ʵʱ����������Ƿ��������ڵ������Ұ
        if (Time.frameCount % excute_num == 0) settrans();//ÿ3ִ֡��һ�Σ���Сѹ��
    }
}
