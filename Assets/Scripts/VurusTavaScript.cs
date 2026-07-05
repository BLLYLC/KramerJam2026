using UnityEngine;

public class VurusTavaScript : MonoBehaviour
{
    float time = 0;
    [SerializeField]float tavaSuresi = 0.2f;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > tavaSuresi)
        {
            Destroy(this.gameObject);
        }
    }
}
