using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;
using System.Collections;

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
    [SerializeField] public int kazanmakIcinGerekenSekil = 8;
    public int toplamYanlisSayisi = 0;
    public int kurtarilmaSayisi = 0;

    [Header("Arayuz Ayarlari")]
    public TMP_Text hakYazisi;
    public TMP_Text kurtarilmaYazisi;
    public TMP_Text asamaYazisi;
    public TMP_Text durumBildirimYazisi; 

    [Header("Zamanlayicilar")]
    public float hamleSuresi = 5f;
    private float hamleSayaci = 0f;
    
    public float sansDusmeSuresi = 2f;
    private float sansDusmeSayaci = 0f;

    [Header("Sistem Durumu")]
    public bool oyunBitti = false;
    public bool dusmanBekleniyor = false;

    [Header("Dusman Sistemi")]
    public List<DusmanDavranisi> baglidusmanlar = new List<DusmanDavranisi>(); 
    private int aktifDusmanSayisi = 0;

    [Header("Oyun Ici Tetikleyiciler")]
    public UnityEvent DogruKoyuldugunda = new UnityEvent();
    public UnityEvent YanlisKoyuldugunda = new UnityEvent();
    public UnityEvent OyunKazanildiginda = new UnityEvent();

    [Header("Gorsel Ayarlar")]
    [SerializeField] private GameObject BEBEGorsel;
    [SerializeField] private Sprite BEBEidleSprite;
    [SerializeField] private Sprite BEBEkızgınSprite;
    private SpriteRenderer BEBESR;
    private float bebeSayac = 0f;
    private float bebeAnimSure = 1f;

    private Coroutine bildirimRoutine; 

    private void Awake() 
    { 
        instance = this; 
    }

    private void Start()
    {
        if (BEBEGorsel != null)
        {
            BEBESR = BEBEGorsel.GetComponent<SpriteRenderer>();
        }
        
        if (durumBildirimYazisi != null)
        {
            durumBildirimYazisi.text = "";
        }
        
        ArayuzuGuncelle();
    }

    private void Update()
    {
        bebeSayac += Time.deltaTime;
        if (BEBESR != null && BEBESR.sprite != BEBEidleSprite && bebeSayac > bebeAnimSure) 
        {
            BEBESR.sprite = BEBEidleSprite;
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
            
            EkranaBildirimYaz("BASARILI!", Color.green);

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
            
            EkranaBildirimYaz("BASARISIZ DENEME", Color.red);

            if (BEBESR != null)
            {
                BEBESR.sprite = BEBEkızgınSprite;
            }
            bebeSayac = 0f;
            
            if (toplamYanlisSayisi % 3 == 0)
            {
                DusmanCagirVeBekle();
            }
        }

        ArayuzuGuncelle();
    }
    
    private void EkranaBildirimYaz(string mesaj, Color renk)
    {
        if (durumBildirimYazisi == null) return;

        if (bildirimRoutine != null)
        {
            StopCoroutine(bildirimRoutine);
        }
        bildirimRoutine = StartCoroutine(BildirimGosterRoutine(mesaj, renk));
    }

    private IEnumerator BildirimGosterRoutine(string mesaj, Color renk)
    {
        durumBildirimYazisi.text = mesaj;
        durumBildirimYazisi.color = renk;
        yield return new WaitForSeconds(1.5f);
        durumBildirimYazisi.text = "";
    }

    private void DusmanCagirVeBekle()
    {
        Debug.Log("3 YANLIS YAPILDI! Sistem kilitlendi, dusman cagiriliyor.");
        dusmanBekleniyor = true;
        aktifDusmanSayisi = 0;

        foreach (DusmanDavranisi dusman in baglidusmanlar)
        {
            if (dusman != null)
            {
                dusman.YenidenCanlandir();
                aktifDusmanSayisi++;
            }
            else
            {
                Debug.LogWarning("DIKKAT: Bagli dusman atanmamis ama sistem kilitlendi!");
            }
        }
        
        ArayuzuGuncelle();
    }

    public void DusmanYenildi()
    {
        aktifDusmanSayisi--;

        if (aktifDusmanSayisi > 0)
        {
            Debug.Log("Bir dusman yenildi ama dalga bitmedi. Kalan dusman: " + aktifDusmanSayisi);
            return;
        }

        Debug.Log("TUM DUSMANLAR YENILDI! Bebek kilidi acildi, sekiller sifirlandi.");
        dusmanBekleniyor = false; 
        mevcutSekil = 0;          
        hamleSayaci = 0f;         
        sansDusmeSayaci = 0f;
        kurtarilmaSayisi++;

        if (BebekGorselYonetici.instance != null)
        {
            BebekGorselYonetici.instance.GorselleriSifirla();
        }
        
        ArayuzuGuncelle();
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
        
        EkranaBildirimYaz("OYUN KAZANILDI!", Color.yellow);
        
        ArayuzuGuncelle();
    }

    public void ArayuzuGuncelle()
    {
        if (asamaYazisi != null)
        {
            asamaYazisi.text = "Asama: " + mevcutSekil + " / " + kazanmakIcinGerekenSekil;
        }

        if (kurtarilmaYazisi != null)
        {
            kurtarilmaYazisi.text = "Kurtarilma: " + kurtarilmaSayisi;
        }

        if (hakYazisi != null)
        {
            int kalanHak;
            if (dusmanBekleniyor)
            {
                kalanHak = 0;
            }
            else
            {
                int mod = toplamYanlisSayisi % 3;
                if (mod == 0)
                {
                    kalanHak = 3;
                }
                else if (mod == 1)
                {
                    kalanHak = 2;
                }
                else
                {
                    kalanHak = 1;
                }
            }
            
            hakYazisi.text = "Hak: " + kalanHak + " / 3";
        }
    }
}