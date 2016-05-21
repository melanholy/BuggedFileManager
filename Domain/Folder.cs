using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace filemanager
{
	public class Folder
	{
		public string Name { get; set; }
		public MyPath Path;
		public IEnumerable<Folder> Folders {
			get { 
				var dirs = Directory.EnumerateDirectories (Path.Join (Name));
				return dirs.Select (x => new Folder(new MyPath(x)));
			}
		}
		public IEnumerable<File> Files {
			get {
				var files = Directory.EnumerateFiles (Path.Join (Name));
				return files.Select (x => new File(new MyPath (x)));
			}
		}

		public Folder (MyPath path)
		{
			Path = path;
			Name = path.GetFileName ();
		}

		public static bool Create(MyPath path)
		{
			if (Directory.Exists (path.Path))
				return false;
			Directory.CreateDirectory (path.Path);
			return true;
		}

		public void Delete()
		{
			Directory.Delete (Path.Path);
		}
	}
}
