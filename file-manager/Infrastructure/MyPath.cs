using System;
using System.Text.RegularExpressions;

namespace filemanager.Infrastructure
{
	public class MyPath
	{
		public string PathStr { get; }

		public MyPath (string pathStr)
		{
            if (pathStr == null)
                throw new ArgumentException();

		    pathStr = pathStr.Replace("\\", "/");
            if (!IsCorrectMyPath(pathStr))
                throw new ArgumentException();
			PathStr = pathStr;
		}

        public static bool IsCorrectMyPath(string path)
        {
            return Regex.IsMatch(path, "(?:(?:^\\w:)|(?:^/+))(?:[^/]+/+)*");
        }

        public MyPath Join(string file)
		{
			return new MyPath($"{PathStr}/{file}");
		}

		public string GetFileName()
		{
            var idx = PathStr.LastIndexOf("/", StringComparison.Ordinal);
            var name =  idx < 0 ? "" : PathStr.Substring(idx + 1);
            return name == "" ? PathStr : name;
		}

		public string GetExt()
		{
		    var idx = PathStr.LastIndexOf(".", StringComparison.Ordinal);
		    return idx < 0 ? "" : PathStr.Substring(idx + 1);
		}
	}
}
