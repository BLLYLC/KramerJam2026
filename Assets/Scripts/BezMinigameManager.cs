using UnityEngine;

public class BezMinigameManager : MonoBehaviour
{
    [Header("Sans Ayarlari")]
    public float basariSansBonusu = 20f;
    public float basarisizlikCezasi = -15f;

    [Header("Sistem Ayarlari")]
    public GameObject minigameEkrani;
    public MonoBehaviour oyuncuHareketKodu;
    public BezFirlatma bezKodu;

    public bool oyunAktif = false;

    public void OyunuBaslat()
    {
        oyunAktif = true;
        
        if (oyuncuHareketKodu != null) oyuncuHareketKodu.enabled = false;
        if (minigameEkrani != null) minigameEkrani.SetActive(true);
        
        bezKodu.Sifirla();
    }

    public void MinigameBitir(bool kazanildiMi)
    {
        if (!oyunAktif) return;
        
        oyunAktif = false;

        if (minigameEkrani != null) minigameEkrani.SetActive(false);
        if (oyuncuHareketKodu != null) oyuncuHareketKodu.enabled = true;

        if (BebekMekanigi.instance != null)
        {
            if (kazanildiMi)
            {
                BebekMekanigi.instance.OlasiligiDegistir(basariSansBonusu);
            }
            else
            {
                BebekMekanigi.instance.OlasiligiDegistir(basarisizlikCezasi);
            }
        }
    }
}