using System;
using System.Collections.Generic;

namespace API
{
    /// <summary>
    /// Иконка для файлов.
    /// Как использовать: при необходимости отобразить новый файл нужно
    /// по его расширению взять соответствующий IFileIcon и получить
    /// изображение из указанного Uri.
    /// </summary>
    public interface IFileIcon
    {
        List<string> Extensions { get; }
        Uri IconUri { get; }
    }
}