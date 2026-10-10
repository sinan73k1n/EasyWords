using UnityEngine;
using System.Collections;
using System;

public class PREMIUM : MonoBehaviour
{


    public static bool GetPremiumVar() {
        if (DEMO.AKTIF) return true;
        return PlayerPrefs.GetInt("premiumVar", 0) == 1 ? true : false;
    }
    public static void SetPremiumVar(bool deger) { PlayerPrefs.SetInt("premiumVar", deger ? 1 : 0); }

    public static bool GetPremiumGunlukCalisiyor() { return PlayerPrefs.GetInt("premiumgunluk calisiyor", 0) == 1 ? true : false; }
    public static void SetPremiumGunlukCalisiyor(bool deger) { PlayerPrefs.SetInt("premiumgunluk calisiyor", deger ? 1 : 0); }

    public static bool GetPremiumGunlukAlinabilir() { return PlayerPrefs.GetInt("premiumgunluk alinabilir", 0) == 1 ? true : false; }
    public static void SetPremiumGunlukAlinabilir(bool deger) { PlayerPrefs.SetInt("premiumgunluk alinabilir", deger ? 1 : 0); }

    public static int GetPremiumGunlukCount() { return PlayerPrefs.GetInt("PremiumgunlukCount", 5); }
    public static void SetPremiumGunlukCount(int count) { PlayerPrefs.SetInt("PremiumgunlukCount", count); }

    public static DateTime GetPremiumAlinacakBirSonrakiSure() { return TARIH.Oku(PlayerPrefs.GetString("PremiumAlinacakBirSonrakiSure", ""), DateTime.Today.AddMinutes(2)); }
    public static void SetPremiumAlinacakBirSonrakiSure(DateTime deger) { PlayerPrefs.SetString("PremiumAlinacakBirSonrakiSure", TARIH.Yaz(deger)); }

    public static DateTime GetPremiumBitmesineKalanSure() { return TARIH.Oku(PlayerPrefs.GetString("PremiumBitmesineKalanSure", ""), DateTime.Today.AddMinutes(2)); }
    public static void SetPremiumBitmesineKalanSure(DateTime deger) { PlayerPrefs.SetString("PremiumBitmesineKalanSure", TARIH.Yaz(deger)); }

    public static DateTime GetPremiumBirSonrakiGun() { return TARIH.Oku(PlayerPrefs.GetString("PremiumBirSonrakiGun", ""), DateTime.Now.AddDays(1)); }
    public static void SetPremiumBirSonrakiGun(DateTime deger) { PlayerPrefs.SetString("PremiumBirSonrakiGun", TARIH.Yaz(deger)); }


    public static bool GetKayitliGuneEsitVeyaGundenBuyuk(DateTime date) { return date.Date <= DateTime.Now.Date; }
    public static void PremiumSureKontrol()
    {
        DateTime temp = GetPremiumBirSonrakiGun();
        //DateTime temp = DateTime.Parse("05/29/2022 13:10:06");
        DateTime tempNow = DateTime.Now.Date;
        //DateTime tempNow = DateTime.Parse("06/3/2022 13:10:06");
       // DebugAll();

        if (temp.Date <= tempNow)
        {
            
            SetPremiumGunlukCount(5);
            SetPremiumGunlukCalisiyor(false);
            SetPremiumGunlukAlinabilir(true);
            SetPremiumBirSonrakiGun(DateTime.Now.AddDays(1));

        }
        else if (GetPremiumBitmesineKalanSure().TimeOfDay <= DateTime.Now.TimeOfDay && GetPremiumGunlukCalisiyor())
        {
            SetPremiumGunlukCalisiyor(false);
        }
        else if (GetPremiumAlinacakBirSonrakiSure().TimeOfDay <= DateTime.Now.TimeOfDay && !GetPremiumGunlukCalisiyor())
        {
            if (GetPremiumGunlukCount() != 0) SetPremiumGunlukAlinabilir(true);
        }

    }
    public static void Default()
    {
        //SetPremiumVar(false);
        SetPremiumGunlukCalisiyor(false);
        SetPremiumGunlukAlinabilir(true);
        SetPremiumGunlukCount(5);
        SetPremiumAlinacakBirSonrakiSure(DateTime.Today.AddMinutes(2));
        SetPremiumBitmesineKalanSure(DateTime.Today.AddMinutes(2));
        SetPremiumBirSonrakiGun(DateTime.Now.AddDays(1));
    }
    public static void DebugAll()
    {
        Debug.LogWarning("GetPremiumVar() " + GetPremiumVar() +
            "\n GetPremiumGunlukCalisiyor() " + GetPremiumGunlukCalisiyor() +
            "\n GetPremiumGunlukAlinabilir() " + GetPremiumGunlukAlinabilir() +
            "\n GetPremiumGunlukCount() " + GetPremiumGunlukCount() +
            "\n GetPremiumAlinacakBirSonrakiSure() " + GetPremiumAlinacakBirSonrakiSure() +
            "\n GetPremiumBitmesineKalanSure() " + GetPremiumBitmesineKalanSure() +
            "\n GetPremiumBirSonrakiGun() " + GetPremiumBirSonrakiGun().Date
            );
    }
   
 


}

