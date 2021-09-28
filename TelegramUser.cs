using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AI_telegram_bot
{
    class TelegramUser : INotifyPropertyChanged, IEnumerable<TelegramUser>
    {
        public TelegramUser(string Nickname, long ChatId)
        {
            this.nick = Nickname;
            this.id = ChatId;
            Messages = new ObservableCollection<string>();
        }

        private string nick;
        public long id;

        public string Nick
        {
            get { return this.nick; }
            set
            {
                this.nick = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Nick)));
            }
        }
        public long Id
        {
            get { return this.id; }
            set
            {
                this.id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Id)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Equals(TelegramUser other) => other.Id == this.id;

        public ObservableCollection<string> Messages { get; set; }

        public void AddMessage(string Text) => Messages.Add(Text);

        public IEnumerator<TelegramUser> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}