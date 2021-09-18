using Dice.App.Common;
using Dice.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice.App.Concrete
{
    public class MenuActionService: Program<MenuAction>
    {
        public MenuActionService()
        {
            Initialize();
        }
        public List<MenuAction> GetMenuActionsByName (string menuName)
        {
            List<MenuAction> result = new List<MenuAction>();
            foreach(var menuAction in Items)
            {
                if (menuAction.MenuName == menuName)
                {
                    result.Add(menuAction);
                }
            }
            return result;
        }
        public void Initialize()
        {
            AddItem(new MenuAction(1, "Nowa gra", "Main"));
            AddItem(new MenuAction(2, "Wczytaj zapisaną grę", "Main"));
            AddItem(new MenuAction(3, "Pomoc", "Main"));
            AddItem(new MenuAction(1, "Zasady gry", "HelpMain"));
            AddItem(new MenuAction(2, "Instrukcje", "HelpMain"));

        }
        public void ViewRules()
        {
            Console.WriteLine("Najpopularniejszą ze wszystkich rodzajów gry w kości jest Yahtzee (generał). Do tej odmiany potrzebujemy pięciu kości do gry. Rozgrywka trwa 13 kolejek. W każdej kolejce mamy prawo do 3 rzutów kośćmi. Pierwszy rzut odbywa się zawsze pięcioma kostkami. W kolejnych dwóch rzutach gracz używa taką ilość kostek, jaką uważa za stosowną - pasującą do jego koncepcji. Po skończonej swojej turze rzutów, gracz ma obowiązek zapisać układ oczek uzyskany na kostkach i dopasować go do jednej z 13 kategorii tabeli. Kategoria, która zostanie wybrana, musi być przemyślana, ponieważ nie będzie opcji ponownego jej wyboru.");
            Console.WriteLine();
            Console.WriteLine("Tabelę dzielimy na część górną oraz dolną, a zsumowane ich wartości - dają nam wynik końcowy. W górnej części znajdziemy podział na 6 kategorii (jedynki, dwójki, trójki, czwórki, piątki oraz szóstki).");
            Console.WriteLine("Dolna część przedstawia siedem kategorii, tj. “trzy jednakowe”, “cztery jednakowe”, “ful”, “mały strit”, “duży strit”, “generał” oraz “szansa”. ");
            Console.WriteLine(" - Tak jak nazwa może wskazywać “trzy jednakowe” oraz “cztery jednakowe” to kombinacja, w której mamy trzy lub cztery jednakowe wartości na kościach w danej kolejce rzutów. W przypadku rzucenia 3, 5, 3, 3 i 3 oczek wypadają nam obydwie kombinacje. Sami decydujemy, którą wybieramy. W obu przypadkach wartością jest suma oczek z pięciu kostek. W tym przypadku będzie taka sama tj. 17 oczek.");
            Console.WriteLine(" - “Ful”, tak jak w pokerze, jest to kombinacja, trójki i pary, np. wyrzucenie 5,5,5,3,3, lub 4,4,4,1,1 daje tą samą wartość - czyli 25pkt. W przypadku niewyrzucenia fula, wpisujemy 0pkt. Tak samo jak w przypadku górnej tabeli, muszą być one uzupełnione.");
            Console.WriteLine(" - Do zdobycia małego strita potrzeba czterech kolejnie po sobie następujących cyfr (wartości kostki), przykładowo: 1,2,3,4,6 lub 1,3,4,5,6. Takie ułożenie kości pozwala zdobyć 30 pkt.");
            Console.WriteLine(" - Duży strit jest rozszerzeniem małego, w tej kombinacji potrzeba aż pięciu kolejnych cyfr, czyli 1,2,3,4,5 lub 2,3,4,5,6. Za dużego strita dostajemy 40 pkt.");
            Console.WriteLine(" - Zdobycie “generała” polega na wyrzuceniu pięciu kostek o tej samej liczbie oczek np. 5,5,5,5,5 lub 2,2,2,2,2 – dostajemy za to 50 pkt. Każdy kolejny wyrzucony generał, może być traktowany jako joker, w przypadku, gdy rubryka generał oraz cała górna tabela gracza została wcześniej uzupełniona. Dżokera zapisuje się w dolnych kategoriach tabeli punktacji, otrzymując stosowną liczbę punktów, w przypadku fula – 25, małego strita – 30, zaś dużego - 40 punktów. Za każdego kolejnego generała gracz otrzymuje 100 pkt premii, ale tylko wtedy, gdy pierwszy został zapisany w kategorii generał.");
            Console.WriteLine(" - Szansa to suma wszystkich kości wyrzuconych w danej turze np. 3,5,2,1,4 – to 15pkt, zaś 6,6,6,3,2 daje nam 23pkt.");
            Console.WriteLine("Po wypełnieniu całej tabeli, pora na zsumowanie wszystkich punktów z obu tabel, górnej i dolnej. Wygrywa gracz z największą ilością punktów, kolejne miejsca zajmują odpowiednio gracze z coraz to niższą sumą punktów.");
            Console.WriteLine("Na podstawie holdemshop.pl");
            Console.WriteLine();
            Console.WriteLine();
        }
        public void ViewInstruction()
        {
            Console.WriteLine("Rozgrywka rozpoczyna się od wyświetlenia tabeli wyników. Myślnikami oznaczone są pola, które będziemy musieli wypełnić.");
            Console.WriteLine("W nawiasach kwadratowych [ ] oznaczone są wartośći wylosowanych kości, natomiast w nawiasach okrągłych ( ) ich numery. Następnie wybieramy liczbę kości jaką chcemy zostawić. Po zatwierdzeniu wpisujemy ich numery (każdorazowo potwierdzając enterem). Po trzech rzutach pojawi się tabela z możliwymi polami oraz wartościami, które możemy uzupełnić. Numer pola, który należy wpisać przy wyborze znajduje się w pierwszej kolumnie");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
