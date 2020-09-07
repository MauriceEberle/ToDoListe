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
}
