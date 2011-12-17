using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VerseMemory
{
    /// <summary>
    /// Interaction logic for DeckViewWindow.xaml
    /// </summary>
    public partial class DeckViewWindow : Window
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
