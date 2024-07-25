using UnityEngine;

/// <summary>
/// ���г�ʼ�����
/// </summary>
public class CheckNull : MonoBehaviour
{
    /// <summary>
    /// ��ʼ��״̬
    /// </summary>
    public bool State = true;

    /// <summary>
    /// �Զ�������п�
    /// </summary>
    /// <typeparam name="T">��������</typeparam>
    /// <param name="obj">��Ӧ����</param>
    /// <param name="ErrorMessage">��Ϊ�յĶ�Ӧ������Ϣ</param>
    public void Check<T>(T obj, string ErrorMessage)
    {
        if (obj == null)
        {
            Debug.LogError(ErrorMessage);
            State = false;
        }
    }
}