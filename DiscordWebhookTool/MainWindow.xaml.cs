using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace DiscordWebhookTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void sendButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(webhookTextBox.Text, "https:\\/\\/discord\\.com\\/api\\/webhooks\\/\\d{17,19}\\/.{68}"))
            {
                MessageBox.Show("The specified webhook URL was invalid. Please paste the link provided by Discord.", "Invalid Webhook URL", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(contentTextBox.Text))
            {
                MessageBox.Show("The specified message content was empty. Please input a value.", "Empty Message Content", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var client = new WebhookClient(webhookTextBox.Text))
            {
                var response = await client.ExecuteAsync(new WebhookPayload()
                {
                    Content = contentTextBox.Text
                });

                if (!response.IsSuccessStatusCode)
                    MessageBox.Show($"An error has occurred:\n\n{await response.Content.ReadAsStringAsync()}", "Webhook Execution Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
