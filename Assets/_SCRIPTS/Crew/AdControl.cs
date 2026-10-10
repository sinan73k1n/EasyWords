using UnityEngine;

// DEMO: Unity Ads kaldırıldı — gerçek reklam istenmez. Sahnelerdeki bileşen ve çağrılar bozulmasın diye
// sınıf aynı adla duruyor; bütün metotlar boş. Eski gerçek reklam kodu git geçmişinde (1.0.9, 1959208).
public class AdControl : MonoBehaviour
{
    public static AdControl instance;

    private void Awake()
    {
        if (FindObjectsOfType<AdControl>().Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void ShowOdul() { }

    public void ShowGecis() { }

    public void ShowBanner() { }

    public void CloseBanner() { }
}
