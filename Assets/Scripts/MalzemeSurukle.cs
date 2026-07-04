using UnityEngine;

public class MalzemeSurukle : MonoBehaviour
{
    private Vector3 baslangicPozisyonu;
    private bool baslangicAlindi = false;
    private bool tavaninUstundeMi = false;
    private SpriteRenderer gorunum;
    private Collider2D fizikKutusu;
    
    [Header("Sistem Baglantisi")]
    public YemekMinigameManager minigameManager;

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
        if (tavaninUstundeMi)
        {
            minigameManager.MalzemeEklendi(); 
            gorunum.enabled = false;
            fizikKutusu.enabled = false;
        }
        else
        {
            transform.position = baslangicPozisyonu;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tava"))
        {
            tavaninUstundeMi = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Tava"))
        {
            tavaninUstundeMi = false;
        }
    }

    public void MalzemeyiSifirla()
    {
        if (!baslangicAlindi)
        {
            baslangicPozisyonu = transform.position;
            baslangicAlindi = true;
        }
        tavaninUstundeMi = false;
        transform.position = baslangicPozisyonu;
        if (gorunum == null) gorunum = GetComponent<SpriteRenderer>();
        if (fizikKutusu == null) fizikKutusu = GetComponent<Collider2D>();
        gorunum.enabled = true;
        fizikKutusu.enabled = true;
    }
}