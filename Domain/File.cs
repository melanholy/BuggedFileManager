using System;

namespace filemanager
{
	public class File
	{
		public MyPath Path;
		public string Name { 
			get { return Path.GetFileName (); }
		}
		public string Extension {
			get { return Path.GetExt (); }
		}

		public File (MyPath path)
		{
			Path = path;
		}

		public static bool Create(MyPath path)
		{
			if (System.IO.File.Exists (path.Path))
				return false;
			System.IO.File.Create (path.Path);
			return true;
		}

		public void Delete()
		{
			System.IO.File.Delete (Path.Path);
		}
	}
}
