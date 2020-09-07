using Newtonsoft.Json;
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
    public partial class MainWindow : Window
    {
        public List<Listenobjekt> ListenObjekte = new List<Listenobjekt>();

        public string path = @"..\..\Speicher\Speicher.json";
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

            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(path))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, ListenObjekte);
                }
            }
        }

        private void GetToDos()
        {
            string json;
            using (StreamReader sr = new StreamReader(path))
            {
                json = sr.ReadToEnd();
            }
            ListenObjekte = JsonConvert.DeserializeObject<List<Listenobjekt>>(json);
            if(ListenObjekte == null)
            {
                ListenObjekte = new List<Listenobjekt>();
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
    public static class ToDoMethods
    {

    }
}
