using UnityEngine;

public class YemekMinigameManager : MonoBehaviour
{
    [Header("Zaman Ayarları")]
    public float verilenSure = 10f;
    private float kalanSure;
    public bool oyunAktif = false; 
    [Header("Oyun Aşamaları")]
    public int tavadakiMalzemeSayisi = 0;
    public float toplamSallamaMiktari = 0f;
    public float gerekenSallamaHedefi = 100f; 
    private MinigameSistemi minigameSistemi;

    private void Start()
    {
        minigameSistemi = GetComponent<MinigameSistemi>();
    }

    public void OyunuBaslat()
    {
        oyunAktif = true;
        kalanSure = verilenSure;
        tavadakiMalzemeSayisi = 0;
        toplamSallamaMiktari = 0f;
        Debug.Log("Yemek Minigame Başladı! Süreniz: " + verilenSure + " saniye.");
        
    }

    private void Update()
    {
        if (!oyunAktif) return;

        kalanSure -= Time.deltaTime;

        if (kalanSure <= 0)
        {
            // Süre bitti!
            oyunAktif = false;
            Debug.Log("Süre doldu, yemek yandı!");
            minigameSistemi.OyunuKaybet(); 
            
        }
        else
        {
            if (tavadakiMalzemeSayisi >= 3 && toplamSallamaMiktari >= gerekenSallamaHedefi)
            {
                oyunAktif = false;
                Debug.Log("Mükemmel yemek! Minigame kazanıldı.");
                minigameSistemi.OyunuKazan(); 
            }
        }
    }

    public void MalzemeEklendi()
    {
        tavadakiMalzemeSayisi++;
        Debug.Log("Tavaya malzeme eklendi. Toplam: " + tavadakiMalzemeSayisi);
    }

    public void TavayiSalla(float sallamaSiddeti)
    {
        if (tavadakiMalzemeSayisi >= 3) 
        {
            toplamSallamaMiktari += sallamaSiddeti;
        }
    }
}