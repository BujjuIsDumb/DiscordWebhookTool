// MIT License
//
// Copyright(c) 2022 Bujju
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Text.RegularExpressions;
using System.Windows;

namespace DiscordWebhookTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebhookPayload _payload;

        public MainWindow()
        {
            InitializeComponent();
            _payload = new WebhookPayload();
        }

        private void contentTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
            => _payload.Content = contentTextBox.Text;

        private async void sendButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(webhookTextBox.Text, "https:\\/\\/discord\\.com\\/api\\/webhooks\\/\\d{17,19}\\/.{68}"))
            {
                MessageBox.Show("The specified webhook URL was invalid. Please paste the link provided by Discord.", "Invalid Webhook URL", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(_payload.Content))
            {
                MessageBox.Show("The specified message content was empty. Please input a value.", "Empty Message Content", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var client = new WebhookClient(webhookTextBox.Text))
            {
                var response = await client.ExecuteAsync(_payload);

                if (!response.IsSuccessStatusCode)
                    MessageBox.Show($"An error has occurred:\n\n{await response.Content.ReadAsStringAsync()}", "Webhook Execution Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}