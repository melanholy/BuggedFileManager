using System;
using System.Collections.Generic;
using System.Linq;

namespace filemanager
{
	public class FileManager
	{
		private MyPath Current;

		public IEnumerable<Folder> GetRoots()
		{
			return System.IO.DriveInfo.GetDrives ()
				.Select (x => new Folder (new MyPath (x.Name)));
		}

		public void GoToFolder(Folder folder)
		{
			Current = Current.Join (folder.Path.Path);
		}

		public void CreateFolder(string name)
		{
			Folder.Create (Current.Join (name));
		}

		public void CreateFile(string name)
		{
			File.Create (Current.Join (name));
		}

		public void Delete(Folder folder)
		{
			folder.Delete ();
		}

		public void Delete(File file)
		{
			file.Delete ();
		}
	}
}

