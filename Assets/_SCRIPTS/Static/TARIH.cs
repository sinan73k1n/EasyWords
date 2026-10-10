using System;
using System.Globalization;

// PlayerPrefs'te tarih saklama. Eskiden DateTime.ToString()/Parse() kullanılıyordu → telefonun dil/bölge
// ayarına bağlıydı; ayar değişince Parse hata verip oyunu bozabiliyordu. Artık kültürden bağımsız "o" biçimi yazılır,
// eski biçimde kaydedilmiş değerler de okunur; okunamazsa varsayılana düşülür (çökme yok).
public static class TARIH
{
    public static string Yaz(DateTime tarih)
    {
        return tarih.ToString("o", CultureInfo.InvariantCulture);
    }

    public static DateTime Oku(string metin, DateTime varsayilan)
    {
        if (string.IsNullOrEmpty(metin)) return varsayilan;
        DateTime sonuc;
        if (DateTime.TryParseExact(metin, "o", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out sonuc)) return sonuc;
        if (DateTime.TryParse(metin, CultureInfo.CurrentCulture, DateTimeStyles.None, out sonuc)) return sonuc;
        if (DateTime.TryParse(metin, CultureInfo.InvariantCulture, DateTimeStyles.None, out sonuc)) return sonuc;
        return varsayilan;
    }
}
