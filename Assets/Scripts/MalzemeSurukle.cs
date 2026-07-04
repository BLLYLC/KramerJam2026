using UnityEngine;

public class MalzemeSurukle : MonoBehaviour
{
    private Vector3 baslangicPozisyonu;
    private bool tavaninUstundeMi = false;
    
    [Header("Sistem Bağlantısı")]
    public YemekMinigameManager minigameManager;

    private void Start()
    {
        baslangicPozisyonu = transform.position; 
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
            Debug.Log(gameObject.name + " tavaya eklendi!");
            minigameManager.MalzemeEklendi(); 
            gameObject.SetActive(false); 
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
}