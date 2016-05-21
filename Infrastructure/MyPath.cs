using System;

namespace filemanager
{
	public class MyPath
	{
		public string Path { get; }

		public MyPath (string path)
		{
			Path = System.IO.Path.GetFullPath (path);
			if (!System.IO.File.Exists (path) && !System.IO.Directory.Exists (path))
				throw new ArgumentException ();
		}

		public MyPath Join(string file)
		{
			return new MyPath(System.IO.Path.Combine (Path, file));
		}

		public string GetFileName()
		{
			return System.IO.Path.GetFileName (Path);
		}

		public string GetExt()
		{
			return System.IO.Path.GetExtension (Path);
		}
	}
}

