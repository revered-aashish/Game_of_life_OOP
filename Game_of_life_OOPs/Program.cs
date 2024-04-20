namespace Game_of_life_OOPs
{
    public class Grid
    {
        private int[,] _grid; //adding underscore before variable name just as a convention that it's private variable
        private int _rows;
        private int _columns;

        public Grid(int rows, int columns)   //Constructor Function
        {
            _rows = rows;
            _columns = columns;
            _grid = new int[rows, columns];
        }

        public void SetCell(int row, int column, int value)    //Setter function
        {
            if (value != 0 && value != 1)
                throw new ArgumentException("Cell value must be 0 or 1.");

            _grid[row, column] = value;
        }

        public int GetCell(int row, int column)  //Getter function for the value of Cell in the Grid
        {
            return _grid[row, column];
        }

        public int Rows => _rows;  //getter function for the number of rows
        public int Columns => _columns; //getter function for number of columns
    }

    public class GameOfLife
    {
        private Grid _grid;

        public GameOfLife(Grid grid) //Constructor
        {
            _grid = grid;
        }

        public Grid NextGeneration()  //returns the next state of the grid
        {
            Grid next = new Grid(_grid.Rows, _grid.Columns); //initializing the new grid which stores the next generation of the grid

            for (int i = 0; i < _grid.Rows; i++)
            {
                for (int j = 0; j < _grid.Columns; j++)
                {
                    int liveNeighbours = CountLiveNeighbour(i, j);

                    if (_grid.GetCell(i, j) == 1 && (liveNeighbours < 2 || liveNeighbours > 3))
                        next.SetCell(i, j, 0);

                    else if (_grid.GetCell(i, j) == 0 && liveNeighbours == 3)
                        next.SetCell(i, j, 1);

                    else
                        next.SetCell(i, j, _grid.GetCell(i, j));
                }
            }

            return next; // Return the updated grid
        }

        private int CountLiveNeighbour(int row, int column) //returns the count of alive neighbour for any specific cell of grid
        {
            //di and dj are used to access all the eight directions neighbor from any cell
            int[] di = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] dj = { 0, 1, 1, 1, 0, -1, -1, -1 };

            int live = 0;

            for (int ind = 0; ind < 8; ind++) //traverses all the neighbor
            {
                int ni = row + di[ind];
                int nj = column + dj[ind];

                if (ni >= 0 && ni < _grid.Rows && nj >= 0 && nj < _grid.Columns)
                {
                    live += _grid.GetCell(ni, nj);
                }
            }
            return live;
        }
    }

    public class InputSide
    {
        public static Grid GetGridFromUserInput()
        {
            Console.Write("Number of rows: ");
            int rows = int.Parse(Console.ReadLine());
            Console.Write("Number of columns: ");
            int columns = int.Parse(Console.ReadLine());

            Grid grid = new Grid(rows, columns);

            Console.WriteLine("Enter grid values (0 or 1):");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write("Enter the {0}th row {1}th column cell value: ", i, j); //Can be commented if not needed
                    int value = int.Parse(Console.ReadLine());
                    grid.SetCell(i, j, value);
                }
            }

            return grid;
        }
    }

    public class OutputSide
    {
        public static void PrintGrid(Grid grid)   //Used for printing the grid
        {
            for (int i = 0; i < grid.Rows; i++)
            {
                for (int j = 0; j < grid.Columns; j++)
                {
                    Console.Write(grid.GetCell(i, j) == 0 ? "." : "*");
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Grid grid = InputSide.GetGridFromUserInput();
                GameOfLife game = new GameOfLife(grid);

                Console.WriteLine("Current Scenario:");
                OutputSide.PrintGrid(grid);

                Grid nextGenerationGrid = game.NextGeneration(); // Calculate the next generation

                Console.WriteLine("Next Scenario:");
                OutputSide.PrintGrid(nextGenerationGrid); // Print the updated grid
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter valid integers."); // when we enter "abc" in Number of rows field
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Error: {e.Message}"); // when we enter any other value than 0 or 1 in the cell of grid
            }
        }
    }
}
