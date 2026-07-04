using UnityEngine;

public class DusmanDavranisi : MonoBehaviour
{
    private Rigidbody2D rb;

    public int azamiCan = 100;
    private int mevcutCan;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        Debug.Log("Düþman öldü!");
        Destroy(gameObject);
    }
}
