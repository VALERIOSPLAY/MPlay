using System.Threading;
using System.Windows;

namespace MPlay
{
    /// <summary>
    /// Логика взаимодействия для IntroScreen.xaml
    /// </summary>
    public partial class IntroScreen : Window
    {
        public IntroScreen()
        {
            InitializeComponent();
            delay();
        }
        void delay()
        {
            Thread.Sleep(10000);
            XUI.Text = "NOPE";
            MainWindow player = new MainWindow();
            player.Show();
            //this.Close();
        }

    }
}
