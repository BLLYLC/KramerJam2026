using UnityEngine;

public class KarakterDovus : MonoBehaviour
{
    [Header("Ses Ayarlarý")]
    public AudioSource sesKaynagi;
    public AudioClip normalVurusSesi;
    public AudioClip bitiriciVurusSesi;

    [Header("SaldiriAyarlari")]
    public Transform saldiriNoktasi;
    public float saldiriCapi = 0.5f;
    public LayerMask dusmanKatmani;

    [Header("Hasar Ayarlarý")]
    public int normalHasar = 10;
    public int komboHasar = 20;

    [Header("Kombo Ayarlarý")]
    public float komboFirlama = 6f;  
    public float komboSüresi = 1f;

    private int comboSayaci = 0;
    private float sonTiklamaZamani = 0f;

    void Update()
    {
        if (Time.time - sonTiklamaZamani > komboSüresi)
        {
            comboSayaci = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Saldir();
        }
    }
    void Saldir()
    {
        comboSayaci++;
        sonTiklamaZamani = Time.time;

        if (comboSayaci > 3)
        {
            comboSayaci = 1;
        }
        // ses efektleri
        if (sesKaynagi != null)
        {
            if (comboSayaci == 3)
            {
                sesKaynagi.PlayOneShot(bitiriciVurusSesi);
            }
            else
            {
                sesKaynagi.PlayOneShot(normalVurusSesi);
            }
        }

        Collider2D[] vurulanDusmanlar = Physics2D.OverlapCircleAll(saldiriNoktasi.position, saldiriCapi, dusmanKatmani);

        foreach (Collider2D dusmanCollider in vurulanDusmanlar)
        {
            DusmanDavranisi dusman = dusmanCollider.GetComponent<DusmanDavranisi>();

            if (dusman != null)
            {
                Vector2 firlamaYonu = (dusman.transform.position - transform.position).normalized;

                if (comboSayaci == 3)
                {
                    // 3. Vuruþ: Kombo hasarý ver ve geriye fýrlat
                    dusman.HasarAl(komboHasar, firlamaYonu, komboFirlama);
                }
                else
                {
                    // 1. ve 2. Vuruþ: Sadece normal hasar ver, fýrlatma gücü 0
                    dusman.HasarAl(normalHasar, Vector2.zero, 0f);
                }
            }
        }
        if (comboSayaci == 3)
        {
            comboSayaci = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (saldiriNoktasi == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(saldiriNoktasi.position, saldiriCapi);
    }
}       
