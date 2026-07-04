using UnityEngine;

public class BebeninCani : MonoBehaviour
{
    [Header("Can Ayarlarý")]
    public int azamiCan = 100;
    private int mevcutCan;

    [Header("Görsel Ayarlarý")]
    // Buraya Unity editöründen 4 görseli sýrasýyla koyacaðýz
    // Element 0: Tam Can (En saðlam kalp)
    // Element 3: Kritik Can (En kýrýk kalp)
    public Sprite[] canGorselleri;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        mevcutCan = azamiCan;

        spriteRenderer = GetComponent<SpriteRenderer>();

        GorseliGuncelle();
    }

    public void HasarAl(int hasarMiktari)
    {
        mevcutCan -= hasarMiktari;
        Debug.Log("BebeKalp hasar aldý, kalan can: " + mevcutCan);

        GorseliGuncelle();

        if (mevcutCan <= 0)
        {
            Debug.Log("BebeKalp yok oldu, oyun bitti.");
            // + oyun bitti kodu
        }
    }
    void GorseliGuncelle()
    {
        if (canGorselleri == null || canGorselleri.Length < 4)
        {
            Debug.LogError("Bebenincani script'ine 4 adet görsel atayýn");
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
