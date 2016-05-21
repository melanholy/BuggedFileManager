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

		public MyPath GoToFolder(string folder)
		{
            HistoryFwd.Clear();
            HistoryBck.Push(Current);

            Current = Current.Join(folder);
		    return Current;
		}

	    public MyPath GoBack()
	    {
            HistoryFwd.Push(Current);

            Current = HistoryBck.Pop();
	        return Current;
	    }

	    public MyPath GoForward()
	    {
            HistoryBck.Push(Current);

            Current = HistoryFwd.Pop();
	        return Current;
	    }
	}
}
