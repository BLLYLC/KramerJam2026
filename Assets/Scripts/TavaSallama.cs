using UnityEngine;

public class TavaSallama : MonoBehaviour
{
    [Header("Sistem Baglantisi")]
    public YemekMinigameManager minigameManager;
    
    public float sallamaEsigi = 15f; 

    private Vector3 tavaBaslangicPozisyonu;
    private bool baslangicAlindi = false;
    private float sonFareY;
    private int sonSallamaYonu = 0; 

    private void Awake()
    {
        if (!baslangicAlindi)
        {
            tavaBaslangicPozisyonu = transform.position;
            baslangicAlindi = true;
        }
    }

    private void OnEnable()
    {
        if (baslangicAlindi)
        {
            transform.position = tavaBaslangicPozisyonu;
            sonSallamaYonu = 0;
        }
    }

    private void OnMouseDown()
    {
        sonFareY = Input.mousePosition.y;
    }

    private void OnMouseDrag()
    {
        Vector3 farePozisyonu = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(tavaBaslangicPozisyonu.x, farePozisyonu.y, tavaBaslangicPozisyonu.z);

        float suankiFareY = Input.mousePosition.y;
        float fark = suankiFareY - sonFareY;

        if (Mathf.Abs(fark) > sallamaEsigi)
        {
            int suankiYon = fark > 0 ? 1 : -1;

            if (suankiYon != sonSallamaYonu)
            {
                sonSallamaYonu = suankiYon;
                sonFareY = suankiFareY;
                minigameManager.TavayiSalla();
            }
        }
    }

    private void OnMouseUp()
    {
        transform.position = tavaBaslangicPozisyonu;
        sonSallamaYonu = 0;
    }
}