// See https://aka.ms/new-console-template for more information

class Program
{
    static void Main(string[] args)
    {
        // concert list created to be viewed and selected below. it is a list of objects from the concertticket class and it is called concerts
        List<ConcertTicket> concerts = new List<ConcertTicket>
        {
            new ConcertTicket("The George", new DateTime(2024, 4, 1), 20, 100f),
            new ConcertTicket("The Dame Tavern", new DateTime(2024, 4, 4), 100, 50f),
            new ConcertTicket("3 Arena, Dublin", new DateTime(2024, 4, 9), 50000, 80f)
        };


// loop to allow user to select a venue and book tickets
        while (true)
        {
            // display list of available venues as well as a way to exit the program
            Console.WriteLine("Available Concert Venues:");
            for (int i = 0; i < concerts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {concerts[i].Venue} on {concerts[i].Date.ToShortDateString()} - Ticket Price: €{concerts[i].TicketPrice} - Tickets available: {concerts[i].Capacity - concerts[i].TicketsSold}");
            }
            Console.WriteLine($"{concerts.Count + 1}. Exit");

            Console.Write("Select a venue to book tickets or enter 4 to exit: ");

            // get user input to store in venueChoice, I added a little bit of catch here in case the user enters an invalid input but will have to add more above
            int venueChoice = ConcertTicket.ConvertStringToInt(Console.ReadLine()) - 1; // here is the first type conversion to string to int

            if (venueChoice == concerts.Count)
            {
                return; // Exit the program
            }
            else if (venueChoice < 0 || venueChoice >= concerts.Count)
            {
                Console.WriteLine("Invalid selection, please try again.");
                continue;
            }
            // display selected venue and ask user how many tickets they want to book. their selection from above is stored in venueChoice and accessed here through selectedConcert.
            ConcertTicket selectedConcert = concerts[venueChoice];

            Console.WriteLine($"You selected: {selectedConcert.Venue} on {selectedConcert.Date.ToShortDateString()}");
            Console.Write("Enter the number of tickets you would like to book: ");
            string input = Console.ReadLine();
            int quantity = ConcertTicket.ConvertStringToInt(input); // here is the second type conversion

            // checking for availability based on number of tickets and if it is available, it will book the tickets
            if (selectedConcert.BookTickets(quantity))
            {
                Console.WriteLine("Yay! Booking successful.");
            }
            else
            {
                Console.WriteLine("Not enough tickets available.");
            }
            // here is a way to see the transactions before being looped back to the start or exiting the program. I need to add some catch here in case the user enters an invalid input
            Console.WriteLine("Would you like to view the transactions? (yes/no): ");
            string viewTransactions = Console.ReadLine().Trim().ToLower();
            if (viewTransactions == "yes")
            {
                selectedConcert.PrintTransactions();
            }
            if (viewTransactions == "no")
            {
                return;
            }
        }
    }
}


// below is a simple concert class with all the properties and methods
//I set the properties Capacity, TicketsSold, and Transactions to private for the setter to modify their values within the ConcertTicket class itself through the different methods below

public class ConcertTicket
{
    public string Venue { get; set; }
    public DateTime Date { get; set; }
    public int Capacity { get; private set; }
    public float TicketPrice { get; set; }
    public int TicketsSold { get; private set; }
    public List<string> Transactions { get; private set; }

// constructor of the concert ticket class that initializes the object with specific values for each property
    public ConcertTicket(string venue, DateTime date, int capacity, float ticketPrice)
    {
        Venue = venue;
        Date = date;
        Capacity = capacity;
        TicketPrice = ticketPrice;
        TicketsSold = 0;
        Transactions = new List<string>();
    }

    public bool BookTickets(int quantity)
    {
        if (TicketsSold + quantity >= Capacity)
        {
            return false; // Booking fails if the capacity for the venue is reached or goes over
        }
        TicketsSold += quantity;
        Transactions.Add($"Sold {quantity} tickets for {Venue} on {Date.ToShortDateString()}.");
        return true;
    }

    // foreach loop to print all the transactions
    public void PrintTransactions()
    {
        Console.WriteLine($"Transactions for {Venue}:");
        foreach (var transaction in Transactions)
        {
            Console.WriteLine(transaction);
        }
    }

    // here is a simple conversion method to convert a string to an int. I originally had a try catch method but it looks ugly so i found this one on stack overflow
    public static int ConvertStringToInt(string input)
    {
        return int.TryParse(input, out int result) ? result : 0;
    }
}

