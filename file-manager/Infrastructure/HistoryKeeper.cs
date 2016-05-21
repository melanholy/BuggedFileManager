using System.Collections.Generic;

namespace filemanager.Infrastructure
{
	public class HistoryKeeper
	{
		private MyPath Current;
	    private readonly Stack<MyPath> HistoryBck;
	    private readonly Stack<MyPath> HistoryFwd;

	    public HistoryKeeper(MyPath start)
	    {
	        Current = start;
	        HistoryBck = new Stack<MyPath>();
            HistoryFwd = new Stack<MyPath>();
	    }

		public void GoToFolder(string folder)
		{
            HistoryBck.Push(Current);
            Current = Current.Join(folder);
            HistoryFwd.Clear();
        }

	    public void GoBack()
	    {
            HistoryFwd.Push(Current);
            Current = HistoryBck.Pop();
	    }

	    public void GoForward()
	    {
            HistoryBck.Push(Current);
            Current = HistoryFwd.Pop();
        }
	}
}
