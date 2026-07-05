using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class BebekMekanigi : MonoBehaviour
{
    public static BebekMekanigi instance;

    [Header("Mekanik Ayarlari")]
    [Range(0f, 100f)] 
    public float basariOlasiligi = 30f; 
    public float minimumOlasilik = 5f;
    public float maksimumOlasilik = 100f;
    public float ikiSaniyedeDusenMiktar = 2f;

    [Header("Oyun Asamalari")]
    public int mevcutSekil = 0;
    public int kazanmakIcinGerekenSekil = 5;
    public int toplamYanlisSayisi = 0;

    [Header("Zamanlayicilar")]
    public float hamleSuresi = 5f;
    private float hamleSayaci = 0f;
    
    public float sansDusmeSuresi = 2f;
    private float sansDusmeSayaci = 0f;

    [Header("Sistem Durumu")]
    public bool oyunBitti = false;
    public bool dusmanBekleniyor = false;

    [Header("Dusman Sistemi")]
    public List <DusmanDavranisi> baglidusmanlar = new List<DusmanDavranisi>(); 

    [Header("Oyun Ici Tetikleyiciler")]
    public UnityEvent DogruKoyuldugunda;
    public UnityEvent YanlisKoyuldugunda;
    public UnityEvent OyunKazanildiginda;

    [SerializeField] private GameObject BEBEGorsel;
    [SerializeField] private Sprite BEBE_idle; 
    [SerializeField] private Sprite BEBE_angry;
    private SpriteRenderer BEBESR;
    private float bebeSpriteSuresi = 1f;
    private float bebeSpriteSayac = 0f;
    private void Awake() 
    { 
        instance = this; 
    }
    private void Start()
    {
        BEBESR = BEBEGorsel.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        bebeSpriteSayac += Time.deltaTime;
        if (BEBESR.sprite != BEBE_idle && bebeSpriteSayac > bebeSpriteSuresi) { 
            BEBESR.sprite = BEBE_idle;
        }

        if (oyunBitti || dusmanBekleniyor) return;

        sansDusmeSayaci += Time.deltaTime;
        if (sansDusmeSayaci >= sansDusmeSuresi)
        {
            sansDusmeSayaci = 0f;
            OlasiligiDegistir(-ikiSaniyedeDusenMiktar);
        }

        hamleSayaci += Time.deltaTime;
        if (hamleSayaci >= hamleSuresi)
        {
            hamleSayaci = 0f;
            BlokKoymayiDene();
        }
    }

    [ContextMenu("Zar At")]
    public void BlokKoymayiDene()
    {
        float cekilenSayi = Random.Range(0f, 100f);
        Debug.Log("BEBEK HAMLE YAPIYOR... Sansi: %" + basariOlasiligi + " | Cekilen Zar: " + cekilenSayi);

        if (cekilenSayi <= basariOlasiligi)
        {
            mevcutSekil++;
            Debug.Log("BASARILI! Bebek " + mevcutSekil + ". sekli koydu.");
            DogruKoyuldugunda.Invoke(); 

            if (mevcutSekil >= kazanmakIcinGerekenSekil)
            {
                TumOyunuKazan();
            }
        }
        else
        {
            toplamYanlisSayisi++;
            Debug.Log("HATA! Bebek sekli koyamadi. Toplam Yanlis: " + toplamYanlisSayisi);
            YanlisKoyuldugunda.Invoke();
            
            BEBESR.sprite = BEBE_angry;
            bebeSpriteSayac = 0f;


            if (toplamYanlisSayisi % 3 == 0)
            {
                DusmanCagirVeBekle();
            }
        }
    }

    private void DusmanCagirVeBekle()
    {
        Debug.Log("3 YANLIS YAPILDI! Sistem kilitlendi, dusman cagiriliyor.");
        dusmanBekleniyor = true;
        foreach (DusmanDavranisi dusman in baglidusmanlar)
        {
            if (baglidusmanlar != null)
            {
                dusman.YenidenCanlandir();
            }
            else
            {
                Debug.LogWarning("DIKKAT: Bagli dusman atanmamis ama sistem kilitlendi!");
            }
        }
        
    }

    public void DusmanYenildi()
    {
        Debug.Log("DUSMAN YENILDI! Bebek kilidi acildi, sekiller sifirlandi.");
        dusmanBekleniyor = false; 
        mevcutSekil = 0;          
        hamleSayaci = 0f;         
        sansDusmeSayaci = 0f;
        BebekGorselYonetici.instance.GorselleriSifirla();
    }

    public void OlasiligiDegistir(float miktar)
    {
        basariOlasiligi += miktar;
        basariOlasiligi = Mathf.Clamp(basariOlasiligi, minimumOlasilik, maksimumOlasilik);
        Debug.Log("SANS GUNCELLEMESI! Yeni Sans: %" + basariOlasiligi);
    }

    private void TumOyunuKazan()
    {
        Debug.Log("MUKEMMEL! Bebek tüm sekilleri bitirdi ve OYUN KAZANILDI!");
        oyunBitti = true;
        OyunKazanildiginda.Invoke();
    }
}