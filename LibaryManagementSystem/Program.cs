﻿namespace LibraryManagementSystem
{
    // ======== BASIC ASSIGNMENT ========

    // TODO: Create the abstract base class LibraryItem with:
    // - Properties: Id, Title, PublicationYear
    // - Constructor that initializes these properties
    // - Abstract method: DisplayInfo()
    // - Virtual method: CalculateLateReturnFee(int daysLate) that returns decimal
    //   with a basic implementation of daysLate * 0.50m

    public abstract class LibraryItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string PublicationYear { get; set; }

        protected LibraryItem(string id, string title, string publicationYear)
        {
            this.Id = id;
            this.Title = title;
            this.PublicationYear = publicationYear;
        }

        public abstract void DisplayInfo();

        public virtual decimal CalculateLateReturnFee(int daysLate)
        {
            return daysLate * 0.5m;
        }
    }

    // TODO: Create the IBorrowable interface with:
    // - Properties: BorrowDate (DateTime?), ReturnDate (DateTime?), IsAvailable (bool)
    // - Methods: Borrow(), Return()

    public interface IBorrowable
    {
        DateTime? BorrowDate { get; set; }
        DateTime? ReturnDate { get; set; }
        bool IsAvailable { get; set; }

        void Borrow();
        void Return();
    }

    // TODO: Create the Book class that inherits from LibraryItem and implements IBorrowable
    // - Add properties: Author, Pages, Genre
    // - Implement all required methods from the base class and interface
    // - Override CalculateLateReturnFee to return daysLate * 0.75m

    public class Book : LibraryItem, IBorrowable
    {
        public string Author { get; set; }
        public int Pages { get; set; }
        public string Genre { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsAvailable { get; set; } = true;

        public Book(int id, string title, int year, string author) : base(id.ToString(), title, year.ToString())
        {
            Author = author;
            IsAvailable = true;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine("*** BOOK INFORMATION ***");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Genre: {Genre}");
            Console.WriteLine($"Pages: {Pages}");
            Console.WriteLine($"Is available: {IsAvailable}");
        }

        public override decimal CalculateLateReturnFee(int daysLate)
        {
            return daysLate * 0.75m;
        }

        public void Borrow()
        {
            if (IsAvailable)
            {
                BorrowDate = DateTime.Now;
                ReturnDate = null;
                IsAvailable = false;
                Console.WriteLine($"Book {Title} has been borrowed successfully");
            }
            else
            {
                Console.WriteLine($"This book {Title} is not available for borrowing");
            }
        }

        public void Return()
        {
            if (!IsAvailable)
            {
                ReturnDate = DateTime.Now;
                IsAvailable = true;

                //calculate return fee
                //assuming people can borrow an item for 2 weeks
                if (BorrowDate.HasValue)
                {
                    TimeSpan borrowedDuration = ReturnDate.Value - BorrowDate.Value;
                    int daysLate = Math.Max(0, (int)borrowedDuration.TotalDays - 14);

                    if (daysLate > 0)
                    {
                        decimal returnFee = CalculateLateReturnFee(daysLate);
                        Console.WriteLine($"Late return fee for book {Title}: {returnFee}");
                    }
                    else
                    {
                        Console.WriteLine($"This book {Title} has been returned on time");
                    }
                }
            }
        }
    }

    // TODO: Create the DVD class that inherits from LibraryItem and implements IBorrowable
    // - Add properties: Director, Runtime (in minutes), AgeRating
    // - Implement all required methods from the base class and interface
    // - Override CalculateLateReturnFee to return daysLate * 1.00m

    public class DVD : LibraryItem, IBorrowable
    {
        public string Director { get; set; }
        public int Runtime { get; set; }
        public string AgeRating { get; set; }

        public DVD(int id, string title, int publicationYear, string director) : base(id.ToString(), title,
            publicationYear.ToString())
        {
            Director = director;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine("*** BOOK INFORMATION ***");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Director: {Director}");
            Console.WriteLine($"Runtime: {Runtime}");
            Console.WriteLine($"Publication year: {PublicationYear}");
            Console.WriteLine($"Is available: {IsAvailable}");
        }

        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsAvailable { get; set; }

        public void Borrow()
        {
            if (IsAvailable)
            {
                BorrowDate = DateTime.Now;
                ReturnDate = null;
                IsAvailable = false;
                Console.WriteLine($"DVD {Title} has been borrowed successfully");
            }
            else
            {
                Console.WriteLine($"This DVD {Title} is not available for borrowing");
            }
        }

        public override decimal CalculateLateReturnFee(int daysLate)
        {
            return daysLate * 1.0m;
        }

        public void Return()
        {
            if (!IsAvailable)
            {
                ReturnDate = DateTime.Now;
                IsAvailable = true;

                //calculate return fee
                //assuming people can borrow an item for 2 weeks
                if (BorrowDate.HasValue)
                {
                    TimeSpan borrowedDuration = ReturnDate.Value - BorrowDate.Value;
                    int daysLate = Math.Max(0, (int)borrowedDuration.TotalDays - 14);

                    if (daysLate > 0)
                    {
                        var returnFee = CalculateLateReturnFee(daysLate);
                        Console.WriteLine($"Late return fee for DVD {Title}: {returnFee}");
                    }
                    else
                    {
                        Console.WriteLine($"This DVD {Title} has been returned on time");
                    }
                }
            }
        }
    }

    // TODO: Create the Magazine class that inherits from LibraryItem
    // - Add properties: IssueNumber, Publisher
    // - Implement all required methods from the base class
    // - Magazines don't need to implement IBorrowable (they typically can't be borrowed)

    public class Magazine : LibraryItem
    {
        public int IssueNumber { get; set; }
        public string Publisher { get; set; }

        public Magazine(int id, string title, int publicationYear, int issueNumber) : base(id.ToString(), title,
            publicationYear.ToString())
        {
            IssueNumber = issueNumber;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine("*** MAGAZINE INFORMATION ***");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Publisher: {Publisher}");
            Console.WriteLine($"Issue number: {IssueNumber}");
            Console.WriteLine($"Publication year: {PublicationYear}");
        }
    }

    // TODO: Create the Library class with:
    // - A list to store LibraryItems
    // - Methods: AddItem(), SearchByTitle(), DisplayAllItems()

    public class Library
    {
        public List<LibraryItem> list = new List<LibraryItem>();

        public void AddItem(LibraryItem item)
        {
            list.Add(item);
        }

        public LibraryItem SearchByTitle(string title)
        {
            //return the first element that satisfies the condition or the default value if nothing is found
            //the default value in this case is null
            //null is catched later in the main method
            return list.FirstOrDefault(item => item.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }

        public void DisplayAllItems()
        {
            Console.WriteLine("============ LIBRARY INVENTORY ============");
            if (list.Count == 0)
            {
                Console.WriteLine("THERE ARE NOTHING IN THE LIBRARY");
                return;
            }

            foreach (var items in list)
            {
                items.DisplayInfo();
                Console.WriteLine("---------------------------------------");
            }
        }
    }

    // ======== ADVANCED ASSIGNMENT ========

    // TODO (ADVANCED): Create a record type for tracking borrowing history
    // - Include: ItemId, Title, BorrowDate, ReturnDate, BorrowerName
    // - Add an init-only property: LibraryLocation

    // TODO (ADVANCED): Create an extension method for string
    // - Create a method ContainsIgnoreCase() that checks if a string contains
    //   another string, ignoring case sensitivity

    // TODO (ADVANCED): Create a generic collection to avoid boxing/unboxing
    // - Create a class LibraryItemCollection<T> where T : LibraryItem
    // - Implement methods: Add(), GetItem(), Count property

    // TODO (ADVANCED): Add ref parameter and ref return methods to the Library class
    // - UpdateItemTitle method using ref parameter
    // - GetItemReference method with ref return


    class Program
    {
        static void Main()
        {
            // Create library
            var library = new Library();

            // Add items
            var book1 = new Book(1, "The Great Gatsby", 1925, "F. Scott Fitzgerald")
            {
                Genre = "Classic Fiction",
                Pages = 180
            };

            var book2 = new Book(2, "Clean Code", 2008, "Robert C. Martin")
            {
                Genre = "Programming",
                Pages = 464
            };

            var dvd1 = new DVD(3, "Inception", 2010, "Christopher Nolan")
            {
                Runtime = 148,
                AgeRating = "PG-13"
            };

            var magazine1 = new Magazine(4, "National Geographic", 2023, 56)
            {
                Publisher = "National Geographic Partners"
            };

            library.AddItem(book1);
            library.AddItem(book2);
            library.AddItem(dvd1);
            library.AddItem(magazine1);

            // Display all items
            library.DisplayAllItems();

            // Borrow and return demonstration
            Console.WriteLine("\n===== Borrowing Demonstration =====");
            book1.Borrow();
            dvd1.Borrow();

            // Try to borrow again
            book1.Borrow();

            // Display changed status
            Console.WriteLine("\n===== Updated Status =====");
            book1.DisplayInfo();
            dvd1.DisplayInfo();

            // Return item
            Console.WriteLine("\n===== Return Demonstration =====");
            book1.Return();

            // Search for an item
            Console.WriteLine("\n===== Search Demonstration =====");
            var foundItem = library.SearchByTitle("Clean");
            if (foundItem != null)
            {
                Console.WriteLine("Found item:");
                foundItem.DisplayInfo();
            }
            else
            {
                Console.WriteLine("Item not found");
            }

            // ======== ADVANCED FEATURES DEMONSTRATION ========
            // Uncomment and implement these sections for the advanced assignment

            /*
            if (ShouldRunAdvancedFeatures())
            {
                // Boxing/Unboxing performance comparison
                Console.WriteLine("\n===== ADVANCED: Boxing/Unboxing Performance =====");

                // Example implementation:
                var standardList = new ArrayList();
                var genericList = new List<int>();
                var customCollection = new LibraryItemCollection<Book>();

                const int iterations = 1_000_000;

                // Measure ArrayList performance (with boxing)
                var stopwatch = Stopwatch.StartNew();
                for (int i = 0; i < iterations; i++)
                {
                    standardList.Add(i);
                }

                int sum1 = 0;
                foreach (int i in standardList)
                {
                    sum1 += i;
                }
                stopwatch.Stop();
                Console.WriteLine($"ArrayList time (with boxing): {stopwatch.ElapsedMilliseconds}ms");

                // Measure generic List<T> performance (no boxing)
                stopwatch.Restart();
                for (int i = 0; i < iterations; i++)
                {
                    genericList.Add(i);
                }

                int sum2 = 0;
                foreach (int i in genericList)
                {
                    sum2 += i;
                }
                stopwatch.Stop();
                Console.WriteLine($"Generic List<T> time (no boxing): {stopwatch.ElapsedMilliseconds}ms");

                // Add books to custom collection
                var book3 = new Book(5, "The Hobbit", 1937, "J.R.R. Tolkien") { Pages = 310 };
                var book4 = new Book(6, "1984", 1949, "George Orwell") { Pages = 328 };

                customCollection.Add(book1);
                customCollection.Add(book3);
                customCollection.Add(book4);

                Console.WriteLine($"Books in custom collection: {customCollection.Count}");

                // Pattern matching demonstration
                Console.WriteLine("\n===== ADVANCED: Pattern Matching =====");

                // Use pattern matching with type patterns to handle different item types
                var item = library.SearchByTitle("Clean");

                // Example pattern matching with switch expression
                string description = item switch
                {
                    Book b when b.Pages > 400 => $"Long book: {b.Title} with {b.Pages} pages",
                    Book b => $"Book: {b.Title} by {b.Author}",
                    DVD d => $"DVD: {d.Title} directed by {d.Director}",
                    Magazine m => $"Magazine: {m.Title}, Issue #{m.IssueNumber}",
                    null => "No item found",
                    _ => $"Unknown type: {item.Title}"
                };

                Console.WriteLine(description);

                // Ref returns demonstration
                Console.WriteLine("\n===== ADVANCED: Ref Returns =====");

                try
                {
                    ref var itemRef = ref library.GetItemReference(1);
                    Console.WriteLine($"Before modification: {itemRef.Title}");
                    itemRef.Title += " (Modified)";
                    Console.WriteLine($"After modification: {itemRef.Title}");

                    string title = "New Title";
                    if (library.UpdateItemTitle(2, ref title))
                    {
                        Console.WriteLine($"Updated title from '{title}' to 'New Title'");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                // Nullable types demonstration
                Console.WriteLine("\n===== ADVANCED: Nullable Types =====");

                Book? nullableBook = null;
                string bookTitle = nullableBook?.Title ?? "No title available";
                Console.WriteLine($"Nullable book title: {bookTitle}");

                var borrowedBook = library.SearchByTitle("gatsby") as Book;
                DateTime? dueDate = borrowedBook?.BorrowDate?.AddDays(14);
                Console.WriteLine($"Due date: {dueDate?.ToString("yyyy-MM-dd") ?? "Not borrowed"}");

                // Record type demonstration
                Console.WriteLine("\n===== ADVANCED: Record Type =====");

                var borrowRecord = new BorrowRecord(
                    1,
                    "The Great Gatsby",
                    DateTime.Now.AddDays(-7),
                    null,
                    "John Smith")
                {
                    LibraryLocation = "Main Branch"
                };

                Console.WriteLine(borrowRecord);

                // Create a modified copy using with-expression
                var returnedRecord = borrowRecord with { ReturnDate = DateTime.Now };
                Console.WriteLine($"Original record: {borrowRecord}");
                Console.WriteLine($"Modified record: {returnedRecord}");

                // Test extension method
                string searchTerm = "code";
                bool contains = "Clean Code".ContainsIgnoreCase(searchTerm);
                Console.WriteLine($"Does 'Clean Code' contain '{searchTerm}'? {contains}");
            }
            */
        }

        static bool ShouldRunAdvancedFeatures()
        {
            Console.WriteLine("\nWould you like to run the advanced features? (y/n)");
            return Console.ReadLine()?.ToLower() == "y";
        }
    }
}