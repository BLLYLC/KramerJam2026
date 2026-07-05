using UnityEngine;

public class BezTetikleyici : MonoBehaviour
{
    public RastgeleMinigame rmg;

    public BezMinigameManager minigameManager;
    private bool oyuncuAlandaMi = false;

    public KarakterDovus oyuncununDovusScripti;

    private void Update()
    {
        if (oyuncuAlandaMi && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E TUSUNA BASILDI! Tetikleyici calisti.");
            
            if (minigameManager != null)
            {
                if (!minigameManager.oyunAktif)
                {
                    Debug.Log("Sistem musait, oyun baslatiliyor...");
                    minigameManager.OyunuBaslat();
                    oyuncununDovusScripti.saldirabilirMi = false; 
                }
                else
                {
                    Debug.Log("HATA: Oyun zaten aktif gozukuyor, baslatilamadi!");
                }
            }
            else
            {
                Debug.Log("HATA: Minigame Manager boslugu doldurulmamis!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Tetikleyiciye bir obje girdi: " + other.gameObject.name + " | Etiketi (Tag): " + other.tag);
        
        if (other.CompareTag("Player"))
        {
            oyuncuAlandaMi = true;
            Debug.Log("Oyuncu alana basariyla girdi! E'ye basilmasi bekleniyor.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuAlandaMi = false;
            Debug.Log("Oyuncu alandan cikti.");
        }
    }
}