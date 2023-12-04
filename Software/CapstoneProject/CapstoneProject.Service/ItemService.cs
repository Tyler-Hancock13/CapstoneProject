using CapstoneProject.Model.Entities;
using CapstoneProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Service
{
    public class ItemService
    {
        private PurchaseOrderItem _item;
        private PurchaseOrderItem item = new PurchaseOrderItem();

        public bool InsertItem(PurchaseOrderItem item)
        {
            _item = item;

            _item.Status = Model.Types.ItemStatus.Pending;
            ItemRepo repo = new ItemRepo();

            return repo.InsertItem(item);
        }

        public bool UpdateItem(PurchaseOrderItem item)
        {
            _item = item;

            return new ItemRepo().UpdateItem(item);
        }

        public bool DeleteItem(int id)
        {
            return new ItemRepo().DeleteItem(id);
        }

        public List<PurchaseOrderItem> GetPurchaseOrderItemById(int id)
        {
            return new ItemRepo().GetItemsById(id);
        }

        public PurchaseOrderItem Get(int id)
        {
            return new ItemRepo().Get(id);
        }

        public bool CheckIfDuplicateItem(PurchaseOrderItem item)
        {
            return false;
        }

        private bool Validate(PurchaseOrder po)
        {
            return false;
        }

        private bool Validate(PurchaseOrderItem item)
        {
            return false;
        }
    }
}
