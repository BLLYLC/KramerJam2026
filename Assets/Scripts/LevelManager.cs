using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
  
    public void LoadAnaOyun()
    {    
        SceneManager.LoadScene(1);      
    }
    public void OyundanCýk()
    {
      Application.Quit();
    }
    public void LoadOyunSonu()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadOyunKazanma()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadAnaMenu()
    {
        SceneManager.LoadScene(0);
    }
}
