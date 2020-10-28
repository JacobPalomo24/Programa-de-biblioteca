/*
 * MIT License
 * 
 * Copyright (c) 2020 Jacob Palomo
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 * 
 */

using System;
using System.IO;

namespace Biblioteca
{
    class Program
    {
        static void Main(string[] args)
        {
            init();
        }

        public static void init()
        {
            // You can change the path and the file name here. Dont forget add the file extension.
            string path = @"C:\Users\jocel\Documents\C#\Biblioteca\libros.txt";

            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Add a book | 2. Look for a book | 3. See all our books | Press any other number or letter for exit ");

            Console.Write("\nOption: ");
            string op = Console.ReadLine();

            Console.Clear();
            switch (op)
            {
                case "1":
                    {
                        addBook(path);
                        break;
                    }
                
                case "2":
                    {
                        searchBook(path);
                        break;
                    }

                case "3":
                    {
                        viewAllBooks(path);
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        public static void addBook(string path)
        {
            Console.WriteLine("¡Add a new book!\n");

            Console.WriteLine("Book information:");
            Console.Write("    Book name: ");
            string bName = Console.ReadLine();
            Console.Write("    Author name: ");
            string bAuthorName = Console.ReadLine();
            Console.Write("    ISBN: ");
            string bISBN = Console.ReadLine();
            Console.Write("    Editorial: ");
            string bEditorial = Console.ReadLine();
            Console.Write("    Publication year: ");
            string bPublicationYear = Console.ReadLine();
            Console.Write("    Publication location: ");
            string bPublicationLocation = Console.ReadLine();
            Console.Write("    Literary gender: ");
            string bLiteraryGender = Console.ReadLine();
            Console.Write("    Pages number: ");
            string bPagesNumber = Console.ReadLine();

            Console.Write("\n¿The information is correct? (Y/N): ");
            if (Console.ReadLine().ToUpper().Equals("N"))
            {
                Console.WriteLine("¿Do you want to correct something? (Y/N)");
                if (Console.ReadLine().ToUpper().Equals("Y"))
                {
                    Console.Clear();
                    addBook(path);
                }
                else
                {
                    Console.Clear();
                    init();
                }
            } else {
                string hr = "\n----------------------------------------------------------------------------------------------------\n";
                string newBookInfo = "Book name: " + bName +
                                     "\nAuthor name: " + bAuthorName +
                                     "\nISBN: " + bISBN +
                                     "\nEditorial: " + bEditorial +
                                     "\nPublication year: " + bPublicationYear +
                                     "\nPublication location: " + bPublicationLocation +
                                     "\nLiterary gender: " + bLiteraryGender +
                                     "\nPages number: " + bPagesNumber;

                int id = 0;
                
                if (!File.Exists(path))
                {
                    id = 1;
                    File.WriteAllText(path, "ID: " + id + "\n" + newBookInfo);
                }
                else
                {
                    StreamReader lectura = File.OpenText(path);
                    string books = lectura.ReadToEnd();
                    string[] book = books.Split('\n');
                    lectura.Close();

                    for (int i = 1; i < book.Length; i += 10)
                    {
                        if (book[i].Substring(0, 9).Equals("Book name"))
                        {
                            if (book[i].Substring(11).Equals(bName))
                            {
                                Console.Clear();
                                Console.Write("El nombre del libro ya existe, ingrese otro.");
                                Console.ReadKey(true);
                                init();
                            }
                        }
                    }

                    for (int i = 0; i < book.Length; i++)
                    {
                        if(book[i].Substring(0, 2).Equals("ID"))
                        {
                            id = int.Parse(book[i].Substring(4));
                        }
                    }

                    id++;

                    File.AppendAllText(path, hr + "ID: " + id + "\n" + newBookInfo);
                }
            }

            Console.Clear();
            Console.WriteLine("Libro agragado correctamente.\n");
            init();
        }

        public static void searchBook(string path)
        {
            if (File.Exists(path))
            {
                Console.WriteLine("¡Look for a book!\n");

                Console.Write("Enter the name of the book: ");
                string search = Console.ReadLine();

                StreamReader lectura = File.OpenText(path);
                string books = lectura.ReadToEnd();
                lectura.Close();

                string[] book = books.Split('\n');
                int lBook = 0;
                byte bf = 0;
                for (int i = 1; i < book.Length; i += 10)
                {
                    if (book[i].Substring(0, 9).Equals("Book name"))
                    {
                        if (book[i].Substring(11).Equals(search))
                        {
                            lBook = i;

                            lBook -= 1;
                            for (int e = 0; e < 9; e++)
                            {
                                Console.Write("\n" + book[lBook + e]);
                            }

                            bf = 1;

                            break;
                        }
                        else
                        {
                            bf = 0;
                        }
                    }
                }

                if (bf != 1)
                {
                    Console.Write("\nBook not found :(");
                }
                else
                {
                    Console.Write("\n\nSuccessfully found book :)");
                }
            }
            else
            {
                Console.Write("There is nothing in our database yet.");
            }

            Console.ReadKey(true);
            Console.Clear();
            init();
        }

        public static void viewAllBooks(string path)
        {

            if (!File.Exists(path))
            {
                Console.Write("There is nothing in our database yet.");
                Console.ReadKey(true);
                Console.Clear();
                init();
            }
            else
            {
                Console.Write("See all our books.\n\n");

                StreamReader lectura = File.OpenText(path);
                string libro = lectura.ReadToEnd();
                lectura.Close();

                Console.WriteLine(libro);

                Console.WriteLine("");
                init();
            }
        }
    }
}
