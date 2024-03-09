using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proyecto_Inf_131
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateKey(object sender, RoutedEventArgs e)
        {
            GenerateKeysRSA generateKeysRSA = new GenerateKeysRSA();

            long primoP = generateKeysRSA.XPrimo(long.Parse(NprimoP.Text));
            long primoQ = generateKeysRSA.XPrimo(long.Parse(NprimoQ.Text));

            long modulo = generateKeysRSA.ModKey(primoP, primoQ);
            long phi = generateKeysRSA.GenPhi(primoP, primoQ);

            long coprime = generateKeysRSA.GenCoPrimo((int)(modulo/2));
            long invModule = generateKeysRSA.InvModular(coprime, phi);

            while( invModule == 0)
            {
                coprime = generateKeysRSA.GenCoPrimo((int)(modulo/2));
                invModule = generateKeysRSA.InvModular(coprime,phi);
            }

            PublicKey.Text = coprime.ToString()+" - "+modulo.ToString();
            PrivateKey.Text = invModule.ToString() + " - " + modulo.ToString();
        }

        private void ClearScreen(object sender, RoutedEventArgs e)
        {
            NprimoP.Text = string.Empty;
            NprimoQ.Text = string.Empty;
            PublicKey.Text = string.Empty;
            PrivateKey.Text = string.Empty;
        }
    }
}
