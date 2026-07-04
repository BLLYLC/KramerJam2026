using UnityEngine;

public class KarakterHareket : MonoBehaviour
{
    public float hareketHizi = 5f;

    private Rigidbody2D rb;
    private Vector2 hareketYonu;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float hareketX = Input.GetAxisRaw("Horizontal");
        float hareketY = Input.GetAxisRaw("Vertical");

        hareketYonu = new Vector2(hareketX, hareketY).normalized;
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(hareketYonu.x * hareketHizi, hareketYonu.y * hareketHizi);
    }
}
