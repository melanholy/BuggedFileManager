namespace FileManager.API
{
    /// <summary>
    /// Вспомогательный енум для IMenuItem, показывающий, где произошел 
    /// клик, который вызвал открытие меню.
    /// </summary>
    public enum ClickPlace
    {
        Folder,
        Empty,
        File
    }
}