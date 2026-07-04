using UnityEngine;

public class OyuncakTetikleyici : MonoBehaviour
{
    public OyuncakMinigameManager minigameManager;
    private bool oyuncuAlandaMi = false;

    private void Update()
    {
        if (oyuncuAlandaMi && Input.GetKeyDown(KeyCode.E))
        {
            if (minigameManager != null && !minigameManager.oyunAktif)
            {
                minigameManager.OyunuBaslat();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuAlandaMi = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuAlandaMi = false;
        }
    }
}