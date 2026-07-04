using UnityEngine;

public class KapiKontrol : MonoBehaviour
{
    private bool oyuncuAlanda = false;
    private bool kapiAcik = false;

    [Header("Ses Ayarlarý")]
    public AudioSource sesKaynagi;
    public AudioClip kapiacses;
    public AudioClip kapikapatses;

    [Header("Kapý Ayarlarý")]
    public float acilmaAcisi = -90f;
    public float donmeHizi = 5f;

    [Header("Bileþen Baðlantýsý")]
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
            hedefRotasyon = kapaliRotasyon * Quaternion.Euler(0, 0, acilmaAcisi);

            if (kapiDuvarCollider != null)
            {
                kapiDuvarCollider.isTrigger = true; // Engeli kaldýr
                sesKaynagi.PlayOneShot(kapiacses);
            }
        }
        else
        {
            hedefRotasyon = kapaliRotasyon;

            if (kapiDuvarCollider != null)
            {
                kapiDuvarCollider.isTrigger = false; // Engeli geri koy
                sesKaynagi.PlayOneShot(kapikapatses);
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuAlanda = false;
        }
    }
}
