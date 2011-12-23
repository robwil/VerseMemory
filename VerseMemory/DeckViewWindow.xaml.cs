using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace VerseMemory
{
    /// <summary>
    /// Interaction logic for DeckViewWindow.xaml
    /// </summary>
    public partial class DeckViewWindow
    {
        private readonly List<Slide> slides;

        public DeckViewWindow(List<Slide> slides )
        {
            this.slides = slides;
            InitializeComponent();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            foreach (Slide slide in slides)
                lstDeckView.Items.Add(slide);
        }
    }
}
