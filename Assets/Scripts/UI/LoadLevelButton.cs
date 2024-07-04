using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelButton : MonoBehaviour
{
    [SerializeField] string _levelName;

    public void Load()
    {
        SceneManager.LoadScene(_levelName);
    }

    public void ExitApp()
    {
        Application.Quit();
        print("Exit");
    }
}