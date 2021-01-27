using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(LoadFirstScene), 1f);
    }

    void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
}
