using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using Npgsql;

namespace WindowsFormsApp1.DbClasses
{
    public class Common
    {
        public static int BookId;
        public static int UserId;
        private static NpgsqlConnection _npgsqlConnection;

        public static void connect()
        {
            try
            {
                _npgsqlConnection =
                    new NpgsqlConnection(
                        "Server=192.168.0.103;Port=5432;Database=postgres;User Id=user;Password=user;ApplicationName=LibraryApp");
                _npgsqlConnection.Open();
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show($"Ошибка с подключением к базе {e.Message}");
            }
        }

        public static void close()
        {
            _npgsqlConnection.Close();
        }

        public static List<Book> load_book()
        {
            var books = _npgsqlConnection.Query<Book>("select * from book");
            return books.OrderBy(c => c.Id).ToList();
        }

        public static List<User> load_user()
        {
            string Str = "select * from users";
            var users = _npgsqlConnection.Query<User>(Str);
            return users.OrderBy(c => c.Id).ToList();
        }

        public static Book Find_book_by_id(int id, List<Book> books)
        {
            foreach (var book in books)
            {
                if (book.Id == id)
                {
                    return book;
                }
            }

            return null;
        }

        public static User Find_user_by_id(int id, List<User> users)
        {
            foreach (var user in users)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }

            return null;
        }

        public static string list_of_debtors(List<Book> books, List<User> users)
        {
            int maxdate = 10;
            string str = "\nСписок должников:\n";
            List<User> mas_debtors = new List<User>();
            DateTime nowdate = DateTime.Now;
            foreach (var book in books)
            {
                if (book.Whos != 0 && book.Date.AddDays(maxdate) < nowdate)
                {
                    var b = Find_user_by_id(book.Whos, users);
                    if (b != null)
                    {
                        mas_debtors.Add(b);
                        str += b + "\n";
                    }
                }

            }

            if (str == "")
            {
                return "Нет долгов \n";
            }

            return str;

        }

        public static string bring_book(List<Book> books, List<User> users, int id_book, int id_user)
        {
            User user = Find_user_by_id(id_user, users);
            Book book = Find_book_by_id(id_book, books);

            if (user != null && book != null)
            {
                if (user.Id_book == 0 && book.Whos == 0)
                {
                    book.Whos = user.Id;
                    book.Date = DateTime.Parse("01-01-2001");
                    user.Books_now += 1;
                    user.Books_alltime += 1;
                    user.Id_book = book.Id;
                    string query1 = "update book set date = :date, whos = :whos where id = :id";
                    string query2 =
                        "update users set id_book = :id_book, books_now = :books_now, books_alltime = :books_alltime where id = :id ";
                    string str1 = $"\n Пользователь с id {id_user} взял книгу {book.Name} автора {book.Author}\n";
                    string query3 = "insert into events (operation_id, logs) values (1, :logs)";
                    _npgsqlConnection.Execute(query1, new {date = book.Date, whos = book.Whos, id = book.Id});
                    _npgsqlConnection.Execute(query2,
                        new
                        {
                            id_book = user.Id_book, books_now = user.Books_now, books_alltime = user.Books_alltime,
                            id = user.Id
                        });
                    _npgsqlConnection.Execute(query3, new {logs = str1});
                    return str1;
                }

                return $"\n Пользователь с id {id_user} уже взял книгу и/или книга с id {book.Id} на руках\n";
            }

            return "Еrror\n";
        }


        public static string bring_book(List<Book> books, List<User> users, Book book, User user)
        {
            if (user != null && book != null)
            {
                if (user.Id_book == 0 && book.Whos == 0)
                {
                    book.Whos = user.Id;
                    book.Date = DateTime.Parse("01-01-2001");
                    user.Books_now += 1;
                    user.Books_alltime += 1;
                    user.Id_book = book.Id;
                    string query1 = "update book set date = :date, whos = :whos where id = :id";
                    string query2 =
                        "update users set id_book = :id_book, books_now = :books_now, books_alltime = :books_alltime where id = :id ";
                    string str1 = $"\n Пользователь с id {user.Id} взял книгу {book.Name} автора {book.Author}\n";
                    string query3 = "insert into events (operation_id, logs) values (1, :logs)";
                    _npgsqlConnection.Execute(query1, new {date = book.Date, whos = book.Whos, id = book.Id});
                    _npgsqlConnection.Execute(query2,
                        new
                        {
                            id_book = user.Id_book, books_now = user.Books_now, books_alltime = user.Books_alltime,
                            id = user.Id
                        });
                    _npgsqlConnection.Execute(query3, new {logs = str1});
                    return str1;
                }

                return $"\n Пользователь с id {user.Id} уже взял книгу и/или книга с id {book.Id} на руках\n";
            }

            return "Еrror\n";
        }

        public static string return_book(List<Book> books, List<User> users, int id_book, int id_user)
        {
            User user = Find_user_by_id(id_user, users);
            Book book = Find_book_by_id(id_book, books);

            if (user != null && book != null)
            {
                if (user.Id_book == book.Id && book.Whos == user.Id)
                {
                    book.Whos = 0;
                    book.Date = DateTime.Parse("2000-1-1");
                    user.Books_now = 0;
                    user.Id_book = 0;
                    string str1 = $"\n Пользователь с id {id_user} вернул книгу {book.Name} автора {book.Author}\n";
                    string query1 = "update book set date = :date, whos = 0 where id = :id";
                    string query2 = "update users set id_book = 0, books_now = 0 where id = :id ";
                    string query3 = "insert into events (operation_id, logs) values (2, :logs)";

                    _npgsqlConnection.Execute(query1, new {date = book.Date, id = book.Id});
                    _npgsqlConnection.Execute(query2, new {id = user.Id});
                    _npgsqlConnection.Execute(query3, new {logs = str1});
                    return str1;
                }

                return $"\n У пользователя с данным id {id_user} нет на руках книги {book.Name} автора {book.Author}\n";

            }

            return "Error\n";
        }


        public static string return_book(List<Book> books, List<User> users, Book book, User user)
        {
            if (user != null && book != null)
            {
                if (user.Id_book == book.Id && book.Whos == user.Id)
                {
                    book.Whos = 0;
                    book.Date = DateTime.Parse("2000-1-1");
                    user.Books_now = 0;
                    user.Id_book = 0;
                    string str1 = $"\n Пользователь с id {user.Id} вернул книгу {book.Name} автора {book.Author}\n";
                    string query1 = "update book set date = :date, whos = 0 where id = :id";
                    string query2 = "update users set id_book = 0, books_now = 0 where id = :id ";
                    string query3 = "insert into events (operation_id, logs) values (2, :logs)";

                    _npgsqlConnection.Execute(query1, new {date = book.Date, id = book.Id});
                    _npgsqlConnection.Execute(query2, new {id = user.Id});
                    _npgsqlConnection.Execute(query3, new {logs = str1});
                    return str1;
                }

                return $"\n У пользователя с данным id {user.Id} нет на руках книги {book.Name} автора {book.Author}\n";

            }

            return "Error\n";
        }

        public static void Clear_base()
        {
            string query1 = @"
update users set id_book = 0, books_now = 0, books_alltime=0;
update book set whos = 0;
DROP TABLE public.events;
CREATE TABLE public.events (
    id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
    logs varchar NULL,
    operation_id int4 NULL);";

            _npgsqlConnection.Execute(query1);

        }
    }
}