using System.Collections;
using UnityEngine;

public class DusmanDavranisi : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;

    public int azamiCan = 100;
    private int mevcutCan;

    private Vector3 baslangicPozisyonu;

    [Header("Sersemleme Ayarları")]
    public float sersemlemeSuresi = 0.5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        gameObject.SetActive(false);
        Debug.Log(gameObject.name + " �ld� ve deaktif edildi.");

        rb = GetComponent<Rigidbody2D>();
        baslangicPozisyonu = transform.localPosition;
        mevcutCan = azamiCan;
    }
    private IEnumerator KirmiziFlasEfekti()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.15f);

        spriteRenderer.color = Color.white;
    }

    public void HasarAl(int hasarMiktari, Vector2 firlamaYonu, float firlamaKuvveti)
    {
        mevcutCan -= hasarMiktari;
        StartCoroutine(KirmiziFlasEfekti());
        Debug.Log($"D��man {hasarMiktari} kadar hasar ald�, kalan can�: {mevcutCan}");

        if (firlamaKuvveti > 0 && rb != null)
        {
            rb.linearVelocity = firlamaYonu * firlamaKuvveti;
            CancelInvoke("Toparlan");
            Invoke("Toparlan", sersemlemeSuresi);
        }

        if (mevcutCan <= 0)
        {
            Ol();
        }
    }
    void Toparlan()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void Ol()
    {
        gameObject.SetActive(false);
        Debug.Log(gameObject.name + " �ld� ve deaktif edildi.");
        BebekMekanigi.instance.DusmanYenildi();
    }

    public void YenidenCanlandir()
    {
        transform.localPosition = baslangicPozisyonu;
        mevcutCan = azamiCan;
        gameObject.SetActive(true);
        spriteRenderer.color = Color.white;
        Debug.Log(gameObject.name + " yeniden canland�");
    }
}
