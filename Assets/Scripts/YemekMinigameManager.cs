using UnityEngine;

public class YemekMinigameManager : MonoBehaviour
{
    [Header("Zaman Ayarlari")]
    public float verilenSure = 10f;
    private float kalanSure;
    public bool oyunAktif = false; 

    [Header("Sans Ayarlari")]
    public float basariSansBonusu = 15f;
    public float basarisizlikCezasi = -10f;
    
    [Header("Oyun Asamalari")]
    public int tavadakiMalzemeSayisi = 0;
    public int toplamSallamaMiktari = 0;
    public int gerekenSallamaHedefi = 15; 

    [Header("Sistem Baglantilari")]
    public GameObject yemekMinigameEkrani; 
    public MonoBehaviour oyuncuHareketKodu; 
    
    private MalzemeSurukle[] malzemeler;
    private MinigameSistemi minigameSistemi;

    private void Start()
    {
        minigameSistemi = GetComponent<MinigameSistemi>();
        if (yemekMinigameEkrani != null)
        {
            malzemeler = yemekMinigameEkrani.GetComponentsInChildren<MalzemeSurukle>(true);
        }
    }

    public void OyunuBaslat()
    {
        oyunAktif = true;
        kalanSure = verilenSure;
        tavadakiMalzemeSayisi = 0;
        toplamSallamaMiktari = 0;
        
        if (yemekMinigameEkrani != null)
        {
            malzemeler = yemekMinigameEkrani.GetComponentsInChildren<MalzemeSurukle>(true);
        }

        if (malzemeler != null)
        {
            for (int i = 0; i < malzemeler.Length; i++)
            {
                if (malzemeler[i] != null)
                {
                    malzemeler[i].MalzemeyiSifirla();
                }
            }
        }

        if (oyuncuHareketKodu != null) oyuncuHareketKodu.enabled = false;
    }

    private void Update()
    {
        if (!oyunAktif) return;

        kalanSure -= Time.deltaTime;

        if (kalanSure <= 0)
        {
            MinigameBitir(false); 
        }
        else if (tavadakiMalzemeSayisi >= 3 && toplamSallamaMiktari >= gerekenSallamaHedefi)
        {
            MinigameBitir(true); 
        }
    }

    private void MinigameBitir(bool kazanildiMi)
    {
        oyunAktif = false;
        yemekMinigameEkrani.SetActive(false); 

        if (oyuncuHareketKodu != null) oyuncuHareketKodu.enabled = true;

        if (kazanildiMi)
        {
            minigameSistemi.OyunuKazan(basariSansBonusu);
        }
        else
        {
            minigameSistemi.OyunuKaybet(basarisizlikCezasi);
        }
    }

    public void MalzemeEklendi()
    {
        tavadakiMalzemeSayisi++;
    }

    public void TavayiSalla()
    {
        if (tavadakiMalzemeSayisi >= 3) 
        {
            toplamSallamaMiktari++;
            Debug.Log("Sallama Sayisi: " + toplamSallamaMiktari);
        }
    }
}