using UnityEngine;

public class KapiKontrol : MonoBehaviour
{
    private bool oyuncuAlanda = false;
    private bool kapiAcik = false;

    [Header("Kapý Ayarlarý")]
    public float acilmaAcisi = -90f; // 2D'de açýlma yönüne göre 90 veya -90 yapabilirsin
    public float donmeHizi = 5f;

    [Header("Bileþen Baðlantýsý")]
    // public yaparak Inspector'da kesinlikle görünmesini saðlýyoruz
    public Collider2D kapiDuvarCollider;

    private Quaternion hedefRotasyon;
    private Quaternion kapaliRotasyon;

    void Start()
    {
        kapaliRotasyon = transform.rotation;
        hedefRotasyon = kapaliRotasyon;

        if (kapiDuvarCollider == null)
        {
            kapiDuvarCollider = GetComponent<Collider2D>();
        }
    }

    void Update()
    {
        if (oyuncuAlanda && Input.GetKeyDown(KeyCode.E))
        {
            KapiyiTetikle();
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, hedefRotasyon, Time.deltaTime * donmeHizi);
    }

    void KapiyiTetikle()
    {
        kapiAcik = !kapiAcik;

        if (kapiAcik)
        {
            // 2D oyunlarda dönüþ Z ekseni üzerinden yapýlýr
            hedefRotasyon = kapaliRotasyon * Quaternion.Euler(0, 0, acilmaAcisi);

            if (kapiDuvarCollider != null)
            {
                kapiDuvarCollider.isTrigger = true; // Engeli kaldýr
            }
        }
        else
        {
            hedefRotasyon = kapaliRotasyon;

            if (kapiDuvarCollider != null)
            {
                kapiDuvarCollider.isTrigger = false; // Engeli geri koy
            }
        }
    }

    // 2D Tetikleyiciler
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuAlanda = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) // Küçük yazým hatasýný düzelterek Collider2D yapýyoruz
    {
        if (other.CompareTag("Player"))
        {
            oyuncuAlanda = false;
        }
    }
}
