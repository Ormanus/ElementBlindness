using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScene : MonoBehaviour
{
   public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
