using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kayit : MonoBehaviour
{
    const string BUTUN_HEPSI_DOGRU = "butun hepsi dogru";
    const string BUTUN_HEPSI_YANLIS = "butun hepsi yanlis";

    const string TOPLAM_DOGRU = "toplam dogru";
    const string TOPLAM_YANLIS = "toplam yanlis";

    const string HAFTA_DOGRU = "hafta dogru";
    const string HAFTA_YANLIS = "hafta yanlis";

    const string GUN_DOGRU = "gun dogru";
    const string GUN_YANLIS = "gun yanlis";




     static int ButunHepsiDogru() { return PlayerPrefs.GetInt(BUTUN_HEPSI_DOGRU, 0); }
     static int ButunHepsiYanlis() { return PlayerPrefs.GetInt(BUTUN_HEPSI_YANLIS, 0); }


     static int HepsiDogru(Sahne sahne) { return PlayerPrefs.GetInt(TOPLAM_DOGRU + sahne.ToString(), 0); }
     static int HepsiYanlis(Sahne sahne) { return PlayerPrefs.GetInt(TOPLAM_YANLIS + sahne.ToString(), 0); }


     static int HaftaDogru(Sahne sahne) { return PlayerPrefs.GetInt(HAFTA_DOGRU + sahne.ToString(), 0); }
     static int HaftaYanlis(Sahne sahne) { return PlayerPrefs.GetInt(HAFTA_YANLIS + sahne.ToString(), 0); }


     static int GunDogru(Sahne sahne) { return PlayerPrefs.GetInt(GUN_DOGRU + sahne.ToString(), 0); }
     static int GunYanlis(Sahne sahne) { return PlayerPrefs.GetInt(GUN_YANLIS + sahne.ToString(), 0); }
    public static void ResetGun()
    {
        ResetFromKey(GUN_DOGRU);
        ResetFromKey(GUN_YANLIS);
    }
    public static void ResetHafta()
    {
        ResetFromKey(HAFTA_DOGRU);
        ResetFromKey(HAFTA_YANLIS);
    }
    static void ResetFromKey(string key)
    {
        PlayerPrefs.SetInt(key + Sahne.Eslestirme5x5.ToString(), 0);
        PlayerPrefs.SetInt(key + Sahne.EslestirmeResimdenYazi1x5.ToString(), 0);
        PlayerPrefs.SetInt(key + Sahne.EslestirmeSestenResim1x5.ToString(), 0);
        PlayerPrefs.SetInt(key + Sahne.EslestirmeSestenYazi1x5.ToString(), 0);
        PlayerPrefs.SetInt(key + Sahne.EslestirmeYazidanResim1x5.ToString(), 0);
    }
    public static void Dogru(Sahne sahne)
    {
        PlayerPrefs.SetInt(GUN_DOGRU + sahne.ToString(), GunDogru(sahne)+1);
        PlayerPrefs.SetInt(HAFTA_DOGRU + sahne.ToString(), HaftaDogru(sahne)+1);
        PlayerPrefs.SetInt(TOPLAM_DOGRU + sahne.ToString(), HepsiDogru(sahne)+1);
        PlayerPrefs.SetInt(BUTUN_HEPSI_DOGRU,ButunHepsiDogru()+1);
    }
    public static void Yanlis(Sahne sahne)
    {
        PlayerPrefs.SetInt(GUN_YANLIS + sahne.ToString(), GunYanlis(sahne)+1);
      PlayerPrefs.SetInt(HAFTA_YANLIS + sahne.ToString(), HaftaYanlis(sahne)+1);
      PlayerPrefs.SetInt(TOPLAM_YANLIS + sahne.ToString(), HepsiYanlis(sahne)+1);
      PlayerPrefs.SetInt(BUTUN_HEPSI_YANLIS, ButunHepsiYanlis()+1);
    }


    public static AllStatusOfType GetScore(Sahne sahne)
    {
        AllStatusOfType temp = new AllStatusOfType();
        temp.ToplamHepsiDogru = ButunHepsiDogru();
        temp.ToplamHepsiYanlis = ButunHepsiYanlis();

        temp.ToplamDogru = HepsiDogru(sahne);
        temp.ToplamYanlis = HepsiYanlis(sahne);

        temp.HaftaDogru = HaftaDogru(sahne);
        temp.HaftaYanlis = HaftaYanlis(sahne);

        temp.GunDogru = GunDogru(sahne);
        temp.GunYanlis = GunYanlis(sahne);

        return temp;
    }


    public static bool IsYeniGun()
    {
        if (PlayerPrefs.GetString("YENIGUN") == string.Empty)
        {
            PlayerPrefs.SetString("YENIGUN", TARIH.Yaz(TimeNow().AddDays(1)));
            return true;
        }
        else if (TARIH.Oku(PlayerPrefs.GetString("YENIGUN"), DateTime.MinValue).Date <= TimeNow().Date)
        {
            PlayerPrefs.SetString("YENIGUN", TARIH.Yaz(TimeNow().AddDays(1)));
            return true;
        }
        return false;
    }

    public static bool IsYeniHafta()
    {

        if (PlayerPrefs.GetString("YENIHAFTA") == string.Empty)
        {
            int i = Convert.ToInt32(TimeNow().DayOfWeek);

            i = 7 - i;
            PlayerPrefs.SetString("YENIHAFTA", TARIH.Yaz(TimeNow().AddDays(i)));
            return true;
        }
        else if (TARIH.Oku(PlayerPrefs.GetString("YENIHAFTA"), DateTime.MinValue).Date < TimeNow().Date)
        {
            int i = Convert.ToInt32(TimeNow().DayOfWeek);
            i = 7 - i;
            PlayerPrefs.SetString("YENIHAFTA", TARIH.Yaz(TimeNow().AddDays(i)));
            return true;
        }
        return false;




    }
    public static bool GetSesAcik() {return PlayerPrefs.GetInt("sesAcik",1)==1; }
    public static void SetSesAcik(bool deger) { PlayerPrefs.SetInt("sesAcik",deger?1:0); }
    static DateTime TimeNow()
    {
        DateTime simdi = DateTime.Now;
        //string zaman = "2/20/2022 2:00:04 PM";
        //simdi = DateTime.Parse(zaman);
        //DEBUG_GAME.instance.Yazdir("DateTime.Now: " + simdi, 1);
        return simdi;
    }
}

public class AllStatusOfType
{
    public int GunDogru;
    public int GunYanlis;
    public int HaftaDogru;
    public int HaftaYanlis;
    public int ToplamDogru;
    public int ToplamYanlis;
    public int ToplamHepsiDogru;
    public int ToplamHepsiYanlis;
}
