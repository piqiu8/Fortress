//#define DEBUG_MODE

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 将摄像机与玩家位置固定，来形成玩家固定视角，并将遮挡玩家视野的障碍物透明化 |
/// 实现方法：视角绑定通过固定玩家和相机之间的向量即可,遮挡物透明处理则创建两个数组，一个记录当前遮挡物，一个记录上一次遮挡物，将当前遮挡物进行透明化，再对比两数组是否存
/// 在相同物体，若存在说明玩家移动后该物体仍然阻挡玩家视野仍需使其透明化，其他不相同的物体则说明已经不再阻挡玩家，则恢复其材质
/// </summary>
public class CameraControl : MonoBehaviour
{
    /// <summary>
    /// 射线方向
    /// </summary>
    private Vector3 vector;

    /// <summary>
    /// 玩家transform组件
    /// </summary>
    private Transform player;

    /// <summary>
    /// 透明材质
    /// </summary>
    public Material trans_material;

    /// <summary>
    /// 源材质
    /// </summary>
    public Material source_material;

    /// <summary>
    /// 碰撞检测射线
    /// </summary>
    private RaycastHit hitinfo;

    /// <summary>
    /// 当前遮挡物列表
    /// </summary>
    private List<GameObject> trans = new List<GameObject>();

    /// <summary>
    /// 上次遮挡物列表
    /// </summary>
    private List<GameObject> last_trans = new List<GameObject>();

    /// <summary>
    /// 射线碰撞信息数组
    /// </summary>
    private RaycastHit[] hits;

    /// <summary>
    /// 规定多少帧执行一次的计数器
    /// </summary>
    private int excute_num = 3;

    /// <summary>
    /// 将透明材质还原回源材质
    /// </summary>
    private void ClearTransparentObjects()
    {
        //遍历两个数组，一一进行对比
        for (int i = 0; i < last_trans.Count; ++i)
        {
            for (int j = 0; j < trans.Count; ++j)
            {
                //用于防止极端情况
                if (trans[j] != null)
                {
                    if (last_trans[i] == trans[j])
                    {
                        //若两数组中存在相同的物体，说明该物体仍然在阻挡玩家视野，将其清除作为标记
                        last_trans[i] = null;
                        //因为最多只会有一个相同，就没必要继续往下比了
                        break;
                    }
                }
            }
        }
        //开始还原未遮挡物体的材质
        for (int i = 0; i < last_trans.Count; ++i)
        {
            //不为null，说明该物体已经没有阻挡玩家视野了，可以还原
            if (last_trans[i] != null) last_trans[i].transform.GetComponent<MeshRenderer>().material = source_material;
        }
        //把数组清除减少空间消耗
        last_trans.Clear();
    }

    /// <summary>
    /// 将遮挡玩家视野的物体改为透明
    /// </summary>
    private void SetTrans()
    {
        //生成一条射线，从摄像机位置向玩家方向射出，若射线接触到物体，则将物体的碰撞信息返回给hitinfo，并返回true
        if (Physics.Raycast(transform.position, vector, out hitinfo))
        {
            //将当前遮挡物数组赋给上一次遮挡物数组
            for (int i = 0; i < trans.Count; ++i) last_trans.Add(trans[i]);
            //清空当前遮挡物数组，以便获取下一次碰撞信息数组
            trans.Clear();
            //若射线碰撞到玩家，说明没有遮挡物，不进行碰撞信息录入，直接还原材质
            if (hitinfo.transform.tag != "Player")
            {
                //获取射线长度100内的所有图层为Deault的物体碰撞信息
                hits = Physics.RaycastAll(transform.position, vector, 100, 1 << LayerMask.NameToLayer("Default"));
                //进行遍历，开始录入数组
                for (int i = 0; i < hits.Length; i++)
                {
                    var hit = hits[i];
                    //如果遮挡物的tag为Player，说明是玩家，玩家后面的物体当然不会遮挡玩家，因此可以停止录入了
                    if (hit.transform.tag == "Player") break;
                    else
                    {
                        //不是玩家，则修改其材质为透明材质
                        hit.transform.GetComponent<MeshRenderer>().material = trans_material;
                        //加入数组
                        trans.Add(hit.transform.gameObject);
                    }
                }
                //进行数组对比，进行还原操作
                ClearTransparentObjects();
            }
            else ClearTransparentObjects();
        }
    }

    private void Start()
    {
        //获取tag为"Player"的游戏物体的transform组件，即获取玩家的transform组件
        player = GameObject.FindWithTag("Player").transform;
        //获取玩家与摄像机之间的向量差，用于将摄像机和玩家角色进行绑定
        vector = player.transform.position - transform.position;
#if DEBUG_MODE
        if (!player)
        {
            Debug.LogError("未获取到transform组件");
            return;
        }
        if (!CameraControlDeBug())
        {
            Debug.LogError("初始化失败");
            return;
        }
#endif
    }

    private void Update()
    {
        //通过向量计算，将摄像机时时与玩家绑定，做到视角随玩家移动
        transform.position = player.transform.position - vector;
        //实时调用来检测是否有物体遮挡玩家视野
        if (Time.frameCount % excute_num == 0) SetTrans();//每3帧执行一次，减小压力
    }

#if DEBUG_MODE
    /// <summary>
    /// CameraControl初始化检查
    /// </summary>
    /// <returns>初始化状态</returns>
    private bool CameraControlDeBug()
    {
        CheckNull checkNull=gameObject.AddComponent<CheckNull>();
        checkNull.Check(player, "player/玩家transform组件未赋值");
        checkNull.Check(trans_material, "trans_material/透明材质未赋值");
        checkNull.Check(source_material, "source_material/源材质未赋值");
        return checkNull.State;
    }
#endif
}