using System;

namespace WindowsFormsApp1.DbClasses
{
    public class Book
    {
        public int Id;
        public string Name;
        public string Author;
        public int Year;
        public string Publ;
        public int Whos;
        public DateTime Date;

        public Book()
        {
        }

        public Book(int id, string name, string author, int year, string publ, int? whos, DateTime? date)
        {
            Id = id;
            Name = name;
            Author = author;
            Year = year;
            Publ = publ;
            Whos = whos ?? 0;
            Date = date ?? DateTime.Parse("01-01-2001");
        }

        public override string ToString()
        {
            if (Whos == 0)
            {
                return $"Книга с id{Id}, название: {Name}, автора: {Author}, года выпуска: {Year}, издательства: {Publ}, доступна";
            }
            return $"Книга с id{Id}, название: {Name}, автора: {Author}, года выпуска: {Year}, издательства: {Publ}, на руках у {Whos} с этой даты: {Date}";
        }

        public static implicit operator string(Book book)
        {
            return book.ToString();
        }
    }
}