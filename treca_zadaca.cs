using System.Runtime.ConstrainedExecution;

namespace ApstraktnaTvornicaExtraPrimjer
{
    // Apstraktni proizvod A - Televizor
    public abstract class TV
    {
        public abstract void UseInterface();
    }

    // Apstraktni proizvod B - Monitor
    public abstract class Display
    {
        public abstract void UseInterface();
    }

    // Konkretni proizvod - Dell televizor
    public class DellTV : TV
    {
        public override void UseInterface()
        {
            // Implementacija Dell televizora
        }
    }

    // Konkretni proizvod - Samsung televizor
    public class SamsungTV : TV
    {
        public override void UseInterface()
        {
            // Implementacija Samsung televizora
        }
    }

    // Konkretni proizvod - Dell monitor
    public class DellDisplay : Display
    {
        public override void UseInterface()
        {
            // Implementacija Dell monitora
        }
    }

    // Konkretni proizvod - Samsung monitor
    public class SamsungDisplay : Display
    {
        public override void UseInterface()
        {
            // Implementacija Samsung monitora
        }
    }

    // Apstraktna tvornica
    public abstract class ElectronicsFactory
    {
        public abstract TV CreateTV();
        public abstract Display CreateDisplay();
    }

    // Konkretna tvornica za Dell proizvode
    public class DellFactory : ElectronicsFactory
    {
        public override TV CreateTV()
        {
            return new DellTV();
        }

        public override Display CreateDisplay()
        {
            return new DellDisplay();
        }
    }

    // Konkretna tvornica za Samsung proizvode
    public class SamsungFactory : ElectronicsFactory
    {
        public override TV CreateTV()
        {
            return new SamsungTV();
        }

        public override Display CreateDisplay()
        {
            return new SamsungDisplay();
        }
    }

    public class App
    {
        private SamsungFactory samsungFactory;
        private DellFactory dellFactory;
        private TV samsungTV;
        private Display samsungDisplay;
        private TV dellTV;
        private Display dellDisplay;

        public App()
        {
            samsungFactory = new SamsungFactory();
            dellFactory = new DellFactory();
            samsungTV = samsungFactory.CreateTV();
            samsungDisplay = samsungFactory.CreateDisplay();
            dellTV = dellFactory.CreateTV();
            dellDisplay = dellFactory.CreateDisplay();
        }

        public void SellDellTV()
        {
            // Implementacija prodaje Dell televizora
        }

        public void SellDellDisplay()
        {
            // Implementacija prodaje Dell monitora
        }

        public void SellSamsungTV()
        {
            // Implementacija prodaje Samsung televizora
        }

        public void SellSamsungDisplay()
        {
            // Implementacija prodaje Samsung monitora
        }
    }
}
//Ova implementacija koristi Abstraktnu tvornicu kako bi odvojila stvaranje objekata od njihove upotrebe.
//Time se poštuje načelo SOLID-a, posebno načelo otvorenog/zatvorenog (OCP) jer se omogućava dodavanje novih
//vrsta proizvoda bez mijenjanja postojećeg koda.