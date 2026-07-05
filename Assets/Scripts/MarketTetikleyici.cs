using UnityEngine;

public class MarketTetikleyici : MonoBehaviour
{
    public MarketMinigameManager minigameManager;
    private bool oyuncuAlandaMi = false;

    public KarakterDovus oyuncununDovusScripti;

    private void Update()
    {
        if (oyuncuAlandaMi && Input.GetKeyDown(KeyCode.E))
        {
            if (minigameManager != null && !minigameManager.oyunAktif)
            {
                minigameManager.OyunuBaslat();
                oyuncununDovusScripti.saldirabilirMi = false;
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