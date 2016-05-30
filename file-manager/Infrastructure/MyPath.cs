﻿using System;

namespace filemanager.Infrastructure
{
	public class MyPath
	{
		public string Path { get; }

		public MyPath (string path)
		{
		    path = path.Replace("\\", "/");
            if (!Helpers.IsCorrectMyPath(path))
                throw new ArgumentException();
			Path = path;
		}

		public MyPath Join(string file)
		{
			return new MyPath($"{Path}/{file}");
		}

		public string GetFileName()
		{
            var idx = Path.LastIndexOf("/", StringComparison.Ordinal);
            var name =  idx < 0 ? "" : Path.Substring(idx + 1);
            return name == "" ? Path : name;
		}

		public string GetExt()
		{
		    var idx = Path.LastIndexOf(".", StringComparison.Ordinal);
		    return idx < 0 ? "" : Path.Substring(idx + 1);
		}
	}
}