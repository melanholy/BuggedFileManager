using System;
using System.Text.RegularExpressions;

namespace FileManager.Domain.Models
{
	public class MyPath
	{
		public string PathStr { get; }

		public MyPath (string pathStr)
		{
            if (pathStr == null)
                throw new ArgumentException();

		    pathStr = Regex.Replace(pathStr, "(?:\\+)|(?://+)", "/");
		    if (pathStr.IndexOf("/", StringComparison.Ordinal) != pathStr.LastIndexOf("/", StringComparison.Ordinal))
		        pathStr = pathStr.TrimEnd('/');
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

	    public MyPath GetDirectory()
	    {
            var idx = PathStr.LastIndexOf("/", StringComparison.Ordinal);
	        string str;
	        if (idx == PathStr.IndexOf("/", StringComparison.Ordinal))
	            str = PathStr.Substring(0, idx + 1);
	        else
	            str = PathStr.Substring(0, idx);

            return new MyPath(str);
        }

		public string GetExt()
		{
		    var idx = PathStr.LastIndexOf(".", StringComparison.Ordinal);
		    return idx < 0 ? "" : PathStr.Substring(idx + 1);
		}
	}
}
