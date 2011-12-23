using System.Windows;
using System.Windows.Input;

namespace VerseMemory
{
    /// <summary>
    /// Interaction logic for EditSlideWindow.xaml
    /// </summary>
    public partial class EditSlideWindow
    {
        private Slide slideRef;
        public EditSlideWindow(Slide slide)
        {
            InitializeComponent();
            slideRef = slide;
            txtQuestion.Text = slide.question;
            txtAnswer.Text = slide.answer;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(txtQuestion);
        }

        private void btnEditSlide_Click(object sender, RoutedEventArgs e)
        {
            slideRef.question = txtQuestion.Text;
            slideRef.answer = txtAnswer.Text;
            Close();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

       
    }
}
