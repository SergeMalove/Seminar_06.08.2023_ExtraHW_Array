// Программа на вход получает натуральное число. Необходимо его преобразовать таким образом, чтобы все нечетные числа стояли впереди, 
// а все четные позади. При этом внутри четных и нечетных чисел очередность должна сохраняться. Результатом должно быть новое число, 
// а не просто вывод на печать цифр в нужном порядке. Использовать можно только арифметические действия без работы со строкой.
// Пример:
// 12345 -> 13524
// 3658563 -> 3553686
// 48 -> 48
// 5497 -> 5974
// Для решения может понадобится функция возведения в степень и приведение типов. По крайней мере мне они понадобились:)
// Чтобы возвести в степень число используйте функцию Math.Pow(value, degree), где value - число, которое возводят в степень, а degree - собственно степень.
// Эта функция возвращает double. Если нужно привести полученный результат к int, используйте следующую конструкцию: (int)Math.Pow(value, degree)

// P. S. Я не использовал массивы, только цикл и ветвление.

using System.Text.RegularExpressions;

string EnterNumber()     // Функция ввода числа с проверкой. Отрицательные числа также принимаются
{
    Regex regex = new Regex("-[0-9]+$|^[0-9]+$");   // вот тут лучше было бы использовать \d, но у меня почему-то выкидывает ошибку
    string numberString = "";

    while (!regex.IsMatch(numberString))
    {
        System.Console.Write("Введите число: ");
        numberString = Console.ReadLine()!;

        if (!regex.IsMatch(numberString))
        {
            System.Console.WriteLine("Введено не число. Пожалуйста повторите ввод.");
        }
    }

    return numberString;
}

char[] MoveDigits(char[] array)     // Функция перемещения цифр. Для отрицательных числе тоже работает.
{
    int arrLengh = array.Length;
    bool flag = true;
    int minusIndicator = 1;

    if (array[0] == '-')          // По идее это костыль, что бы работало с отрицательными числами, но ничего лучше я пока придумать не смог.
    {
        array[0] = '1';
        minusIndicator = 0;
    }

    while (flag == true)          // Не самый оптимальный алгоритм для перемещения чисел, т.к. он будет проходить по уже выставленным числам,
    {                             // но в целом для не сильно длинного массива цифр пойдет
        flag = false;

        for (int i = 0; i < arrLengh - 1; i++)
        {
            if (array[i] % 2 == 0 && array[i + 1] % 2 != 0)    // по крайней мере за одну итерацию по циклу перемещение элементов будет
            {                                                  // происходить сразу от вначала и от конца.
                char temp = array[i];
                array[i] = array[i + 1];
                array[i + 1] = temp;
                flag = true;
            }
            if (array[arrLengh - i - 2] % 2 == 0 && array[arrLengh - i - 1] % 2 != 0)
            {
                char temp = array[arrLengh - i - 2];
                array[arrLengh - i - 2] = array[arrLengh - i - 1];
                array[arrLengh - i - 1] = temp;
                flag = true;
            }
        }
    }

    if (minusIndicator == 0)      // окончание костыля
    {
        array[0] = '-';
    }

    return array;
}

void printArray(char[] array)     // Функция вывода массива на экран
{
    System.Console.WriteLine();
    System.Console.Write("Результат работы программы: ");
    foreach (var c in array)
    {
        System.Console.Write(c);
    }
    System.Console.WriteLine("\n");
}

char[] numberArray = MoveDigits(EnterNumber().ToCharArray());  
printArray(numberArray);                      // Вот тут в принципе можно вообще вставить в аргумент функции строку выше, но наверно это уже перебор =)))