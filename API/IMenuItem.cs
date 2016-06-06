using System.Collections.Generic;

namespace API
{
    public interface IMenuItem
    {
        List<string> Extensions { get; }
        string Text { get; }
        void Click(string path, string filename, ClickPlace place);
    }
}
