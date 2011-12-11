using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Input;

namespace VerseMemory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool showingAnswer;
        internal Deck deck;
        private Slide currentSlide;

        public MainWindow()
        {
            InitializeComponent();
            showingAnswer = false;
            txtAnswer.Visibility = Visibility.Hidden;
            lblQuestion.Visibility = Visibility.Visible;

            deck = new Deck();

            LoadSlidesFromFile();

            UpdateScreenText();
        }

        private void OnKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SwapVisibilities();
                if (showingAnswer == false)
                    AdvanceSlide();
            }
            else if (e.Key == Key.N)
            {
                NewSlideWindow w = new NewSlideWindow(this);
                w.ShowDialog();
                HandleNewSlides();
            }
            else if (e.Key == Key.I)
            {
                ImportFileWindow w = new ImportFileWindow();
                w.ShowDialog();
                HandleNewSlides();
            }
            else if (e.Key == Key.M)
            {
                ToggleMemorized();
            }
            else if (e.Key == Key.R)
            {
                ResetDecks();
            }
            else if (e.Key == Key.S)
                SaveSlidesToFile();
            else if (e.Key == Key.Escape)
            {
                // TODO: Save state to disk
                Close();
            }
            UpdateScreenText();
        }

        private void ToggleMemorized()
        {
            if (currentSlide.isMemorized)
            {
                deck.remainingSlides.Remove(currentSlide);
                deck.notMemorizedSlides.Add(currentSlide);
            }
            else
            {
                deck.notMemorizedSlides.Remove(currentSlide);
                deck.remainingSlides.Add(currentSlide);
            }
            currentSlide.isMemorized = !currentSlide.isMemorized;
            UpdateScreenText();
        }

        private void ResetDecks()
        {
            for (int i = deck.finishedSlides.Count - 1; i >= 0; i--)
            {
                Slide slide = deck.finishedSlides[i] as Slide;
                if (slide != null && slide.isMemorized)
                {
                    deck.remainingSlides.Add(slide);
                }
                else
                {
                    deck.notMemorizedSlides.Add(slide);
                }

                deck.finishedSlides.RemoveAt(i);
            }
            HandleNewSlides();
        }

        private void HandleNewSlides()
        {
            // Sort the decks by weight to handle latest additions
            deck.remainingSlides.Sort();
            deck.finishedSlides.Sort();
            deck.notMemorizedSlides.Sort();

            // If no slide is currently being displayed, but we have stuff to display. Show the next available slide.
            if (currentSlide == null && (deck.remainingSlides.Count + deck.notMemorizedSlides.Count) > 0)
                ShowNextAvailableSlide();
        }

        private void AdvanceSlide()
        {
            // handle normal case, where card is memorized
            if (currentSlide.isMemorized == true)
            {
                // Add slide to Finished deck.
                deck.finishedSlides.Add(currentSlide);

                // Remove slide from wherever it came from. Could be either of the two decks, so do both.
                deck.remainingSlides.Remove(currentSlide);
                deck.notMemorizedSlides.Remove(currentSlide);
            }

            // If not memorized, don't even move it
            //

            // Get next available card and show it to the screen.
            ShowNextAvailableSlide();
            UpdateScreenText();
        }

        private void SwapVisibilities()
        {
            if (showingAnswer)
            {
                txtAnswer.Visibility = Visibility.Hidden;
                lblQuestion.Visibility = Visibility.Visible;
                showingAnswer = false;
            } else
            {
                txtAnswer.Visibility = Visibility.Visible;
                lblQuestion.Visibility = Visibility.Hidden;
                showingAnswer = true;
            }
        }

        /**
         * Take care of displaying the correct text on the screen.
         * This includes the up-to-date status text, as well as the current slide.
         */
        private void UpdateScreenText()
        {
            lblStatus.Content = "Remaining: " + deck.remainingSlides.Count + "\tFinished: " + deck.finishedSlides.Count +
                                "\t Not Memorized: " + deck.notMemorizedSlides.Count;

            if (currentSlide == null)
            {
                lblSlideStats.Content = "No slide loaded. Press R put all slides into Remaining deck.";
                lblQuestion.Content = "";
                txtAnswer.Text = "";
            }
            else
            {
                lblSlideStats.Content = "Weight: " + currentSlide.weight + "\tMemorized: " + currentSlide.isMemorized;
                lblQuestion.Content = currentSlide.question;
                txtAnswer.Text = currentSlide.answer;
            }
        }

        /**
         * Pulls next card off Remaining deck, or Not Memorized deck, if available.
         * It makes currentSlide equal to this card.
         */
        private void ShowNextAvailableSlide()
        {
            if (deck.remainingSlides.Count > 0)
            {
                currentSlide = deck.remainingSlides[0] as Slide;
            }
            else if (deck.notMemorizedSlides.Count > 0)
            {
                currentSlide = deck.notMemorizedSlides[0] as Slide;
            }
            else
            {
                currentSlide = null;
            }
        }

        private void LoadSlidesFromFile()
        {
            using (FileStream fileStream = new FileStream("slides.js", FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (fileStream.Length == 0)
                    return;
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Deck));
                deck = serializer.ReadObject(fileStream) as Deck;
            }
        }

        private void SaveSlidesToFile()
        {
            using (FileStream fileStream = new FileStream("slides.js", FileMode.Create, FileAccess.Write))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Deck));
                serializer.WriteObject(fileStream, deck);
            }
        }
    }
}
