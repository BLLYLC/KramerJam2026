using UnityEngine;

public class SepetHareket : MonoBehaviour
{
    private Vector3 baslangicPozisyonu;
    private bool baslangicAlindi = false;

    private void Awake()
    {
        if (!baslangicAlindi)
        {
            baslangicPozisyonu = transform.position;
            baslangicAlindi = true;
        }
    }

    private void OnEnable()
    {
        if (baslangicAlindi)
        {
            transform.position = baslangicPozisyonu;
        }
    }

    private void OnMouseDrag()
    {
        Vector3 farePozisyonu = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(farePozisyonu.x, baslangicPozisyonu.y, baslangicPozisyonu.z);
    }
}