using System.Collections.Generic;

public class InventoryPresenter
{
    #region Source
    public class Source : ObservableList<ItemPair>
    {
        public Source(IEnumerable<ItemPair> original)
        {
            foreach (var item in original)
            {
                Add(item);
            }
        }
    }
    public Source source;
    #endregion

    #region Add Command
    public class AddCommand
    {
        private InventoryDataModel _dataModel;

        public AddCommand(InventoryDataModel dataModel)
        {
            _dataModel = dataModel;
        }

        public bool CanExecute(ItemPair item)
        {
            return true;
        }

        public void Execute(ItemPair item)
        {
            _dataModel.Add(item);
        }

        public bool TryExecute(ItemPair item)
        {
            if (CanExecute(item))
            {
                Execute(item);
                return true;
            }

            return false;
        }
    }
    public AddCommand addCommand;
    #endregion

    #region Remove Command
    public class RemoveCommand
    {
        private InventoryDataModel _dataModel;

        public RemoveCommand(InventoryDataModel dataModel)
        {
            _dataModel = dataModel;
        }

        public bool CanExecute(ItemPair item)
        {
            return true;
        }

        public void Execute(ItemPair item)
        {
            _dataModel.Remove(item);
        }

        public bool TryExecute(ItemPair item)
        {
            if (CanExecute(item))
            {
                Execute(item);
                return true;
            }

            return false;
        }
    }
    public RemoveCommand removeCommand;
    #endregion

    #region Swap Command
    public class SwapCommand
    {
        private InventoryDataModel _dataModel;

        public SwapCommand(InventoryDataModel dataModel)
        {
            _dataModel = dataModel;
        }

        public bool CanExecute(ItemPair item1, ItemPair item2)
        {
            return _dataModel.Contains(item1) && _dataModel.Contains(item2);
        }

        public void Execute(ItemPair item1, ItemPair item2)
        {
            int index1 = _dataModel.IndexOf(item1);
            int index2 = _dataModel.IndexOf(item2);
            _dataModel[index1] = item2;
            _dataModel[index2] = item1;
        }

        public bool TryExecute(ItemPair item1, ItemPair item2)
        {
            if (CanExecute(item1, item2))
            {
                Execute(item1, item2);
                return true;
            }

            return false;
        }
    }
    public SwapCommand swapCommand;
    #endregion


    public InventoryPresenter()
    {
        source = new Source(InventoryDataModel.instance);
        InventoryDataModel.instance.itemAdded += (item) => source.Add(item);
        InventoryDataModel.instance.itemRemoved += (item) => source.Remove(item);
        InventoryDataModel.instance.itemChanged += (index, item) => source[index] = item;
        //InventoryDataModel.instance.listChanged += ???
        addCommand = new AddCommand(InventoryDataModel.instance);
        removeCommand = new RemoveCommand(InventoryDataModel.instance);
        swapCommand = new SwapCommand(InventoryDataModel.instance);
    }
}