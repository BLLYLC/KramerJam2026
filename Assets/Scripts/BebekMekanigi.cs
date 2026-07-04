using UnityEngine;
using UnityEngine.Events; 

public class BebekMekanigi : MonoBehaviour
{
    // --- SİNGLETON (MUHTAR) KISMI ---
    public static BebekMekanigi instance;

    private void Awake() 
    { 
        instance = this; 
    }

    // --- MEKANİK AYARLARI ---
    [Header("Mekanik Ayarları")]
    [Tooltip("Bebeğin bloğu doğru yerine koyma olasılığı (0-100 arası)")]
    [Range(0f, 100f)] 
    public float basariOlasiligi = 30f; 

    [Header("Oyun İçi Tetikleyiciler")]
    public UnityEvent DogruKoyuldugunda;
    public UnityEvent YanlisKoyuldugunda;

    [ContextMenu("Zar At")]
    public void BlokKoymayiDene()
    {
        float cekilenSayi = Random.Range(0f, 100f);

        if (cekilenSayi <= basariOlasiligi)
        {
            Debug.Log("BAŞARILI! Bebek bloğu doğru soktu. Çekilen sayı: " + cekilenSayi);
            DogruKoyuldugunda.Invoke(); 
        }
        else
        {
            Debug.Log("YANLIŞ! Bebek bloğu sokamadı. Çekilen sayı: " + cekilenSayi);
            YanlisKoyuldugunda.Invoke(); 
        }
    }

    public void OlasiligiDegistir(float miktar)
    {
        basariOlasiligi += miktar;
        basariOlasiligi = Mathf.Clamp(basariOlasiligi, 0f, 100f);
        Debug.Log("Yeni Başarı Olasılığı: %" + basariOlasiligi);
    }
}