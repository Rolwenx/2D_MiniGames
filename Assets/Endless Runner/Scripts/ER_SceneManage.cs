using UnityEngine;
using UnityEngine.SceneManagement;

public class ER_SceneManage : MonoBehaviour
{
   public void NavigateScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
