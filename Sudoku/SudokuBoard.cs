class SudokuBoard : ICloneable {
    private ISet<int>[][] cells;

    public SudokuBoard()
    {
        cells = new HashSet<int>[9][];
        for (int x = 0; x < 9; x++)
        {
            cells[x] = new HashSet<int>[9];
            for (int y = 0; y < 9; y++)
            {
                cells[x][y] = new HashSet<int>();
                FillSet(cells[x][y]);
            }
        }
    }
    private SudokuBoard(ISet<int>[][] oldCells) {
        //this.cells = (ISet<int>[][]) cells.Clone();
        //Console.WriteLine(cells);
        cells = new HashSet<int>[9][];
        for (int x = 0; x < 9; x++)
        {
            cells[x] = new HashSet<int>[9];
            for (int y = 0; y < 9; y++)
            {
                var oldSet = oldCells[x][y];
                var newSet = new HashSet<int>(oldSet);
                cells[x][y] = newSet;
            }
        }
    }
    private static void FillSet(ISet<int> set)
    {
        for (int n = 1; n <= 9; n++)
        {
            set.Add(n);
        }
    }

    public void Display()
    {
        string boldCorner = "+";
        string boldVerticalLine = "|";
        string boldHorizontalLine = "-";
        string thinLine = "·";

        string boldSeparator = MakeSeparator(boldCorner, boldHorizontalLine, false);
        string thinSeparator = MakeSeparator(boldVerticalLine, thinLine, true);

        Console.WriteLine(boldSeparator);
        for (int y = 0; y < 9; y++)
        {
            for (int cRow = 0; cRow < 3; cRow++)
            {
                Console.Write($"{boldVerticalLine} ");
                for (int x = 0; x < 9; x++)
                {
                    var currentCell = cells[x][y];
                    for (int cCol = 0; cCol < 3; cCol++)
                    {
                        int cellNum = 3 * cRow + cCol + 1;
                        if (currentCell.Contains(cellNum)) Console.Write($"{cellNum} ");
                        else Console.Write($"  ");
                    }
                    if (x % 3 == 2) Console.Write($"{boldVerticalLine} ");
                    else Console.Write($"{thinLine} ");
                }
                Console.WriteLine();
            }
            if (y % 3 == 2) Console.WriteLine(boldSeparator);
            else Console.WriteLine(thinSeparator);
        }
    }
    private static string MakeSeparator(string corner, string line, bool addLineSpace)
    {
        int squareLen = (3 * 4) * 2;
        int boardLen = squareLen * 3;

        string str = corner;

        for (int i = 0; i < boardLen; i++)
        {
            if (i % squareLen == squareLen - 1) str += corner;
            else if (i % 2 == 1 || !addLineSpace) str += line;
            else str += " ";
        }
        return str;
    }

    public void DecideCell(int x, int y, int num)
    {
        var currentCell = cells[x][y];

        if (!(1 <= num && num <= 9))
        {
            // Rethink exception type
            throw new ArgumentException($"Number {num} is not within range [1...9].");
        }
        
        if (!currentCell.Contains(num))
        {
            // Rethink exception type
            throw new ArgumentException($"Number {num} is not within the cell in position ({x}, {y}).");
        }

        cells[x][y].Clear();
        cells[x][y].Add(num);

        UpdateBoardWithCell(x, y, num);
    }
    private void UpdateBoardWithCell(int cellX, int cellY, int num) {
        for (int x = 0; x < 9; x++)
        {
            removeCellExcept(x, cellY, num, cellX, cellY);
        }
        for (int y = 0; y < 9; y++)
        {
            removeCellExcept(cellX, y, num, cellX, cellY);
        }
        int topLeftSquareCellX = cellX / 3 * 3;
        int topLeftSquareCellY = cellY / 3 * 3;
        for (int relativeX = 0; relativeX < 3; relativeX++)
        {
            for (int relativeY = 0; relativeY < 3; relativeY++)
            {
                int x = relativeX + topLeftSquareCellX;
                int y = relativeY + topLeftSquareCellY;

                removeCellExcept(x, y, num, cellX, cellY);
            }
        }
    }
    private void removeCellExcept(int x, int y, int num, int exceptX, int exceptY) {
        if (exceptX == x && exceptY == y) return;

        var currentCell = cells[x][y];

        if (!currentCell.Contains(num)) return;

        currentCell.Remove(num);

        if (currentCell.Count() == 1) {
            foreach (int n in currentCell) {
                UpdateBoardWithCell(x, y, n);
            }
        }
    }

    public object Clone() {
        return new SudokuBoard(cells);
    }
}