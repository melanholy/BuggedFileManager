namespace filemanager.Domain.Interfaces
{
    /// <summary>
    /// Вспомогательный интерфейс для копирования/перемещения. Содержит в 
    /// себе логику копирования/перемещения. Копирование - KeepOrogonal = true,
    /// иначе перемещение.
    /// Как использовать: нужно создать IFileMoveProcess, который запомнит,
    /// какой файл должен быть скопирован. Для выполнения самого копирования
    /// нужно вызвать метод To, передав ему файл, в который нужно произвести
    /// копирование/перемещение.
    /// </summary>
    public interface IFileMoveProcess
    {
        bool KeepOriginal { get; set; }
        void To(IFile destFile);
    }
}