using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUIMainMenu : MonoBehaviour
{
    public static CanvasUIMainMenu instance;
    [SerializeField] GameObject _goStats;
    [SerializeField] CardOfStatus _resimdenYazi, _sestenYazi, _sestenResim, _yazidanResim, _besX5, _hepsi;
    [Header("Stat Button")]

    [SerializeField] MyButton _statButton;
    [SerializeField] MyButton _myBtnPremium;
    [SerializeField] TMP_Text _txtPremium;
    [SerializeField] Sprite[] _iconsOfStat;
    public GameObject _menuCategorie;
    bool _isStatOpen = false;
    public bool _premiumButtonAktif = true;
   public bool _ilkGecisPremiumdan = false;
    private void Awake()
    {
        instance = this;
        TEMP._gidilecekSahne = Sahne.MainMenu;

        if(!PREMIUM.GetPremiumVar()) PREMIUM.PremiumSureKontrol();
        _goStats.SetActive(false);
        _statButton.SetIcon(_iconsOfStat[0]);
    }

    private void Start()
    {
//        Debug.Log(GetListOfWords.FullPaket(Categories.Karisik).Count);
        

        SetButtonPremium();
    }
    private void LateUpdate()
    {
        if (_premiumButtonAktif) return;



        if (PREMIUM.GetPremiumGunlukCalisiyor())
        {
            _txtPremium.text = DoThis.GeriSayimFrom(PREMIUM.GetPremiumBitmesineKalanSure());
            _txtPremium.color = Color.green;
            PREMIUM.PremiumSureKontrol();
        }
        else if (!PREMIUM.GetPremiumGunlukAlinabilir() && PREMIUM.GetPremiumGunlukCount() > 0)
        {
            if (!_ilkGecisPremiumdan)
            {
                _ilkGecisPremiumdan = true;
                if (FindObjectOfType<UI_KATEGORI>()) FindObjectOfType<UI_KATEGORI>().HandleX();
            }
            _txtPremium.text = DoThis.GeriSayimFrom(PREMIUM.GetPremiumAlinacakBirSonrakiSure());
            _txtPremium.color = Color.yellow;
            PREMIUM.PremiumSureKontrol();

        }
        else if (PREMIUM.GetPremiumGunlukAlinabilir())
        {
            _premiumButtonAktif = true;
            SetButtonPremium();
            if (FindObjectOfType<UI_KATEGORI>()) FindObjectOfType<UI_KATEGORI>().HandleX();

        }
        else if (PREMIUM.GetPremiumGunlukCount() <= 0)
        {
            _txtPremium.text = DoThis.GeriSayimGunSonu();
            _txtPremium.color = Color.yellow;
            PREMIUM.PremiumSureKontrol();
        }
        //if (PREMIUM.GetPremiumGunlukCalisiyor())
        //{
        //    _txtPremium.text = DoThis.GeriSayimFrom(PREMIUM.GetPremiumBitmesineKalanSure());
        //    _txtPremium.color = Color.green;
        //    PREMIUM.PremiumSureKontrol();
        //}
        //else if (!PREMIUM.GetPremiumGunlukAlinabilir()&& PREMIUM.GetPremiumGunlukCount() > 0)
        //{
        //    if (!_ilkGecisPremiumdan)
        //    {
        //        _ilkGecisPremiumdan = true;
        //        if (FindObjectOfType<UI_KATEGORI>()) FindObjectOfType<UI_KATEGORI>().HandleX();
        //    }
        //    _txtPremium.text = DoThis.GeriSayimFrom(PREMIUM.GetPremiumAlinacakBirSonrakiSure());
        //    _txtPremium.color = Color.yellow;
        //    PREMIUM.PremiumSureKontrol();

        //}
        //else if (PREMIUM.GetPremiumGunlukAlinabilir())
        //{
        //    _premiumButtonAktif = true;
        //    SetButtonPremium();
        //    if (FindObjectOfType<UI_KATEGORI>()) FindObjectOfType<UI_KATEGORI>().HandleX();

        //}
        //else if (PREMIUM.GetPremiumGunlukCount() <= 0)
        //{
        //    _txtPremium.text = DoThis.GeriSayimGunSonu();
        //    _txtPremium.color = Color.yellow;
        //    PREMIUM.PremiumSureKontrol();
        //}


    }

  public  void SetButtonPremium()
    {
        if (DEMO.AKTIF)
        {
            // demo: herkes premium ama düğme görünür kalır; basınca şaka mesajlı premium paneli açılır
            _premiumButtonAktif = true;
            _txtPremium.text = "PREMIUM";
            _txtPremium.color = Color.yellow;
            _myBtnPremium.SetDurumButton(MyButton.durumButton.basilmadi);
            return;
        }
        if (PREMIUM.GetPremiumVar())
        {
            _myBtnPremium.gameObject.SetActive(false); return;
        }



        if (PREMIUM.GetPremiumGunlukCalisiyor() || !PREMIUM.GetPremiumGunlukAlinabilir())
        {
            _myBtnPremium.SetDurumButton(MyButton.durumButton.basildi);
            _premiumButtonAktif = false;
        }
        else if (PREMIUM.GetPremiumGunlukAlinabilir())
        {
            _premiumButtonAktif = true;
            _txtPremium.text = "PREMIUM\n(" + PREMIUM.GetPremiumGunlukCount() + ")";
            _txtPremium.color = Color.yellow;
            _myBtnPremium.SetDurumButton(MyButton.durumButton.basilmadi);
        }
        else
        {
            //_myBtnPremium.SetDurumButton(MyButton.durumButton.basildi);
            Debug.Log("else");

        }

    }

    public void EventPremium()
    {
        if (FindObjectOfType<btnMenuKategori>()) return;
        if (!DEMO.AKTIF && PREMIUM.GetPremiumGunlukCount() <= 0) return;
        Instantiate(GetObje.Hangi(GetObje.objeName.UI_PREMIUM));
    }
    public void HandleCikis()
    {
        if (FindObjectOfType<btnMenuKategori>()) return;
        Application.Quit();
    }

    IEnumerator SahneGecis(float delay, Sahne sahne)
    {
        yield return new WaitForSeconds(delay);
        GoToScene.Hangi(sahne);
    }


    public void EventAyarlar()
    {
        if (FindObjectOfType<btnMenuKategori>()) return;
        Instantiate(GetObje.Hangi(GetObje.objeName.UI_AYARLAR));
    }

    public void EventSatinAlma()
    {
        if (FindObjectOfType<btnMenuKategori>()) return;
        Instantiate(GetObje.Hangi(GetObje.objeName.UI_SATIN_ALMA));
    }
    public void EventUpStat()
    {
        if (FindObjectOfType<btnMenuKategori>()) return;
        if (_isStatOpen)
        {
            _statButton.SetIcon(_iconsOfStat[0]);
            _statButton.SetDurumButton(MyButton.durumButton.basilmadi);

            _goStats.SetActive(false);
            _isStatOpen = false;
        }
        else
        {
            _statButton.SetIcon(_iconsOfStat[1]);
            KayitlariCekVeAta();

            _goStats.SetActive(true);

            _isStatOpen = true;
        }
    }
    void KayitlariCekVeAta()
    {
        AllStatusOfType allStatusOfType = Kayit.GetScore(Sahne.EslestirmeResimdenYazi1x5);
        Ata(allStatusOfType, _resimdenYazi);
        allStatusOfType = Kayit.GetScore(Sahne.EslestirmeSestenYazi1x5);
        Ata(allStatusOfType, _sestenYazi);
        allStatusOfType = Kayit.GetScore(Sahne.EslestirmeSestenResim1x5);
        Ata(allStatusOfType, _sestenResim);
        allStatusOfType = Kayit.GetScore(Sahne.EslestirmeYazidanResim1x5);
        Ata(allStatusOfType, _yazidanResim);
        allStatusOfType = Kayit.GetScore(Sahne.Eslestirme5x5);
        Ata(allStatusOfType, _besX5);
        _hepsi.SetCardOfStatus(0, 0, 0, 0, allStatusOfType.ToplamHepsiDogru, allStatusOfType.ToplamHepsiYanlis);
    }
    void Ata(AllStatusOfType allStatusOfType, CardOfStatus cardOfStatus)
    {
        cardOfStatus.SetCardOfStatus(
            allStatusOfType.GunDogru,
            allStatusOfType.GunYanlis,
            allStatusOfType.HaftaDogru,
            allStatusOfType.HaftaYanlis,
            allStatusOfType.ToplamDogru,
            allStatusOfType.ToplamYanlis
            );
    }
}
