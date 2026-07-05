using UnityEngine;

public class MutfakTetikleyici : MonoBehaviour
{
    public RastgeleMinigame rmg;

    [Header("Bağlanacak Objeler")]
    public GameObject yemekMinigameEkrani; 
    public YemekMinigameManager minigameManager;

    public KarakterDovus oyuncununDovusScripti;

    private bool oyuncuIceride = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuIceride = true;
            Debug.Log("Oyuncu mutfağa geldi, E'ye basabilir.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuIceride = false;
        }
    }

    private void Update()
    {
        if (oyuncuIceride && Input.GetKeyDown(KeyCode.E) && !yemekMinigameEkrani.activeSelf && rmg.yemek)
        {
            yemekMinigameEkrani.SetActive(true);
            
            minigameManager.OyunuBaslat();
            GetComponent<SpriteRenderer>().color = Color.white;

            oyuncununDovusScripti.saldirabilirMi = false;
        }
    }
}