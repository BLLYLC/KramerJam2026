using UnityEngine;

public class OyuncakTetikleyici : MonoBehaviour
{
    public RastgeleMinigame rmg;

    public OyuncakMinigameManager minigameManager;
    private bool oyuncuAlandaMi = false;

    public KarakterDovus oyuncununDovusScripti;

    private void Update()
    {
        if (oyuncuAlandaMi && Input.GetKeyDown(KeyCode.E) && rmg.oyuncak)
        {
            if (minigameManager != null && !minigameManager.oyunAktif)
            {
                minigameManager.OyunuBaslat();
                GetComponent<SpriteRenderer>().color = Color.white;
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