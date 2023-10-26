
var board = new SudokuBoard();
var prevBoards = new Stack<SudokuBoard>();

/*/
board.DecideCell(0, 0, 1);
board.DecideCell(0, 1, 2);
board.DecideCell(0, 2, 3);
board.DecideCell(1, 0, 4);
board.DecideCell(1, 1, 5);
board.DecideCell(1, 2, 6);
//*/

board.Display();

int pos = 0;
Console.WriteLine($"x: {pos % 9}, y: {pos / 9}");



while (true)
{
    int num = 0;

    try {
        num = ConsoleInput.AskForOneNumInput();
    } catch (Exception e) {
        Console.WriteLine(e);
        continue;
    }

    if (num == 0) {
        pos++;
        Console.WriteLine($"x: {pos % 9}, y: {pos / 9}");
        continue;
    }

    if (num == -1) 
    {
        if (pos == 0) continue;

        pos--;
        Console.WriteLine($"x: {pos % 9}, y: {pos / 9}");

        if (prevBoards.Count() == 0) continue;

        board = prevBoards.Pop();
        
        Console.Clear();
        board.Display();
        continue;
    }

    var prevBoard = (SudokuBoard) board.Clone();

    try {
        board.DecideCell(pos % 9, pos / 9, num);
        pos++;
    } catch (ArgumentException e) {
        Console.WriteLine(e);
        continue;
    }
    
    prevBoards.Push(prevBoard);

    Console.Clear();
    board.Display();
    Console.WriteLine($"x: {pos % 9}, y: {pos / 9}");
}










while (true)
{
    Info? info;

    try {
        info = ConsoleInput.AskForInput();
    } catch (Exception e) {
        Console.WriteLine(e);
        continue;
    }

    if (info.x == -1) 
    {
        if (prevBoards.Count() == 0) continue;

        board = prevBoards.Pop();
        
        Console.Clear();
        board.Display();
        continue;
    }

    var prevBoard = (SudokuBoard) board.Clone();

    try {
        board.DecideCell(info.x, info.y, info.n);
    } catch (ArgumentException e) {
        Console.WriteLine(e);
        continue;
    }

    prevBoards.Push(prevBoard);

    Console.Clear();
    board.Display();
}

