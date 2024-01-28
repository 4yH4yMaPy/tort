using System.Text;
using System.Xml.Linq;

namespace Tortiki
{
    internal class Elements
    {

        public Elements(int element, string description, int price, Action action)
        {

            this.EL = element;
            this.Description = description;
            this.Price = price;
            this.Action = action;
        }

   
        public int EL { get; set; }      
        public string Description { get; set; } 
        public int Price { get; set; }
        public Action Action { get; set; }
    }

    public static class Global
    {
        public static int Price = 0;

        public static List<string> Order = new List<string> { };
    }
    public class Menu 
{
    public Menu PlayMenu { get; set; }
    public string Imya { get; set; }
    public string Arrow { get; set; }
    public ConsoleColor ArrowColour { get; set; }
    public ConsoleColor ForegroundColour { get; set; }
    public ConsoleColor MenuItemColour { get; set; }
    public ConsoleColor ImyaColour { get; set; }

    public int AllPrice { get; private set; } = 0;

    private List<Elements> ItemList;
    public int[] prices = new int[] { };

    private int position;
    private bool Exit;
    public int Cena;

    public Menu(string arrow = "->")
    {
        ItemList = new List<Elements>();
        position = 0;

        this.Arrow = arrow;
        ArrowColour = ConsoleColor.Blue;
        ForegroundColour = ConsoleColor.White;
        MenuItemColour = ConsoleColor.White;
        ImyaColour = ConsoleColor.Red;
    }
        public void Draw()
        {
            Console.Clear();
            Console.WriteLine(Imya);

            for (int i = 0; i < ItemList.Count; i++)
            {
                if (i == position)
                {
                    Console.ForegroundColor = ArrowColour;
                    Console.Write(Arrow + " ");
                }
                else
                {
                    Console.Write(new string(' ', Arrow.Length + 1));
                }

                Console.WriteLine(ItemList[i].Description);

                if (i == position)
                {
                    Console.ForegroundColor = ForegroundColour;
                }
            }

            Console.WriteLine($"Цена: {Global.Price}");

            if (Global.Order.Count > 0)
            {
                Console.WriteLine("Заказ: ");
                foreach (string item in Global.Order)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("Заказа нет");
            }
        }
        public void ShowMenu()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Draw();
        Exit = false;
        while (!Exit)
        {
            MenuUpdate();

        }
    }

    public void HideMenu()
    {

        Exit = true;
    }

    public void MenuUpdate()
    {
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.Enter:
                {
                    Console.Clear();
                    Console.CursorVisible = true;
                    ItemList[position].Action();
                    Global.Price += ItemList[position].Price;
                    Global.Order.Add(ItemList[position].Description);
                    Console.CursorVisible = false;
                    Console.Clear();
                    Draw();
                    break;
                }
            case ConsoleKey.Escape:
                {
                    if (PlayMenu != null)
                    {
                        HideMenu();
                    }
                    break;
                }
            case ConsoleKey.UpArrow:
                {
                    position--;
                    if (position < 0)
                    {
                        position++;
                        Console.Clear();
                        Draw();

                    }
                    break;
                }
            case ConsoleKey.DownArrow:
                {

                    if (position < (ItemList.Count - 1))
                    {
                        position++;
                        Console.Clear();
                        Draw();

                    }
                    break;
                }
        }
    }

        public bool AddItem(int element, string description, int price, Action action)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));
            if (action == null) throw new ArgumentNullException(nameof(action));

            if (ItemList.Any(item => item.EL == element))
                return false;

            var newItem = new Elements(element, description, price, action);
            ItemList.Add(newItem);

            return true;
        }
    }
internal class Program
{
    static void Main()
    {
        Menu mainMenu = new Menu();
        Menu Forma= new Menu("->");
        Forma.Imya = "Форма ";
        Forma.AddItem(0, "Квадрат - 100", 100, Forma.HideMenu);
        Forma.AddItem(1, "Круг - 300", 300, Forma.HideMenu);
        Forma.AddItem(2, "куб - 400", 400, Forma.HideMenu);
        Forma.PlayMenu = mainMenu;



        Menu Size= new Menu("->");
        Size.Imya = "Размер";
        Size.AddItem(0, "маленький - 430", 430, Size.HideMenu);
        Size.AddItem(1, "большой - 560", 560, Size.HideMenu);
        Size.AddItem(2, "огромный - 654", 654, Size.HideMenu);



        Size.PlayMenu = mainMenu;

        Menu Taste = new Menu("->");
        Taste.Imya = "Вкус";
        Taste.AddItem(0, "ежевика - 300", 300, Taste.HideMenu);
        Taste.AddItem(1, "брусника - 200", 200, Taste.HideMenu);
        Taste.AddItem(2, "манго- 100", 100, Taste.HideMenu);
        Taste.prices = new int[] { 300, 350, 500 };
        Taste.PlayMenu = mainMenu;

        Menu Nachinka = new Menu("->");
        Nachinka.Imya = "Начинка";
        Nachinka.AddItem(0, "Шоколад - 200", 200, Nachinka.HideMenu);
        Nachinka.AddItem(1, "Малина - 300", 300, Nachinka.HideMenu);
        Nachinka.AddItem(2, "Белый шоколад - 400", 400, Nachinka.HideMenu);
        Nachinka.prices = new int[] { 250, 300, 400};


        Nachinka.PlayMenu = mainMenu;

        Menu Yarys = new Menu("->");
        Yarys.Imya = "Количество ярусов";
        Yarys.AddItem(0, "Один ярус - 564", 564, Yarys.HideMenu);
        Yarys.AddItem(1, "Два яруса - 843", 843, Yarys.HideMenu);
        Yarys.AddItem(2, "Три яруса - 923", 923, Yarys.HideMenu);

        Yarys.PlayMenu = mainMenu;

        Menu Dekor = new Menu("->");
        Dekor.Imya = "Декорация";
        Dekor.AddItem(0, "Кружки - 100", 100, Dekor.HideMenu);
        Dekor.AddItem(1, "Еще кружки - 100", 100, Dekor.HideMenu);
        Dekor.AddItem(3, "Волны-100", 100, Dekor.HideMenu);

        Dekor.PlayMenu = mainMenu;

        mainMenu.Imya = " Торты";
        mainMenu.AddItem(0, "Форма", 0, Forma.ShowMenu);
        mainMenu.AddItem(1, "Размер", 0, Size.ShowMenu);
        mainMenu.AddItem(2, "Вкус", 0, Taste.ShowMenu);
        mainMenu.AddItem(3, "Начинка", 0, Nachinka.ShowMenu);
        mainMenu.AddItem(4, "Количество ярусов", 0, Yarys.ShowMenu);
        mainMenu.AddItem(5, "Декорация", 0, Dekor.ShowMenu);
        mainMenu.AddItem(6, "Конец заказа", 0, Exit);

        mainMenu.ShowMenu();

    }

    private static void Exit()
    {
        Console.WriteLine("Ваш заказ оформляется)");

        string path = "C:\\Users\\BAM\\Desktop\\История действий.txt";
        StringBuilder orderDescription = new StringBuilder("Заказ:\n");

        foreach (string item in Global.Order)
        {
            orderDescription.AppendLine(item);
        }

        orderDescription.AppendLine("Цена: " + Global.Price);

        File.AppendAllText(path, orderDescription.ToString());

        Environment.Exit(0);
    }
    
   }
}
