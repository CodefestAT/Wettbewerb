// --------------------------------------------------------------------------------
// Copyright(C) 2016 Andreas Jakl, https://twitter.com/andijakl
// https://github.com/andijakl/speak-clearly
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
// --------------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using Windows.Media.SpeechRecognition;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace SpeakClearly
{
    /// <summary>
    /// "Pronounciation test" using the Windows 10 continuous dictation API.
    /// Speak any text for 7 seconds, and then the app will tell you the confidence
    /// how well it understood you, as well as which text the app thinks you said.
    /// Try to improve your score and battle with your friends!
    /// 
    /// This is the starting template - take a look at the new 
    /// Windows 10 Continuous Dictation API to complete this app!
    /// 
    /// For more information, read the documentation here:
    /// https://msdn.microsoft.com/en-us/library/windows/apps/mt185602.aspx
    /// 
    /// A speech recognition example is available from GitHub:
    /// https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/SpeechRecognitionAndSynthesis
    /// 
    /// Download the solution starting mid-January 2016 from:
    /// https://github.com/andijakl/speak-clearly
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // TODO: The speech recognizer used by the app for the continuous recognition session.

        // Speech events may originate from a thread other than the UI thread.
        // Keep track of the UI thread dispatcher so that we can update the
        // UI in a thread-safe manner.
        private CoreDispatcher _dispatcher;

        /// <summary>
        /// Countdown timer to stop recognition after a certain time.
        /// </summary>
        private Timer _countdownTimer;

        /// <summary>
        /// Saves the text that has been recognized so far.
        /// </summary>
        private readonly StringBuilder _recognizedText = new StringBuilder();

        /// <summary>
        /// Stores the sum of all confidence scores. Dividing it by the _numSegments
        /// will result in the average score.
        /// </summary>
        private double _averageScore;

        /// <summary>
        /// Number of text segments received so far.
        /// </summary>
        private int _numSegments;

        /// <summary>
        /// Count how many seconds the app has been listening to inform the user.
        /// </summary>
        private int _secondsSpeaking;

        /// <summary>
        /// Configuration variable how many seconds the app should listen
        /// </summary>
        private const int MaxSeconds = 7;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Note: to keep the amount of demo code to a minimum,
            // this example does not currently handle multitasking or OnNavigatedFrom events.

            // Get UI dispatcher for accessing the UI from the background thread
            // Callbacks from the speech recognition API will happen in a background thread.
            _dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;

            // TODO: Initialize the speech recognizer


            // TODO: Compile the speech recognizer constraints using the default


            // TODO: Register for the callback when a result of the Continuous Recognition Session was generated.
            // In this method, we need to save the result text and confidence score.
            // If the user briefly pauses speaking, there will be multiple callbacks to this method during
            // the 7 seconds, each containing the current fragment of text.


            // TODO: Register for the callback when the Continuous Recognition Session completes.
            // In the best case, this happens because our app stopped it after 7 seconds.
            // However, it can also happen if there is a timeout (the user doesn't speak) or if there
            // is a configuration issue.

        }

        /// <summary>
        /// UI button to start a new test was clicked.
        /// If no test is currently running, this resets the statistics and UI, starts the timer and
        /// a new continuous speech recognition session.
        /// </summary>
        private async void BtnStartTest_Click(object sender, RoutedEventArgs e)
        {
            if (_countdownTimer == null)
            {
                // Reset
                _numSegments = 0;
                _averageScore = 0.0;
                _secondsSpeaking = 0;
                _recognizedText.Clear();

                // Setup UI
                TxtScore.Text = "Listening... please speak for " + MaxSeconds + " seconds!";

                // TODO: Start continuous recognition session


                // Start timer
                _countdownTimer = new Timer(CountdownTimerCallback, null, 0, 1000);
            }
        }

        /// <summary>
        /// TODO: Intermediate speech recognition result received.
        /// Store the confidence so that we can calculate the average at the end.
        /// Also store the text that was recognized in this result segment.
        /// </summary>


        /// <summary>
        /// TODO: Callback when the continuous speech recognition session completed.
        /// In the ideal case, this is because the app stopped the session after 7 seconds.
        /// In that case, calculate the average score and show this as well as the recognized text
        /// to the user.
        /// In case the status is an error state, show the error to the user.
        /// </summary>



        /// <summary>
        /// Periodic callback from the countdown timer - called once per second until
        /// the method resets the timer and stops the continuous speech recognition.
        /// </summary>
        private async void CountdownTimerCallback(object state)
        {
            // Count the seconds the user has been speaking already
            _secondsSpeaking++;
            Debug.WriteLine("Timer callback: " + _secondsSpeaking);
            if (_secondsSpeaking >= MaxSeconds)
            {
                // Stop the timer
                ResetTimer();
                // TODO: Stop recognition
                
            }
            else
            {
                // Keep the user up to date about how many seconds are left to speak
                await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    TxtScore.Text = "Listening... please speak for " + (MaxSeconds - _secondsSpeaking) + " more seconds!";
                });
            }
        }

        /// <summary>
        /// If the timer is active, stops it and sets its member variable to null.
        /// </summary>
        private void ResetTimer()
        {
            if (_countdownTimer == null) return;
            _countdownTimer.Dispose();
            _countdownTimer = null;
        }
    }
}
