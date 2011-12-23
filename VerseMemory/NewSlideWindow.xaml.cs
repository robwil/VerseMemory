using System.Windows;
using System.Windows.Input;

namespace VerseMemory
{
    /// <summary>
    /// Interaction logic for NewSlideWindow.xaml
    /// </summary>
    public partial class NewSlideWindow
    {
        private MainWindow mainWindowRef;
        public NewSlideWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            mainWindowRef = mainWindow;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(txtQuestion);
        }

        private void btnAddSlide_Click(object sender, RoutedEventArgs e)
        {
            Slide newSlide = new Slide(txtQuestion.Text, txtAnswer.Text);
            mainWindowRef.deck.notMemorizedSlides.Add(newSlide);
            Close();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

       
    }
}
