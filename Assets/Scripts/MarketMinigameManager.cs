using UnityEngine;
using System.Collections.Generic;
using TMPro;

public enum AburCuburTipi { Cikolata, Biskuvi, Jelibon, Lolipop }

public class MarketMinigameManager : MonoBehaviour
{
    [Header("Zaman Ayarlari")]
    public float verilenSure = 12f;
    private float kalanSure;
    public bool oyunAktif = false;

    [Header("Sans Ayarlari")]
    public float basariSansBonusu = 15f;
    public float basarisizlikCezasi = -10f;
    
    [Header("Sistem Baglantilari")]
    public GameObject marketMinigameEkrani;
    public MonoBehaviour oyuncuHareketKodu;
    
    public TMP_Text listeYazisi; 

    private List<AburCuburTipi> alinacaklar = new List<AburCuburTipi>();
    private MarketEsyaSurukle[] tumEsyalar;
    private MinigameSistemi minigameSistemi;

    private void Start()
    {
        minigameSistemi = GetComponent<MinigameSistemi>();
        if (marketMinigameEkrani != null)
        {
            tumEsyalar = marketMinigameEkrani.GetComponentsInChildren<MarketEsyaSurukle>(true);
        }
    }

    public void OyunuBaslat()
    {
        oyunAktif = true;
        kalanSure = verilenSure;
        
        ListeyiHazirla();
        EsyalariSifirla();

        if (oyuncuHareketKodu != null) oyuncuHareketKodu.enabled = false;
        if (marketMinigameEkrani != null) marketMinigameEkrani.SetActive(true);
    }

    private void ListeyiHazirla()
    {
        alinacaklar.Clear();
        List<AburCuburTipi> tumTipler = new List<AburCuburTipi> 
        { 
            AburCuburTipi.Cikolata, 
            AburCuburTipi.Biskuvi, 
            AburCuburTipi.Jelibon, 
            AburCuburTipi.Lolipop 
        };
        
        int kacUrun = Random.Range(2, 5); 
        
        for (int i = 0; i < kacUrun; i++)
        {
            int rastgeleIndeks = Random.Range(0, tumTipler.Count);
            alinacaklar.Add(tumTipler[rastgeleIndeks]);
            tumTipler.RemoveAt(rastgeleIndeks);
        }

        YaziyiGuncelle();
    }

    private void YaziyiGuncelle()
    {
        if (listeYazisi == null) return;
        
        string metin = "Alinacaklar:\n";
        foreach (AburCuburTipi tip in alinacaklar)
        {
            metin += "- " + tip.ToString() + "\n";
        }
        listeYazisi.text = metin;
        
        Debug.Log("MARKET LISTESI OLUSTURULDU:\n" + metin); 
    }

    private void EsyalariSifirla()
    {
        if (tumEsyalar == null) return;
        
        foreach (MarketEsyaSurukle esya in tumEsyalar)
        {
            if (esya != null) esya.EsyayiSifirla();
        }
    }

    private void Update()
    {
        if (!oyunAktif) return;

        kalanSure -= Time.deltaTime;
        if (kalanSure <= 0)
        {
            MinigameBitir(false);
        }
    }

    public void SepeteEklendi(AburCuburTipi eklenenTip)
    {
        if (!oyunAktif) return;

        if (alinacaklar.Contains(eklenenTip))
        {
            alinacaklar.Remove(eklenenTip);
            YaziyiGuncelle();

            if (alinacaklar.Count == 0)
            {
                MinigameBitir(true);
            }
        }
        else
        {
            MinigameBitir(false);
        }
    }

    private void MinigameBitir(bool kazanildiMi)
    {
        oyunAktif = false;
        if (marketMinigameEkrani != null) marketMinigameEkrani.SetActive(false);
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
}