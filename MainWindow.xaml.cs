using System;
using System.Windows;
using System.IO;
using System.Collections.ObjectModel;
using Telegram.Bot;
using System.Linq;

namespace AI_telegram_bot
{
    public partial class MainWindow : Window
    {
        ObservableCollection<TelegramUser> Users;
        TelegramBotClient bot;

        [Obsolete]
        public MainWindow()
        {
            InitializeComponent();
            Users = new ObservableCollection<TelegramUser>();
            usersList.ItemsSource = Users;

            var token = "1918767773:AAF_bGCI-WkEwhW39lSc6tSkESw21y_mSqg"; 
            bot = new TelegramBotClient(token);

            bot.OnMessage += delegate (object sender, Telegram.Bot.Args.MessageEventArgs e)
            {
                string msg = $"{DateTime.Now}: {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";
                File.AppendAllText("data.log", $"{msg}\n");

                this.Dispatcher.Invoke(() =>
                {
                    var person = new TelegramUser(e.Message.Chat.FirstName, e.Message.Chat.Id);
                    if (!Users.Contains(person)) Users.Add(person);
                    Users[Users.IndexOf(person)].AddMessage($"{person.Nick}: {e.Message.Text}");
                });
            };

            bot.StartReceiving();

            ButtonSendMessage.Click += delegate { SendMessage(); };
            txtBoxSendMessage.KeyDown += (s, e) => { if (e.Key == System.Windows.Input.Key.Return) { SendMessage(); } };
        }

        public void SendMessage()
        {
            var concreteUser = Users[Users.IndexOf(usersList.SelectedItem as TelegramUser)];
            string responseMsg = $"Support: {txtBoxSendMessage.Text}";
            concreteUser.Messages.Add(responseMsg);

            bot.SendTextMessageAsync(concreteUser.Id, txtBoxSendMessage.Text);
            string logText = $"{DateTime.Now}: >> {concreteUser.Id} {concreteUser.Nick} {responseMsg}\n";
            File.AppendAllText("data.log", logText);

            txtBoxSendMessage.Text = String.Empty;
        }
    }
}