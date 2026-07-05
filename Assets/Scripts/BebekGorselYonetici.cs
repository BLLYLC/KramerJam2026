using UnityEngine;
using System.Collections;

public class BebekGorselYonetici : MonoBehaviour
{
    public static BebekGorselYonetici instance;

    [Header("Hareket Ayarlari")]
    public SpriteRenderer hareketliBlok; 
    public Transform baslangicNoktasi;
    public float hareketHizi = 15f;

    [Header("Tahta Ayarlari")]
    public Transform[] delikNoktalari; 
    public SpriteRenderer[] dolguGorselleri; 
    public Sprite[] sekilGorselleri; 

    private bool animasyonOynuyor = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GorselleriSifirla(); 
    }

    public void BasariAnimasyonunuBaslat()
    {
        if (!animasyonOynuyor)
        {
            int doldurulanDelikIndex = BebekMekanigi.instance.mevcutSekil - 1; 
            StartCoroutine(BasariRoutine(doldurulanDelikIndex));
        }
    }

    public void HataAnimasyonunuBaslat()
    {
        if (!animasyonOynuyor)
        {
            int asilGirmesiGereken = BebekMekanigi.instance.mevcutSekil;
            StartCoroutine(HataRoutine(asilGirmesiGereken));
        }
    }

    private IEnumerator BasariRoutine(int hedefIndex)
    {
        animasyonOynuyor = true;
        Transform hedefDelik = delikNoktalari[hedefIndex];

        while (Vector3.Distance(hareketliBlok.transform.position, hedefDelik.position) > 0.1f)
        {
            hareketliBlok.transform.position = Vector3.MoveTowards(hareketliBlok.transform.position, hedefDelik.position, hareketHizi * Time.deltaTime);
            yield return null;
        }

        dolguGorselleri[hedefIndex].enabled = true;

        hareketliBlok.transform.position = baslangicNoktasi.position;

        if (BebekMekanigi.instance.mevcutSekil < sekilGorselleri.Length)
        {
            hareketliBlok.sprite = sekilGorselleri[BebekMekanigi.instance.mevcutSekil];
        }

        animasyonOynuyor = false;
    }

    private IEnumerator HataRoutine(int dogruHedef)
    {
        animasyonOynuyor = true;

        int rastgeleYanlisDelik = Random.Range(0, delikNoktalari.Length);
        while (rastgeleYanlisDelik == dogruHedef)
        {
            rastgeleYanlisDelik = Random.Range(0, delikNoktalari.Length);
        }

        Transform hedefDelik = delikNoktalari[rastgeleYanlisDelik];

        while (Vector3.Distance(hareketliBlok.transform.position, hedefDelik.position) > 0.1f)
        {
            hareketliBlok.transform.position = Vector3.MoveTowards(hareketliBlok.transform.position, hedefDelik.position, hareketHizi * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        while (Vector3.Distance(hareketliBlok.transform.position, baslangicNoktasi.position) > 0.1f)
        {
            hareketliBlok.transform.position = Vector3.MoveTowards(hareketliBlok.transform.position, baslangicNoktasi.position, hareketHizi * Time.deltaTime);
            yield return null;
        }

        animasyonOynuyor = false;
    }

    public void GorselleriSifirla()
    {
        StopAllCoroutines();
        animasyonOynuyor = false;
        
        foreach (SpriteRenderer dolgu in dolguGorselleri)
        {
            dolgu.enabled = false;
        }

        hareketliBlok.transform.position = baslangicNoktasi.position;
        if (sekilGorselleri.Length > 0)
        {
            hareketliBlok.sprite = sekilGorselleri[0];
        }
    }
}