using UnityEngine;

public class DusmanSaldiri : MonoBehaviour
{
    public float takipHizi = 2f;
    public int saniyelikHasar = 10;

    private Transform hedefTransform;
    private BebeninCani hedefCanScripti;

    private bool hedefeDegiyorMu = false;
    private float hasarZamanlayici = 0f;

    void Start()
    {
        GameObject hedefNesne = GameObject.Find("BebeKalp");

        if (hedefNesne != null)
        {
            hedefTransform = hedefNesne.transform;
            hedefCanScripti = hedefNesne.GetComponent<BebeninCani>();
        }
        else
        {
            Debug.LogWarning("Sahnede 'BebeKalp' adýnda bir nesne bulunamadý!");
        }
    }

    void Update()
    {
        if (hedefTransform != null && !hedefeDegiyorMu)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                hedefTransform.position,
                takipHizi * Time.deltaTime
            );
        }

        if (hedefeDegiyorMu && hedefCanScripti != null)
        {
            // Zamanlayýcýya geçen süreyi ekliyoruz (saniye hesabý)
            hasarZamanlayici += Time.deltaTime;

            // 1 saniye dolduðunda hasar ver ve zamaný sýfýrla
            if (hasarZamanlayici >= 1f)
            {
                hedefCanScripti.HasarAl(saniyelikHasar);
                hasarZamanlayici = 0f; // Zamanlayýcýyý sýfýrla ki yeniden saysýn
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BebeKalp")
        {
            hedefeDegiyorMu = true;
            hasarZamanlayici = 1f; // Ýlk deðdiði an bekletmeden hemen ilk hasarý vursun diye 1 yapýyoruz
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BebeKalp")
        {
            hedefeDegiyorMu = false;
            hasarZamanlayici = 0f;
        }
    }
}
