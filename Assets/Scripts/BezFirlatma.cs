using UnityEngine;

public class BezFirlatma : MonoBehaviour
{
    public float firlatmaGucu = 8f;
    public float maksimumCekmeMesafesi = 2.5f;
    public BezMinigameManager minigameManager;

    private Vector3 baslangicPozisyonu;
    private Rigidbody2D rb;
    private bool firlatildiMi = false;
    private bool surukleniyorMu = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        baslangicPozisyonu = transform.position;
        rb.isKinematic = true;
    }

    public void Sifirla()
    {
        transform.position = baslangicPozisyonu;
        rb.isKinematic = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        firlatildiMi = false;
        surukleniyorMu = false;
        Debug.Log("Bez sifirlandi ve atisa hazir.");
    }

    private void OnMouseDown()
    {
        if (firlatildiMi) return;
        surukleniyorMu = true;
    }

    private void OnMouseDrag()
    {
        if (!surukleniyorMu) return;

        Vector3 farePozisyonu = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        farePozisyonu.z = baslangicPozisyonu.z;

        Vector3 mesafe = farePozisyonu - baslangicPozisyonu;
        if (mesafe.magnitude > maksimumCekmeMesafesi)
        {
            mesafe = mesafe.normalized * maksimumCekmeMesafesi;
        }

        transform.position = baslangicPozisyonu + mesafe;
    }

    private void OnMouseUp()
    {
        if (!surukleniyorMu) return;
        surukleniyorMu = false;
        firlatildiMi = true;

        rb.isKinematic = false;
        
        Vector3 firlatmaVektoru = baslangicPozisyonu - transform.position;
        rb.AddForce(firlatmaVektoru * firlatmaGucu, ForceMode2D.Impulse);
        Debug.Log("Bez firlatildi!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bez KATI BIR SEYE CARPTI: " + collision.gameObject.name + " | Etiketi: " + collision.gameObject.tag);

        if (!firlatildiMi) 
        {
            Debug.Log("Carpisma oldu ama bez henuz firlatilmamis sayiliyor.");
            return;
        }

        if (collision.gameObject.CompareTag("Zemin"))
        {
            Debug.Log("BEZ ZEMINE CARPTI! Oyun Kaybedildi.");
            minigameManager.MinigameBitir(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bez HAYALET BIR SEYIN ICINDEN GECTI: " + other.gameObject.name + " | Etiketi: " + other.tag);

        if (!firlatildiMi) return;

        if (other.CompareTag("CopKovasi"))
        {
            Debug.Log("BEZ COPE GIRDI! Oyun Kazanildi.");
            minigameManager.MinigameBitir(true);
        }
    }
}