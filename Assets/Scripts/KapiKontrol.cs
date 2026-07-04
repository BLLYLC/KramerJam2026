using UnityEngine;

public class KapiKontrol : MonoBehaviour
{
    private bool oyuncuAlanda = false; 
    private bool kapiAcik = false;     

    [Header("Kapý Ayarlarý")]
    public float acilmaAcisi = 90f;   
    public float donmeHizi = 5f;      

    private Quaternion hedefRotasyon;
    private Quaternion kapaliRotasyon;

    void Start()
    {
        kapaliRotasyon = transform.rotation;
        hedefRotasyon = kapaliRotasyon;
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
        kapiAcik = !kapiAcik; // Açýk ise kapalý, kapalý ise açýk yap

        if (kapiAcik)
        {
            // Y ekseninde (yukarý doðru) baþlangýç açýsýna belirlenen dereceyi ekle
            hedefRotasyon = kapaliRotasyon * Quaternion.Euler(0, acilmaAcisi, 0);
        }
        else
        {
            // Hedefi tekrar eski orijinal haline getir
            hedefRotasyon = kapaliRotasyon;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuAlanda = true;
            // Buraya ekrana "E tuþuna bas" yazdýracak UI kodlarý gelebilir.
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuAlanda = false;
        }
    }
}
