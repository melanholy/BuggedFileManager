using System.Collections.Generic;

namespace FileManager.API
{
    /// <summary>
    /// Строка для меню, раскрывающемся при нажатии на "диск" правой кнопкой 
    /// мыши. Место нажатия передается через енум ClickPlace.
    /// Как использовать: при нажатии правой кнопкой мыши на "диск" должно открываться
    /// меню, в которое нужно добавить соответствующий расширению этого файла
    /// пункт с именем Text, при нажатии на который должен вызываться метод Click.
    /// Метод может выполняться достаточно долго, поэтому стоит делать асинхронно.
    /// </summary>
    public interface IMenuItem
    {
        List<string> Extensions { get; }
        string Text { get; }
        void Click(string path, string filename, ClickPlace place);
    }
}
