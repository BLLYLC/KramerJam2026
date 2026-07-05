using UnityEngine;

public class MinigameSistemi : MonoBehaviour
{
    public void OyunuKazan(float kazanilanPuan)
    {
        Debug.Log("Minigame KAZANILDI! Bebeğin şansı artıyor: +" + kazanilanPuan);
        BebekMekanigi.instance.OlasiligiDegistir(kazanilanPuan);
        
    }

    public void OyunuKaybet(float kaybedilenPuan)
    {
        Debug.Log("Minigame KAYBEDİLDİ! Bebeğin şansı düşüyor: " + kaybedilenPuan);
        BebekMekanigi.instance.OlasiligiDegistir(kaybedilenPuan);
        
    }
}