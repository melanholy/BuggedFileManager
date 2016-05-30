using System.Collections.Generic;

namespace filemanager.Infrastructure
{
	public class HistoryKeeper<TObject>
	{
		private TObject Current;
	    private readonly Stack<TObject> HistoryBck;
	    private readonly Stack<TObject> HistoryFwd;

	    public HistoryKeeper(TObject start)
	    {
	        Current = start;
	        HistoryBck = new Stack<TObject>();
            HistoryFwd = new Stack<TObject>();
	    }

		public void Do(TObject obj)
		{
            HistoryFwd.Clear();
            HistoryBck.Push(Current);

		    Current = obj;
		}

	    public TObject GoBack()
	    {
            HistoryFwd.Push(Current);

            Current = HistoryBck.Pop();
	        return Current;
	    }

	    public TObject GoForward()
	    {
            HistoryBck.Push(Current);

            Current = HistoryFwd.Pop();
	        return Current;
	    }
	}
}
