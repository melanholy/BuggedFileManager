using System.Collections.Generic;

namespace FileManager.API
{
    /// <summary>
    /// Ассоциации для файлов. При дабл-клике на файл должен вызываться
    /// соответствующий IFileOpener.
    /// Как использовать: при дабл-клике нужно вызвать метод Open соответствующего
    /// расширению файла IFileOpener'a. Метод может выполняться достаточно долго,
    /// поэтому стоит делать асинхронно.
    /// </summary>
    public interface IFileOpener
    {
        List<string> Extensions { get; }
        void Open(string file);
    }
}