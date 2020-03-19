using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ListViewProblem.Models;
using ListViewProblem.Views;
using MvvmHelpers;
using System.Windows.Input;
using System.Linq;

namespace ListViewProblem.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public ObservableRangeCollection<TestItemGroupModel> GroupItems { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            GroupItems = new ObservableRangeCollection<TestItemGroupModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        public ICommand ExpandGroup => new Command<TestItemGroupModel>(group =>
        {
            if (!group.Expanded && GroupItems.Any(x => x.Expanded))
                GroupItems.FirstOrDefault(x => x.Expanded).Expanded = false;
            group.Expanded = !group.Expanded;
        });

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //Items.Clear();
                //var items = await DataStore.GetItemsAsync(true);
                //foreach (var item in items)
                //{
                //    Items.Add(item);
                //}

                GroupItems.Clear();

                var groups = await DataStore.GetGroups();

                foreach (var group in groups)
                    GroupItems.Add(group);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}