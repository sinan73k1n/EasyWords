using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class AYARLAR : MonoBehaviour
{
  
    public static bool GetSesAcik() { return PlayerPrefs.GetInt("sesAcik", 1) == 1; }
    public static void SetSesAcik(bool acik) { PlayerPrefs.SetInt("sesAcik", acik ? 1 : 0); }

    public static bool GetReklamVar() { if (DEMO.AKTIF) return false; return PlayerPrefs.GetInt("reklamVar", 1) == 1; }
    public static void SetReklamVar(bool var) { PlayerPrefs.SetInt("reklamVar", var ? 1 : 0); }

   public static void Default() { SetSesAcik(true); }
}

