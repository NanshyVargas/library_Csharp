using System;

namespace WindowsFormsApp1.DbClasses
{
    public class User
    {
        public int Id;
        public string Name;
        public DateTime Register_date;
        public int Books_now;
        public int Books_alltime;
        public int Id_book;

        public User(int id, string name, DateTime registerDate, int  booksNow, int booksAlltime, int? idBook)
        {
            Id = id;
            Name = name;
            Register_date = registerDate;
            Books_now = booksNow;
            Books_alltime = booksAlltime;
            Id_book = idBook ?? 0;
        }

        public User()
        {
            
        }
        public override string ToString()
        {
            return $"Пользователь с id{Id}, \nФИО: {Name}, \nдатой регистрации: {Register_date}. \nКоличество книг на руках: {Books_now}, \nВсего прочитано книг: {Books_alltime}. \nСейчас на руках: {Id_book} ";
        }
        
        public static implicit operator string(User user)
        {
            return user.ToString();
        }
    }
}