using UnityEngine;

public class DusenOyuncak : MonoBehaviour
{
    public OyuncakMinigameManager minigameManager;
    public float dusmeHizi = 5f;
    public float altSinirY = -6f;
    
    private Vector3 baslangicPozisyonu;
    private bool baslangicAlindi = false;
    private bool aktifMi = false;

    private void Awake()
    {
        if (!baslangicAlindi)
        {
            baslangicPozisyonu = transform.position;
            baslangicAlindi = true;
        }
    }

    public void OyuncagiFirlat(float rastgeleX)
    {
        transform.position = new Vector3(rastgeleX, baslangicPozisyonu.y, baslangicPozisyonu.z);
        gameObject.SetActive(true);
        aktifMi = true;
    }

    private void Update()
    {
        if (!aktifMi) return;

        transform.Translate(Vector3.down * dusmeHizi * Time.deltaTime);

        if (transform.position.y < altSinirY)
        {
            aktifMi = false;
            gameObject.SetActive(false);
            minigameManager.OyuncakKacirildi();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!aktifMi) return;

        if (other.CompareTag("MinigameSepet"))
        {
            aktifMi = false;
            gameObject.SetActive(false);
            minigameManager.OyuncakYakalandi();
        }
    }

    public void OyuncagiSifirla()
    {
        aktifMi = false;
        gameObject.SetActive(false);
        transform.position = baslangicPozisyonu;
    }
}