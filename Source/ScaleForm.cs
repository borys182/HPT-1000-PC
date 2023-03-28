using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPT1000.Source.Driver;

namespace HPT1000.Source
{
    class ScaleForm
    {
        Form    form        = null;  ///<Refernecja na formtke ktora podlega skalowaniu 

        double  oldWidth    = 0;   ///< Zmienna przechowuje poprzednia wartosc szerokosci formatki. Wymagane do obvliczenia procentwej zmiany rozmaru formy
        double  oldHeight   = 0;    ///< Zmienna przechowuje poprzednia wartosc wysokosaci formatki. Wymagane do obvliczenia procentwej zmiany rozmaru formy

        bool    makeScale   = false;     ///<Flaga podaje informacje czy klasa jest w trkacie skalowania formatki

        List<TabPage> pages = new List<TabPage>(); ///< Z uwagi na fakt ze niektore tabpage sa dodawane /usuwane z pagecontrol to aby je poprawnie skalowac musze stworzyc dla nich oddzielna liste
    
        //---------------------------------------------------------------------------
        /**
         * Konstruktor
         */ 
        public ScaleForm()
        {
            makeScale = false;
        }
        //---------------------------------------------------------------------------
        /**
            Metoda ma za zadanie ustawienie wskaznika formularza oraz uzupelnienie listy komponentow formy w liscie
        */
        public void SetForm(Form aForm)
        {
            if (aForm != null)
            {
                form = aForm;
                if (aForm != null)
                {
                    //Inicjalizacja rozmiaru formatki
                    oldWidth  = aForm.Width;
                    oldHeight = aForm.Height;
                }
            }
        }
        //---------------------------------------------------------------------------
        /**
            Metoda dodaje do listy tbaPage koljene Tab-y ktroe maja byc skalowane pomimo tego ze sa usuwane w czasie dzialania aplikacji
        */
        public void AddPageTab(TabPage page)
        {
            pages.Add(page);
        }
        //---------------------------------------------------------------------------
        /**
            Metoda skaluje forme biorac pod uwage zmiane szerokosci/wysokosci. Skalowaniu sa poddawane komponety formy jak i jej rozmiar
        */
        public void Scale()
        {
            //Ustaw flage ze skalowanie zostalo rozpoczete
            makeScale = true;
            //Wykonaj skalowanie formatki
            ScaleWidth();
            ScaleHeight();
            //ustaw flage zakonczenia skalowania
            makeScale = false;
        }
        //---------------------------------------------------------------------------
        /**
            Metoda jest odpowiedzialna za skalowanie formatki w sytuacji gdy zmianie ulegla szerokosc okna
        */
        void ScaleWidth()
        {
            //Sprawdz czy szerokosc okna zostala zmieniona jezeli tak to wykonaj slaowanie szerokosci okna
            if (form != null && oldWidth != form.Width)
            {
                //Oblicz procent zmian wielkosci formatki
                double aPercent = 0;
                if (oldWidth != 0)
                    aPercent = form.Width / oldWidth;  //Od obliczonego procentu odejmuje 1 aby uzyskac procent zmiany od 0 a nie procentowa zawartosc jednej liczby w drugiej
                                                       //Skaluj komponety
                MakeScaleComponent(aPercent, Types.TypeScalible.Width);
                //Aktualizuj stara wartosc
                oldWidth = form.Width;
            }
       }
        //---------------------------------------------------------------------------
        void ScaleHeight()
        {
            if (form != null && oldHeight != form.Height)
            {
                //Oblicz procent zmian wielkosci formatki
                double aPercent = 0;
                if (oldHeight != 0)
                    aPercent = form.Height / oldHeight; //Od obliczonego procentu odejmuje 1 aby uzyskac procent zmiany od 0 a nie procentowa zawartosc jednej liczby w drugiej
                                                        //Skaluj komponety
                MakeScaleComponent(aPercent, Types.TypeScalible.Heigh);
                //Aktualizuj stara wartosc
                oldHeight = form.Height;
            }
        }
        //---------------------------------------------------------------------------
        /**
            Metoda ma za zadanie skalowania komponentow na formatce. Skaowaniu podlegaja: Size , Font oraz Left/Top (zachaczenie)
        */
        void MakeScaleComponent(double aPercent, Types.TypeScalible aType)
        {
            if (form != null)
            {
                for (int i = 0; i < form.Controls.Count; i++)
                    MakeScaleComponent(form.Controls[i], aPercent, aType);
            }
            for (int i = 0; i < pages.Count; i++)
                MakeScaleComponent(pages[i], aPercent, aType);
        }
        //---------------------------------------------------------------------------
        /**
            Metoda ma za zadanie skalowania komponentow na formatce. Skaowaniu podlegaja: Size , Font oraz Left/Top (zachaczenie)
        */
        void MakeScaleComponent(Control aControl, double aPercent, Types.TypeScalible aTypeScale)
        {
            //Wyskaluj rodzica
            ScaleComponent(aControl, aPercent, aTypeScale);
            //Wyskaluj dzieci
            //Nie skaluj dzieci dla PageContorlProcess poniewaz dzieci sa dodane do oddzielnej listy z racji tego ze w czasie dzilania programy moga byc dodawane/usuwane
            if(aControl.Name != "tabControlProcess")
                for (int i = 0; i < aControl.Controls.Count; i++)
                    MakeScaleComponent(aControl.Controls[i], aPercent, aTypeScale);
        }
        //---------------------------------------------------------------------------
        /**
            Metoda ma za zadanie skalowania komponentow na formatce. Skaowaniu podlegaja: Size , Font oraz Left/Top (zachaczenie)
        */
        void ScaleComponent(Control control, double aPercent, Types.TypeScalible aType)
        {
            if (control != null)
            {
                if (aPercent != 0)
                {
                    //Oblicz nowe wartosci
                    if (aType == Types.TypeScalible.Width)
                    {
                        control.Left = Convert.ToInt32(control.Left * aPercent);
                        control.Width = Convert.ToInt32(control.Width * aPercent);
                    }
                    if (aType == Types.TypeScalible.Heigh)
                    {
                        control.Top = Convert.ToInt32(control.Top * aPercent);
                        control.Height = Convert.ToInt32(control.Height * aPercent);
                    }
                }
            }
            //Jezeli jestem PageControlem to ustaw poprawna wartosc szerokosci/wysokosci tabsheta
            SetWidhtTabSheetInNewPageControl(control, aPercent,aType);
        }
        //---------------------------------------------------------------------------
        /**
            Metoda ma za zadanie znalezienie odpowiedniego rozmiaru czcionki dla komponetu w zaleznosci od jego rozmiarow
        */
        void SetWidhtTabSheetInNewPageControl(Control aControl, double aPercent,Types.TypeScalible aType)
        {
            if (aControl != null)
            {
                if (aControl.GetType() == typeof(TabControl))
                {
                    TabControl aPageControl = (TabControl)aControl;
                    if (aType == Types.TypeScalible.Width)
                        aPageControl.ItemSize = new System.Drawing.Size(Convert.ToInt32(aPageControl.ItemSize.Width * aPercent), aPageControl.ItemSize.Height);
             //       if(aPageControl.Alignment == TabAlignment.Right && aType == Types.TypeScalible.Heigh)
             //           aPageControl.ItemSize = new System.Drawing.Size(aPageControl.ItemSize.Width,Convert.ToInt32(aPageControl.ItemSize.Height * aPercent));
                }
            }
        }
        //---------------------------------------------------------------------------
        /**
            Podaj informacje czy procedura skalowania jest aktualnie wykonywana
        */
        bool IsMakeScale()
        {
            return makeScale;
        }
        //------------------------------------------------------------------------------
    }
}

