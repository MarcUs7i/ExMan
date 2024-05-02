using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExMan
{
    public class Menu
    {
        public int SelectedIndex = 0;
        private string starterMessage;
        private string[] options;
        private bool showFooter;
        private string[] endMessage;

        private int pagesRequiredForMenu;
        private int topPositionToStartFrom;
        private int consoleHeight = Console.WindowHeight - 1;
        private int choicesPerPage;

        public Menu(string aStarterMessage, string[] aOptions,string[] aEndMessage, bool aShowFooter) {
            starterMessage = aStarterMessage;
            options = aOptions;
            showFooter = aShowFooter;
            endMessage = aEndMessage;
        }

        public void Run()
        {
            int currentPage = 1;
            Console.Clear();
            ConsoleKey keyPressed;
            Console.WriteLine(starterMessage);

            CalculateRequiredValues();
            RenderMenu(currentPage);
            do
            {
                keyPressed = Console.ReadKey(true).Key;
                int oldPage = currentPage;
                int oldOption = 0;
                switch (keyPressed)
                {
                    case ConsoleKey.DownArrow:
                        ++SelectedIndex;
                        if (SelectedIndex >= choicesPerPage * currentPage)
                        {
                            currentPage++;
                        }
                        else if (SelectedIndex >= options.Length)
                        {
                            currentPage = 1;
                            SelectedIndex = 0;
                        }
                        if (currentPage == oldPage) oldOption = SelectedIndex - 1;

                        break;
                    case ConsoleKey.UpArrow:
                        --SelectedIndex;
                        
                        if (SelectedIndex < 0)
                        {
                            currentPage = pagesRequiredForMenu;
                            SelectedIndex = options.Length - 1;
                        }
                        else if (SelectedIndex < choicesPerPage * (currentPage - 1))
                        {
                            currentPage--;
                        }
                        if (currentPage == oldPage) oldOption = SelectedIndex + 1;

                        break;
                }
                
                if (currentPage != oldPage) RenderMenu(currentPage);
                else if (keyPressed != ConsoleKey.Escape && keyPressed != ConsoleKey.Enter) UpdateMenu(oldOption, SelectedIndex);
            } while (keyPressed is not ConsoleKey.Enter and not ConsoleKey.Escape);
            if (keyPressed == ConsoleKey.Escape) SelectedIndex = -1;
        }

        private void RenderMenu(int page)
        {
            Console.SetCursorPosition(0, topPositionToStartFrom);
            for (int i = (page-1)*choicesPerPage; i < choicesPerPage*page; i++)
            {
                ClearLine();
                if(i < options.Length) WriteOption(options[i], SelectedIndex == i);
                Console.Write(Environment.NewLine);
            }

            if (showFooter)
            {
                foreach (var line in endMessage)
                {
                    Console.WriteLine(line);
                }
            }
        }

        private void UpdateMenu(int previousOption, int newOption)
        {
            if (previousOption < 0) previousOption = options.Length - 1;
            if (newOption >= options.Length) newOption = 0;
            if (previousOption >= options.Length) previousOption = 0;
            if (newOption < 0) newOption = options.Length - 1;

            Console.SetCursorPosition(0, topPositionToStartFrom + (previousOption % (choicesPerPage)));
            ClearLine();
            WriteOption(options[previousOption], false);

            Console.SetCursorPosition(0, Console.CursorTop + (newOption - previousOption));
            ClearLine();
            WriteOption(options[newOption], true);
        }

        private void ClearLine()
        {
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        private void CalculateRequiredValues()
        {
            topPositionToStartFrom = Console.CursorTop;
            choicesPerPage = consoleHeight - topPositionToStartFrom - (showFooter ? 1 + endMessage.Length : 0);
            pagesRequiredForMenu = (int)Math.Ceiling((double)options.Length / choicesPerPage);
        }

        private void WriteOption(string option, bool selected) {
            if (selected)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write("");
            }
            Console.Write($"{option}");

            Console.ResetColor();
        }
    }
}
