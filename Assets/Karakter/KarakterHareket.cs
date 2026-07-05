using UnityEngine;

public class KarakterHareket : MonoBehaviour
{
    public float hareketHizi = 5f;

    private Rigidbody2D rb;
    private Vector2 hareketYonu;

    private bool sagaBakiyor = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float hareketX = Input.GetAxisRaw("Horizontal");
        float hareketY = Input.GetAxisRaw("Vertical");

        hareketYonu = new Vector2(hareketX, hareketY).normalized;

        if (hareketX > 0 && !sagaBakiyor)
        {
            YonuCevir();
        }
        else if (hareketX < 0 && sagaBakiyor)
        {
            YonuCevir();
        }
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(hareketYonu.x * hareketHizi, hareketYonu.y * hareketHizi);
    }

    void YonuCevir()
    {
        sagaBakiyor = !sagaBakiyor;

        Vector3 yeniOlcek = transform.localScale;

        yeniOlcek.x *= -1;

        transform.localScale = yeniOlcek;
    }
    public bool sagaBakýyorMuAl()
    {
        return sagaBakiyor;
    }
}
