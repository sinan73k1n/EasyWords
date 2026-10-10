using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UI_PREMIUM : MonoBehaviour
{
    [Header("Ayarlar")]
    [SerializeField] float _delay = 0.4f;
    [SerializeField] float _delayYokEt = 0.4f;
    [Header("Tanimlanacaklar")]
    [SerializeField] MyButton _myBtnPremium;
    [SerializeField] TMP_Text _txtPremium, _txtHeader;

    int _count;
    bool _reklamVar;
    bool _premiumGunlukCalisiyor;
    bool _premiumGunlukAlinabilir;

    bool _myButtonAktif = true;
    private void Awake()
    {
        if (!DEMO.AKTIF) PREMIUM.PremiumSureKontrol();

        if ((TEMP._gidilecekSahne == Sahne.Harfler || TEMP._gidilecekSahne == Sahne.Sayilar ||
            TEMP._secilenCategorie == Categories.Karisik || PREMIUM.GetPremiumVar() || PREMIUM.GetPremiumGunlukCalisiyor())
            && SceneManager.GetActiveScene().name != Sahne.MainMenu.ToString())
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        AdControl.instance.ShowBanner();

        IlkAyar();
    }
    private void IlkAyar()
    {
        if (DEMO.AKTIF)
        {
            // demo: düğme hep basılabilir, basınca şaka mesajı çıkar (gerçek reklam yok)
            _myButtonAktif = true;
            _txtPremium.text = "AD for Premium";
            _txtPremium.color = Color.green;
            _txtHeader.text = "PREMIUM ON";
            _myBtnPremium.SetDurumButton(MyButton.durumButton.basilmadi);
            return;
        }
        PREMIUM.PremiumSureKontrol();
        _count = PREMIUM.GetPremiumGunlukCount();
        _reklamVar = AYARLAR.GetReklamVar();
        _premiumGunlukCalisiyor = PREMIUM.GetPremiumGunlukCalisiyor();
        _premiumGunlukAlinabilir = PREMIUM.GetPremiumGunlukAlinabilir();


        if (_premiumGunlukCalisiyor)
        {
            _myButtonAktif = false;
            _myBtnPremium.SetDurumButton(MyButton.durumButton.aktifDegil);
        }

        else if (_count > 0 && !_premiumGunlukCalisiyor && _premiumGunlukAlinabilir)
        {
            _myButtonAktif = true;
            string ads = _reklamVar ? "AD for Premium" : "Premium";
            _txtPremium.text = $"{ads} ({_count})";
            _txtPremium.color = Color.green;

            _myBtnPremium.SetDurumButton(MyButton.durumButton.basilmadi);
        }
        else if (_count > 0 && !_premiumGunlukCalisiyor && !_premiumGunlukAlinabilir)
        {

            _myBtnPremium.SetDurumButton(MyButton.durumButton.aktifDegil);
            _myButtonAktif = false;

        }
        else
        {
            _myBtnPremium.SetDurumButton(MyButton.durumButton.aktifDegil);

            _myButtonAktif = false;
        }

    }
    private void Update()
    {
        if (DEMO.AKTIF) return;
        if (_myButtonAktif) return;
        if (PREMIUM.GetPremiumGunlukAlinabilir()) return;
        if (PREMIUM.GetPremiumGunlukCount() <= 0)
        {
            _txtPremium.text = DoThis.GeriSayimGunSonu();

            if (DateTime.Parse("23:46:59") < DateTime.Parse(_txtPremium.text)) IlkAyar();
        }
        else
        {
            if (PREMIUM.GetPremiumGunlukCalisiyor())
            {
                _txtPremium.text = DoThis.GeriSayimFrom(PREMIUM.GetPremiumBitmesineKalanSure());
                _txtPremium.color = Color.green;
            }
            else if (!PREMIUM.GetPremiumGunlukCalisiyor() && !PREMIUM.GetPremiumGunlukAlinabilir())
            {
                _txtPremium.text = DoThis.GeriSayimFrom(PREMIUM.GetPremiumAlinacakBirSonrakiSure());
                _txtPremium.color = Color.yellow;

            }
            else
            {
                IlkAyar();
                Debug.Log("asd");
            }

            if (PREMIUM.GetPremiumGunlukCalisiyor()) _txtHeader.text = "PREMIUM ON"; else _txtHeader.text = "PREMIUM OFF";

        }

        PREMIUM.PremiumSureKontrol();
        if (PREMIUM.GetPremiumGunlukAlinabilir()) IlkAyar();
        if (PREMIUM.GetPremiumGunlukCount() == 5) { IlkAyar(); }

    }


    public void EvetntMainMenu()
    {
        AdControl.instance.CloseBanner();
        if (DoThis.GetMyButtonFromScene("premium"))
        {
            

            CanvasUIMainMenu.instance.SetButtonPremium();
            Destroy(gameObject);
        }
        else StartCoroutine(AnaMenuyeGit(_delay));
    }

    public void EvetnPremium()
    {
        if (!_myButtonAktif) return;
        if (DEMO.AKTIF)
        {
            DEMO.MesajReklamIzle();
            _myBtnPremium.SetDurumButton(MyButton.durumButton.basilmadi);
            return;
        }
        if (_reklamVar)
        {
            AdControl.instance.ShowOdul();
        }
        else 
        {
            SureliPremiumVer();
        }


    }

  public  void SureliPremiumVer()
    {
        AdControl.instance.CloseBanner();

        DateTime temp = DateTime.Now.AddMinutes(5f);
        PREMIUM.SetPremiumBitmesineKalanSure(temp);
        temp = DateTime.Now.AddMinutes(15f);

        //DateTime temp = DateTime.Now.AddSeconds(10f);
        //PREMIUM.SetPremiumBitmesineKalanSure(temp);
        //temp = DateTime.Now.AddSeconds(15f);

        PREMIUM.SetPremiumAlinacakBirSonrakiSure(temp);
        int tempInt = PREMIUM.GetPremiumGunlukCount();
        tempInt--;
        PREMIUM.SetPremiumGunlukCount(tempInt);
        _count = tempInt;
        PREMIUM.SetPremiumGunlukCalisiyor(true);
        PREMIUM.SetPremiumGunlukAlinabilir(false);
//        PREMIUM.DebugAll();
        if (CanvasUI.instance) CanvasUI.instance.SetSureliPremium(true);
        if (DoThis.GetMyButtonFromScene("premium")) DoThis.GetMyButtonFromScene("premium").SetDurumButton(MyButton.durumButton.basildi);
        if (CanvasUIMainMenu.instance)
        {
            CanvasUIMainMenu.instance._premiumButtonAktif = false;
            CanvasUIMainMenu.instance._ilkGecisPremiumdan = false;
        }
        Invoke(nameof(YokEt), _delayYokEt);
    }


    void YokEt()
    {
        

        Destroy(gameObject);
    }
    IEnumerator AnaMenuyeGit(float delay)
    {

        yield return new WaitForSeconds(delay);
        GoToScene.Hangi(Sahne.MainMenu);
    }

}
