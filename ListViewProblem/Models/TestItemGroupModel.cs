using System;
using MvvmHelpers;

namespace ListViewProblem.Models
{
    public class TestItemGroupModel : ObservableRangeCollection<Item>
    {
        private TestItemModelGroup _itemGroup;
        private ObservableRangeCollection<Item> _itemModels = new ObservableRangeCollection<Item>();

        public TestItemGroupModel(TestItemModelGroup items)
        {
            _itemGroup = items;

            foreach (var item in items.Items)
            {
                _itemModels.Add(item);
            }
        }

        public string GroupTitle => _itemGroup.GroupTitle;

        private bool _expanded;
        public bool Expanded
        {
            get => _expanded;
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Expanded"));
                    if (_expanded)
                    {
                        AddRange(_itemModels);
                    }
                    else
                    {
                        Clear();
                    }
                }
            }
        }
    }
}
