// bookstore.cs
using System;
/*
The following example illustrates declaring, instantiating, and using a delegate. The BookDB class encapsulates a bookstore database that maintains a database 
of books. It exposes a method ProcessPaperbackBooks, which finds all paperback books in the database and calls a delegate for each one. The delegate type used 
is called ProcessBookDelegate. The Test class uses this class to print out the titles and average price of the paperback books.

The use of delegates promotes good separation of functionality between the bookstore database and the client code. The client code has no knowledge of how 
the books are stored or how the bookstore code finds paperback books. The bookstore code has no knowledge of what processing is done on the paperback books 
after it finds them.

*/
// A set of classes for handling a bookstore:
namespace Bookstore
{
    using System.Collections;

    // Describes a book in the book list:
    public struct Book
    {
        public string Title;        // Title of the book.
        public string Author;       // Author of the book.
        public decimal Price;       // Price of the book.
        public bool Paperback;      // Is it paperback?

        public Book(string title, string author, decimal price, bool paperBack)
        {
            Title = title;
            Author = author;
            Price = price;
            Paperback = paperBack;
        }
    }

    // Declare a delegate type for processing a book:
    public delegate void ProcessBookDelegate(Book book);

    // Maintains a book database.
    public class BookDB
    {

        // List of all books in the database:
        ArrayList list = new ArrayList();

        // Add a book to the database:
        public void AddBook(string title, string author, decimal price, bool paperBack)
        {
            list.Add(new Book(title, author, price, paperBack));
        }

        // Call a passed-in delegate on each paperback book to process it: 
        public void ProcessPaperbackBooks(ProcessBookDelegate processBook)
        {
            foreach (Book b in list)
            {
                if (b.Paperback)
                    // Calling the delegate:
                    processBook(b);
            }
        }

    }
}

// Using the Bookstore classes:
namespace BookTestClient
{
    using Bookstore;

    // Class to total and average prices of books:
    class PriceTotaller
    {
        int countBooks = 0;
        decimal priceBooks = 0.0m;

        internal void AddBookToTotal(Book book)
        {
            countBooks += 1;
            priceBooks += book.Price;
        }

        internal decimal AveragePrice()
        {
            return priceBooks / countBooks;
        }
    }

    // Class to test the book database:
    class Test
    {
        // Print the title of the book.
        static void PrintTitle(Book b)
        {
            Console.WriteLine("   {0}", b.Title);
        }

        // Execution starts here.
        static void Main1()
        {
            
            BookDB bookDB = new BookDB();

            // Initialize the database with some books:
            AddBooks(bookDB);

            // Print all the titles of paperbacks:
            Console.WriteLine("Paperback Book Titles:");
            // Create a new delegate object associated with the static 
            // method Test.PrintTitle:
            //bookDB.ProcessPaperbackBooks(new ProcessBookDelegate(PrintTitle));
            bookDB.ProcessPaperbackBooks( PrintTitle);

            // Get the average price of a paperback by using
            // a PriceTotaller object:
            PriceTotaller totaller = new PriceTotaller();
            // Create a new delegate object associated with the nonstatic 
            // method AddBookToTotal on the object totaller:
            //bookDB.ProcessPaperbackBooks(new ProcessBookDelegate(totaller.AddBookToTotal));
            bookDB.ProcessPaperbackBooks(totaller.AddBookToTotal);

            Console.WriteLine("Average Paperback Book Price: ${0:#.##}", totaller.AveragePrice());
            Console.WriteLine("Press any key to exit ....");
            Console.ReadKey();
        }

        // Initialize the book database with some test books:
        static void AddBooks(BookDB bookDB)
        {
            bookDB.AddBook("The C Programming Language",               
                "Brian W. Kernighan and Dennis M. Ritchie", 19.95m, true);
            bookDB.AddBook("The Unicode Standard 2.0",
               "The Unicode Consortium", 39.95m, true);
            bookDB.AddBook("The MS-DOS Encyclopedia",
               "Ray Duncan", 129.95m, false);
            bookDB.AddBook("Dogbert's Clues for the Clueless",
               "Scott Adams", 12.00m, true);
        }
    }

    #region
    /*
    -- Code Discussion --
    # Declaring a delegate   
    The following statement:
        public delegate void ProcessBookDelegate(Book book);
    declares a new delegate type. Each delegate type describes the number and types of the arguments, and the type of the return value of methods 
    that it can encapsulate. Whenever a new set of argument types or return value type is needed, a new delegate type must be declared.

    # Instantiating a delegate   
    Once a delegate type has been declared, a delegate object must be created and associated with a particular method. Like all other objects,
    a new delegate object is created with a new expression. When creating a delegate, however, the argument passed to the new expression is special
    — it is written like a method call, but without the arguments to the method.
    The following statement:
        bookDB.ProcessPaperbackBooks(new ProcessBookDelegate(PrintTitle));
    
    creates a new delegate object associated with the static method Test.PrintTitle. The following statement:
        bookDB.ProcessPaperbackBooks(new ProcessBookDelegate(totaller.AddBookToTotal));

    creates a new delegate object associated with the nonstatic method AddBookToTotal on the object totaller. In both cases, this new delegate object is immediately passed to the ProcessPaperbackBooks method.
    Note that : once a delegate is created, the method it is associated with never changes — delegate objects are immutable.
    
    # Calling a delegate   
    Once a delegate object is created, the delegate object is typically passed to other code that will call the delegate. 
    A delegate object is called by using the name of the delegate object, followed by the parenthesized arguments to be passed to the delegate. 
    An example of a delegate call is:
    
        processBook(b);

    A delegate can either be called synchronously, as in this example, or asynchronously by using BeginInvoke and EndInvoke methods.
    */
    #endregion
}