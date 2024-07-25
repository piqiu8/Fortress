using UnityEngine;

/// <summary>
/// 进行初始化检查
/// </summary>
public class CheckNull : MonoBehaviour
{
    /// <summary>
    /// 初始化状态
    /// </summary>
    public bool State = true;

    /// <summary>
    /// 对对象进行判空
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="obj">对应对象</param>
    /// <param name="ErrorMessage">若为空的对应错误信息</param>
    public void Check<T>(T obj, string ErrorMessage)
    {
        if (obj == null)
        {
            Debug.LogError(ErrorMessage);
            State = false;
        }
    }
}