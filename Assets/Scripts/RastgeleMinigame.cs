using System.Collections;
using UnityEngine;

public class RastgeleMinigame : MonoBehaviour
{
    public bool islemTamamlandi = false;

    public bool yemek = false;
    public bool market = false;
    public bool oyuncak = false;
    public bool bez = false;

    void Start()
    {
        StartCoroutine(AnaSecimDongusu());
    }

    IEnumerator AnaSecimDongusu()
    {
        while (true)
        {
            // 1. ADIM: 4 saniye boyunca bekliyoruz
            yield return new WaitForSeconds(4f);

            // 2. ADIM: 0, 1, 2, 3 sayýlarýndan birini rastgele seç (%25 þans)
            // NOT: Random.Range tam sayýlarda üst sýnýrý dahil etmez, bu yüzden (0, 4) yazdýk.
            int rastgeleSecim = Random.Range(0, 4);
            Debug.Log("Rastgele Sayý Seçildi: " + rastgeleSecim);

            // 3. ADIM: Seçilen görevi çalýþtýr ve o görev TAMAMEN BÝTENE KADAR burada bekle
            yield return StartCoroutine(SecilenGoreviCalistir(rastgeleSecim));

            // Görev bittiði an döngü baþa dönecek ve YENÝDEN 4 saniye saymaya baþlayacak.
            Debug.Log("Çalýþtýrýlan þey bitti. 4 saniyelik yeni sayaç baþladý...");
        }
    }

    IEnumerator SecilenGoreviCalistir(int secim)
    {
        switch (secim)
        {
            case 0:
                yield return StartCoroutine(GorevSifir());
                break;
            case 1:
                yield return StartCoroutine(GorevBir());
                break;
            case 2:
                yield return StartCoroutine(GorevIki());
                break;
            case 3:
                yield return StartCoroutine(GorevUc());
                break;
        }
    }

    IEnumerator GorevSifir()
    {
        Debug.Log("0. Görev baþladý");
        yemek = true;
        islemTamamlandi = false;

        yield return new WaitUntil(() => islemTamamlandi == true); ;
        Debug.Log("0. Görev bitti!");
    }

    IEnumerator GorevBir()
    {
        Debug.Log("1. Görev baþladý");
        market = true;
        islemTamamlandi = false;

        yield return new WaitUntil(() => islemTamamlandi == true); ;
        Debug.Log("1. Görev bitti!");
    }

    IEnumerator GorevIki()
    {
        Debug.Log("2. Görev baþladý");
        oyuncak = true;
        islemTamamlandi = false;

        yield return new WaitUntil(() => islemTamamlandi == true); ;
        Debug.Log("2. Görev bitti!");
    }

    IEnumerator GorevUc()
    {
        Debug.Log("3. Görev baþladý");
        bez = true;
        islemTamamlandi = false;

        yield return new WaitUntil(() => islemTamamlandi == true); ;
        Debug.Log("3. Görev bitti!");
    }

}
