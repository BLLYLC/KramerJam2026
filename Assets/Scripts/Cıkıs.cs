using UnityEngine;
using UnityEngine.UI;

public class Cýkýs : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // Find the LevelManager dynamically in case it was reloaded
        LevelManager manager = FindFirstObjectByType<LevelManager>();

        if (manager != null)
        {
            Application.Quit();
        }
    }
}