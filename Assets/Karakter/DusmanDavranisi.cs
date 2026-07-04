using UnityEngine;

public class DusmanDavranisi : MonoBehaviour
{
    private Rigidbody2D rb;

    public int azamiCan = 100;
    private int mevcutCan;

    private Vector3 baslangicPozisyonu;

    void Start()
    {
        gameObject.SetActive(false);
        Debug.Log(gameObject.name + " öldü ve deaktif edildi.");

        rb = GetComponent<Rigidbody2D>();
        baslangicPozisyonu = transform.localPosition;
        mevcutCan = azamiCan;
    }

    public void HasarAl(int hasarMiktari, Vector2 firlamaYonu, float firlamaKuvveti)
    {
        mevcutCan -= hasarMiktari;
        Debug.Log($"Düþman {hasarMiktari} kadar hasar aldý, kalan caný: {mevcutCan}");

        if (firlamaKuvveti > 0)
        {
            rb.linearVelocity = firlamaYonu * firlamaKuvveti;
        }

        if (mevcutCan <= 0)
        {
            Ol();
        }
    }

    void Ol()
    {
        gameObject.SetActive(false);
        Debug.Log(gameObject.name + " öldü ve deaktif edildi.");
    }

    public void YenidenCanlandir()
    {
        transform.localPosition = baslangicPozisyonu;
        mevcutCan = azamiCan;
        gameObject.SetActive(true);
        Debug.Log(gameObject.name + " yeniden canlandý!");
    }
}
