using UnityEngine;

public class MinigameSistemi : MonoBehaviour
{
    [Header("Minigame Etki Ayarları")]
    public float kazanmaEtkisi = 15f;  
    public float kaybetmeEtkisi = -10f;

    public KarakterDovus oyuncununDovusScripti;

    public void OyunuKazan()
    {
        Debug.Log("Minigame KAZANILDI! Bebeğin şansı artıyor: +" + kazanmaEtkisi);
        BebekMekanigi.instance.OlasiligiDegistir(kazanmaEtkisi);
        oyuncununDovusScripti.saldirabilirMi = true;
    }

    public void OyunuKaybet()
    {
        Debug.Log("Minigame KAYBEDİLDİ! Bebeğin şansı düşüyor: " + kaybetmeEtkisi);
        BebekMekanigi.instance.OlasiligiDegistir(kaybetmeEtkisi);
        oyuncununDovusScripti.saldirabilirMi = true;
    }
}