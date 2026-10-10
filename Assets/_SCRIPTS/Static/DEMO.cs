using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Reklam ve satın alma DEMO modunda: gerçek reklam istenmez, gerçek ödeme yapılmaz,
// herkes premium ve reklamsız oynar. Reklam/satın alma düğmeleri yalnız şaka mesajı gösterir.
public static class DEMO
{
    // const değil: const olsaydı kontrolün arkasındaki eski kod "erişilemez kod" uyarısı verirdi
    public static readonly bool AKTIF = true;

    static bool Turkce { get { return Application.systemLanguage == SystemLanguage.Turkish; } }

    public static void MesajReklamIzle()
    {
        Goster(Turkce
            ? "Şaka! Bizde reklam yok.\nHer şey zaten açık, keyfini çıkar!"
            : "JUST KIDDING!\nWE DON'T HAVE ADS.\nEVERYTHING IS ALREADY UNLOCKED!");
    }

    public static void MesajReklamKaldir()
    {
        Goster(Turkce
            ? "Kaldıracak reklam yok ki!\nZaten reklamsız oynuyorsun."
            : "THERE ARE NO ADS TO REMOVE!\nYOU ARE ALREADY PLAYING AD-FREE.");
    }

    public static void MesajPremium()
    {
        Goster(Turkce
            ? "Sen zaten bizim premium kullanıcımızsın!\nBütün kategoriler senin için açık."
            : "YOU ARE ALREADY OUR PREMIUM USER!\nEVERYTHING IS UNLOCKED FOR YOU.");
    }

    // Ekranı karartan, dokununca ya da birkaç saniye sonra kapanan mesaj
    static void Goster(string mesaj)
    {
        Object eski = Object.FindObjectOfType<DemoMesajKutusu>();
        if (eski) Object.Destroy(((DemoMesajKutusu)eski).gameObject);

        GameObject goCanvas = new GameObject("DEMO_MESAJ");
        Canvas canvas = goCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 500;
        CanvasScaler scaler = goCanvas.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 1920);
        scaler.matchWidthOrHeight = 0;
        goCanvas.AddComponent<GraphicRaycaster>();

        GameObject goKarartma = new GameObject("Karartma", typeof(RectTransform), typeof(Image), typeof(Button));
        goKarartma.transform.SetParent(goCanvas.transform, false);
        RectTransform rtKarartma = goKarartma.GetComponent<RectTransform>();
        rtKarartma.anchorMin = Vector2.zero;
        rtKarartma.anchorMax = Vector2.one;
        rtKarartma.offsetMin = Vector2.zero;
        rtKarartma.offsetMax = Vector2.zero;
        goKarartma.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.75f);

        GameObject goYazi = new GameObject("Yazi", typeof(RectTransform), typeof(TextMeshProUGUI));
        goYazi.transform.SetParent(goKarartma.transform, false);
        RectTransform rtYazi = goYazi.GetComponent<RectTransform>();
        rtYazi.anchorMin = new Vector2(0.08f, 0.3f);
        rtYazi.anchorMax = new Vector2(0.92f, 0.7f);
        rtYazi.offsetMin = Vector2.zero;
        rtYazi.offsetMax = Vector2.zero;
        TextMeshProUGUI txt = goYazi.GetComponent<TextMeshProUGUI>();
        // Bangers Türkçe harflerin bir kısmını içermiyor → Türkçede varsayılan TMP fontu
        TMP_FontAsset font = Turkce ? TMP_Settings.defaultFontAsset : Resources.Load<TMP_FontAsset>("Fonts & Materials/Bangers SDF");
        if (font) txt.font = font;
        txt.text = mesaj;
        txt.alignment = TextAlignmentOptions.Center;
        txt.enableAutoSizing = true;
        txt.fontSizeMin = 40;
        txt.fontSizeMax = 90;
        txt.color = Color.yellow;
        txt.raycastTarget = false;

        DemoMesajKutusu kutu = goCanvas.AddComponent<DemoMesajKutusu>();
        goKarartma.GetComponent<Button>().onClick.AddListener(kutu.Kapat);
        if (SoundBox.instance) SoundBox.instance.PlayOneShot(NamesOfSound.bas);
    }
}

public class DemoMesajKutusu : MonoBehaviour
{
    const float SURE = 3.5f;

    private void Start()
    {
        Invoke(nameof(Kapat), SURE);
    }

    public void Kapat()
    {
        if (this) Destroy(gameObject);
    }
}
