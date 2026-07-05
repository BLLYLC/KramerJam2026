using UnityEngine;

public class BebeninCani : MonoBehaviour
{
    [Header("Can Ayarlari")]
    public int azamiCan = 100;
    private int mevcutCan;

    [Header("Gorsel Ayarlari")]
    public Sprite[] canGorselleri;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer BEBESR;
    [SerializeField] private GameObject BebeGorsel;
    [SerializeField] private Sprite BEBEaglaSprite;
    [SerializeField] private Sprite BEBEidleSprite;
    private float timer = 0f;
    [SerializeField] private float aglamaSuresi = 0.75f;

    [Header("Ses Ayarlari")]
    public AudioClip hasarSesi;
    private AudioSource sesKaynagi;

    void Start()
    {
        sesKaynagi = gameObject.AddComponent<AudioSource>();
        
        BEBESR = BebeGorsel.GetComponent<SpriteRenderer>();
        mevcutCan = azamiCan;
        spriteRenderer = GetComponent<SpriteRenderer>();
        GorseliGuncelle();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > aglamaSuresi && BEBESR.sprite == BEBEaglaSprite)
        {
            BEBESR.sprite = BEBEidleSprite;
        }
    }

    public void HasarAl(int hasarMiktari)
    {
        mevcutCan -= hasarMiktari;
        Debug.Log("BebeKalp hasar aldi, kalan can: " + mevcutCan);

        if (hasarSesi != null)
        {
            sesKaynagi.PlayOneShot(hasarSesi);
        }

        GorseliGuncelle();
        BEBESR.sprite = BEBEaglaSprite;
        timer = 0f;

        if (mevcutCan <= 0)
        {
            Debug.Log("BebeKalp yok oldu, oyun bitti.");
            LevelManager.Instance.LoadOyunSonu();
        }
    }

    void GorseliGuncelle()
    {
        if (canGorselleri == null || canGorselleri.Length < 4)
        {
            Debug.LogError("Bebenincani script'ine 4 adet gorsel atayin");
            return;
        }

        float canYuzdesi = (float)mevcutCan / azamiCan;

        if (canYuzdesi > 0.75f)
        {
            spriteRenderer.sprite = canGorselleri[0];
        }
        else if (canYuzdesi > 0.50f)
        {
            spriteRenderer.sprite = canGorselleri[1];
        }
        else if (canYuzdesi > 0.25f)
        {
            spriteRenderer.sprite = canGorselleri[2];
        }
        else
        {
            spriteRenderer.sprite = canGorselleri[3];
        }
    }
}