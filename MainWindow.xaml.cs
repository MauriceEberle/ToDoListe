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

namespace ToDoListe
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public class Listenobjekt
    {
        public Listenobjekt(bool isChecked, string text)
        {
            IsChecked = isChecked;
            Text = text;
        }

        public bool IsChecked { get; set; }
        public string Text { get; set; }
    }
    public partial class MainWindow : Window
    {
        public List<Listenobjekt> listenObjekte = new List<Listenobjekt>()
        {
            new Listenobjekt(false, "Blablabla")               
        };
        public MainWindow()
        {
            InitializeComponent();

            ToDoBox.ItemsSource = listenObjekte;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            string input = text_input.Text;
            var newListenobjekt = new Listenobjekt(false, input);
            listenObjekte.Add(newListenobjekt);
            ToDoBox.Items.Refresh();
            text_input.Text = "";
        }
    }
}
