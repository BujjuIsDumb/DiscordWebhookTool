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

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using DiscordWebhookTool.Entities;

namespace DiscordWebhookTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _content;

        private List<Embed> _embeds;

        private int _selected;

        public MainWindow()
        {
            InitializeComponent();
            _embeds = new List<Embed>();
        }

        #region Embed Designer
        private void contentTextBox_TextChanged(object sender, TextChangedEventArgs e)
            => _content = contentTextBox.Text;

        private void authorTextBox_TextChanged(object sender, TextChangedEventArgs e)
            => _embeds[_selected].Author = new EmbedAuthor() { Name = authorTextBox.Text };

        private void titleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (embedListBox.SelectedIndex != -1)
            {
                embedListBox.Items[_selected] = new ListBoxItem()
                {
                    Content = titleTextBox.Text,
                    Style = (Style)FindResource("DiscordListBoxItemTheme")
                };

                embedListBox.SelectedIndex = _selected;

                _embeds[_selected].Title = titleTextBox.Text;
            }
        }

        private void descriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
            => _embeds[_selected].Description = descriptionTextBox.Text;

        private void footerTextBox_TextChanged(object sender, TextChangedEventArgs e)
            => _embeds[_selected].Footer = new EmbedFooter() { Text = footerTextBox.Text };

        private void colorTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(colorTextBox.Text, "#(?:[0-9a-fA-F]{3}){1,2}$"))
                _embeds[_selected].Color = Convert.ToInt32(colorTextBox.Text.Replace("#", null), 16);
        }

        private void deleteEmbedButton_Click(object sender, RoutedEventArgs e)
        {
            // Remove the embed.
            if (embedListBox.SelectedIndex != -1)
            {
                _embeds.RemoveAt(_selected);
                _selected -= 1;
                embedListBox.Items.RemoveAt(_selected + 1);
            }

            // Hide the embed designer if there are no longer any embeds.
            if (_embeds.Count == 0)
            {
                authorTextBlock.Visibility = Visibility.Hidden;
                authorTextBox.Visibility = Visibility.Hidden;
                titleTextBlock.Visibility = Visibility.Hidden;
                titleTextBox.Visibility = Visibility.Hidden;
                descriptionTextBlock.Visibility = Visibility.Hidden;
                descriptionTextBox.Visibility = Visibility.Hidden;
                footerTextBlock.Visibility = Visibility.Hidden;
                footerTextBox.Visibility = Visibility.Hidden;
                colorTextBlock.Visibility = Visibility.Hidden;
                colorTextBox.Visibility = Visibility.Hidden;
            }
        }

        private void addEmbedButton_Click(object sender, RoutedEventArgs e)
        {
            if (_embeds.Count <= 10)
            {
                // Show the embed designer.
                authorTextBlock.Visibility = Visibility.Visible;
                authorTextBox.Visibility = Visibility.Visible;
                titleTextBlock.Visibility = Visibility.Visible;
                titleTextBox.Visibility = Visibility.Visible;
                descriptionTextBlock.Visibility = Visibility.Visible;
                descriptionTextBox.Visibility = Visibility.Visible;
                footerTextBlock.Visibility = Visibility.Visible;
                footerTextBox.Visibility = Visibility.Visible;
                colorTextBlock.Visibility = Visibility.Visible;
                colorTextBox.Visibility = Visibility.Visible;

                // Create embed.
                _selected = embedListBox.Items.Add(new ListBoxItem()
                {
                    Content = "New Embed",
                    Style = (Style)FindResource("DiscordListBoxItemTheme")
                });

                embedListBox.SelectedIndex = _selected;
                _embeds.Add(new Embed() { Title = "New Embed" });

                // Hide embed data from previous embed.
                authorTextBox.Text = string.Empty;
                titleTextBox.Text = "New Embed";
                descriptionTextBox.Text = string.Empty;
                footerTextBox.Text = string.Empty;
                colorTextBox.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("You can only have up to 10 embeds on a message.", "Max Embed Limit Reached", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void editEmbeds_Click(object sender, RoutedEventArgs e)
        {
            // Show embed management buttons.
            deleteEmbedButton.Visibility = Visibility.Visible;
            addEmbedButton.Visibility = Visibility.Visible;

            // Show embed designer if there are embeds.
            if (_embeds.Count > 0)
            {
                authorTextBlock.Visibility = Visibility.Visible;
                authorTextBox.Visibility = Visibility.Visible;
                titleTextBlock.Visibility = Visibility.Visible;
                titleTextBox.Visibility = Visibility.Visible;
                descriptionTextBlock.Visibility = Visibility.Visible;
                descriptionTextBox.Visibility = Visibility.Visible;
                footerTextBlock.Visibility = Visibility.Visible;
                footerTextBox.Visibility = Visibility.Visible;
                colorTextBlock.Visibility = Visibility.Visible;
                colorTextBox.Visibility = Visibility.Visible;
            }

            // Hide the message content designer.
            contentTextBlock.Visibility = Visibility.Hidden;
            contentTextBox.Visibility = Visibility.Hidden;
        }

        private void editMessageButton_Click(object sender, RoutedEventArgs e)
        {
            // Hide embed management buttons.
            deleteEmbedButton.Visibility = Visibility.Hidden;
            addEmbedButton.Visibility = Visibility.Hidden;

            // Hide the embed designer.
            authorTextBlock.Visibility = Visibility.Hidden;
            authorTextBox.Visibility = Visibility.Hidden;
            titleTextBlock.Visibility = Visibility.Hidden;
            titleTextBox.Visibility = Visibility.Hidden;
            descriptionTextBlock.Visibility = Visibility.Hidden;
            descriptionTextBox.Visibility = Visibility.Hidden;
            footerTextBlock.Visibility = Visibility.Hidden;
            footerTextBox.Visibility = Visibility.Hidden;
            colorTextBlock.Visibility = Visibility.Hidden;
            colorTextBox.Visibility = Visibility.Hidden;

            // Show the message content designer.
            contentTextBlock.Visibility = Visibility.Visible;
            contentTextBox.Visibility = Visibility.Visible;
        }

        private void embedListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Set the embed data in the embed desinger if this isn't being run through embed management buttons.
            if (embedListBox.SelectedIndex != -1 && _selected != embedListBox.SelectedIndex)
            {
                _selected = embedListBox.SelectedIndex;

                authorTextBox.Text = _embeds[_selected].Author?.Name;
                titleTextBox.Text = _embeds[_selected].Title;
                descriptionTextBox.Text = _embeds[_selected].Description;
                footerTextBox.Text = _embeds[_selected].Footer?.Text;
                colorTextBox.Text = _embeds[_selected].Color?.ToString("X");
            }
        }
        #endregion

        private async void sendButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(webhookTextBox.Text, "https:\\/\\/discord\\.com\\/api\\/webhooks\\/\\d{17,19}\\/.{68}"))
            {
                MessageBox.Show("The specified webhook URL was invalid. Please paste the link provided by Discord.", "Invalid Webhook URL", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(_content) && _embeds.Count <= 0)
            {
                MessageBox.Show("The specified message content was empty. Please input a value.", "Empty Message Content", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var client = new WebhookClient(webhookTextBox.Text))
            {
                var response = await client.ExecuteAsync(new WebhookPayload()
                {
                    Content = _content,
                    Embeds = _embeds
                });

                if (!response.IsSuccessStatusCode)
                    MessageBox.Show($"An error has occurred:\n\n{await response.Content.ReadAsStringAsync()}", "Webhook Execution Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}