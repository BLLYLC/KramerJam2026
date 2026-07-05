using UnityEngine;
using System.Collections;

public class OyuncakMinigameManager : MonoBehaviour
{
    [Header("Sans Ayarlari")]
    public float basariSansBonusu = 15f;
    public float basarisizlikCezasi = -10f;

    [Header("Ortak Sistem Baglantisi")]
    public MinigameSistemi genelSistem;

    [Header("Sistem Ayarlari")]
    public GameObject minigameEkrani;
    public MonoBehaviour oyuncuHareketKodu;
    public DusenOyuncak[] oyuncaklar;
    public float firlatmaAraligi = 1.2f;
    public float minX = -6f;
    public float maxX = 6f;

    [Header("Durum Kontrolü")]
    public bool oyunAktif = false;
    private int yakalananSayisi = 0;
    private int kacirilanSayisi = 0;
    private int firlatilanSayisi = 0;

    public void OyunuBaslat()
    {
        oyunAktif = true;
        yakalananSayisi = 0;
        kacirilanSayisi = 0;
        firlatilanSayisi = 0;

        if (oyuncuHareketKodu != null) oyuncuHareketKodu.enabled = false;
        if (minigameEkrani != null) minigameEkrani.SetActive(true);

        foreach (DusenOyuncak oyuncak in oyuncaklar)
        {
            oyuncak.OyuncagiSifirla();
        }

        StartCoroutine(OyuncaklariFirlatRoutine());
    }

    private IEnumerator OyuncaklariFirlatRoutine()
    {
        while (firlatilanSayisi < oyuncaklar.Length && oyunAktif)
        {
            float rastgeleX = Random.Range(minX, maxX);
            oyuncaklar[firlatilanSayisi].OyuncagiFirlat(rastgeleX);
            firlatilanSayisi++;
            yield return new WaitForSeconds(firlatmaAraligi);
        }
    }

    public void OyuncakYakalandi()
    {
        if (!oyunAktif) return;
        yakalananSayisi++;
        DurumKontrol();
    }

    public void OyuncakKacirildi()
    {
        if (!oyunAktif) return;
        kacirilanSayisi++;
        DurumKontrol();
    }

    private void DurumKontrol()
    {
        if (yakalananSayisi == oyuncaklar.Length)
        {
            MinigameBitir(true);
        }
        else if (yakalananSayisi + kacirilanSayisi == oyuncaklar.Length)
        {
            MinigameBitir(false);
        }
    }

    private void MinigameBitir(bool kazanildiMi)
    {
        oyunAktif = false;
        StopAllCoroutines();

        if (minigameEkrani != null) minigameEkrani.SetActive(false);
        if (oyuncuHareketKodu != null) oyuncuHareketKodu.enabled = true;

        if (genelSistem != null)
        {
            if (kazanildiMi)
            {
                genelSistem.OyunuKazan(basariSansBonusu);
            }
            else
            {
                genelSistem.OyunuKaybet(basarisizlikCezasi);
            }
        }
    }
}