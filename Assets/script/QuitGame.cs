using UnityEngine;

/// <summary>
/// �˳���Ϸ
/// </summary>
public class QuitGame : MonoBehaviour
{
    public void EndingGame()
    {
        //#if UNITY_EDITOR��#endif�������ô������Ϸ��Ҫ�������еĴ��룬UnityEditor����unity��ʹ�õģ���������ò��˵ģ��ᵼ�´������
#if UNITY_EDITOR
        //��������״̬Ϊfalse���˳�unity����״̬
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        //�˳���Ϸ
        Application.Quit();
    }
}