using System;
using System.Collections.Generic;
using System.Linq;
using FileManager.Domain.Interfaces;

namespace FileManager.GUI.Application
{
    public class TreeNode
    {
        public List<TreeNode> Children { get; }
        public string Text { get; }
        public MyFile File;

        public TreeNode(MyFile file)
        {
            File = file;
            Text = file.Path.GetFileName();
            Children = new List<TreeNode>();
        }
    }

    public class FileTree
    {
        public TreeNode Root;

        public FileTree(MyFile root)
        {
            Root = new TreeNode(root);
            Root.Children.Add(null);
        }

        public void Expand(TreeNode node)
        {
            if (!(node.File is Folder))
                throw new ArgumentException();

            var dir = (Folder) node.File;
            node.Children.Clear();
            node.Children.AddRange(dir.EnumerateFiles().Select(x =>
            {
                var t = new TreeNode(x);
                if (x is Folder)
                    t.Children.Add(null);
                return t;
            }));
        }
    }
}
