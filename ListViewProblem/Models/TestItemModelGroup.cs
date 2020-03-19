using System;
using System.Collections.Generic;

namespace ListViewProblem.Models
{
    public class TestItemModelGroup
    {
        public string GroupTitle { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
