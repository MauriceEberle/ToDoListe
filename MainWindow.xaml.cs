using System;
using System.Collections.Generic;
using System.IO;
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

        public override string ToString()
        {
            return $"{IsChecked.ToString()}~{Text}";
        }
    }
    public partial class MainWindow : Window
    {
        public List<Listenobjekt> ListenObjekte = new List<Listenobjekt>()
        {
        };
        public string path = @"..\..\Speicher\Speicher.txt";
        public MainWindow()
        {
            InitializeComponent();
            GetToDos();
            ToDoBox.ItemsSource = ListenObjekte;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (text_input.Text.Length > 3)
            {
                string input = text_input.Text;
                var newListenobjekt = new Listenobjekt(false, input);
                ListenObjekte.Add(newListenobjekt);
                text_input.Text = "";
                Update();
            }
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            int index = ToDoBox.SelectedIndex;

            if (index != -1)
            {
                ListenObjekte.RemoveAt(index);
                text_input.Text = "";
                Update();
            }

        }

        private void btn_deleteAll_Click(object sender, RoutedEventArgs e)
        {

            for (int i = ListenObjekte.Count() - 1; i >= 0; i--)
            {
                if (ListenObjekte[i].IsChecked)
                {
                    ListenObjekte.RemoveAt(i);
                }
            }
            Update();
        }

        private void btn_change_Click(object sender, RoutedEventArgs e)
        {
            int index = ToDoBox.SelectedIndex;
            if (index != -1)
            {
                ListenObjekte[index].Text = text_input.Text;
            }
            text_input.Text = "";
            Update();
        }
        private void Update()
        {
            ToDoBox.Items.Refresh();
            using (StreamWriter sw = File.CreateText(path))
            {
                foreach (var listenObjekt in ListenObjekte)
                {
                    sw.WriteLine(listenObjekt.ToString());
                }
            }
        }

        private void GetToDos()
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string toDoContent;
                while ((toDoContent = sr.ReadLine()) != null)
                {
                    string[] newList = toDoContent.Split('~');
                    bool isChecked = Convert.ToBoolean(newList[0]);
                    var listenObjekt = new Listenobjekt(isChecked, newList[1]);
                    ListenObjekte.Add(listenObjekt);
                }
            }
        }

        private void ToDoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ToDoBox.SelectedIndex;
            if (index != -1)
            {
                text_input.Text = ListenObjekte[index].Text;
            }
        }
    }
}
