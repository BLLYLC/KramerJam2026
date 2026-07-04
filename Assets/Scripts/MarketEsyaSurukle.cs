using UnityEngine;

public class MarketEsyaSurukle : MonoBehaviour
{
    public AburCuburTipi urunTipi;
    public MarketMinigameManager minigameManager;

    private Vector3 baslangicPozisyonu;
    private bool baslangicAlindi = false;
    private bool sepetinUstundeMi = false;
    private SpriteRenderer gorunum;
    private Collider2D fizikKutusu;

    private void Awake()
    {
        gorunum = GetComponent<SpriteRenderer>();
        fizikKutusu = GetComponent<Collider2D>();
        if (!baslangicAlindi)
        {
            baslangicPozisyonu = transform.position;
            baslangicAlindi = true;
        }
    }

    private void OnMouseDrag()
    {
        Vector3 farePozisyonu = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        farePozisyonu.z = 0f;
        transform.position = farePozisyonu;
    }

    private void OnMouseUp()
    {
        if (sepetinUstundeMi)
        {
            gorunum.enabled = false;
            fizikKutusu.enabled = false;
            minigameManager.SepeteEklendi(urunTipi);
        }
        else
        {
            transform.position = baslangicPozisyonu;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sepet"))
        {
            sepetinUstundeMi = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sepet"))
        {
            sepetinUstundeMi = false;
        }
    }

    public void EsyayiSifirla()
    {
        if (!baslangicAlindi)
        {
            baslangicPozisyonu = transform.position;
            baslangicAlindi = true;
        }
        sepetinUstundeMi = false;
        transform.position = baslangicPozisyonu;
        if (gorunum == null) gorunum = GetComponent<SpriteRenderer>();
        if (fizikKutusu == null) fizikKutusu = GetComponent<Collider2D>();
        gorunum.enabled = true;
        fizikKutusu.enabled = true;
    }
}