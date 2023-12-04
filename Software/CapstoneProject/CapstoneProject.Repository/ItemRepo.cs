using CapstoneProject.DAL;
using CapstoneProject.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.Repository
{
    public class ItemRepo
    {
        public List<PurchaseOrderItem> GetItemsById(int id)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@PONumber", id, SqlDbType.Int, ParameterDirection.Input, 0)
            };

            DataTable dt = new DataAccess().Execute("PurchaseOrders_SelectItemById", CommandType.StoredProcedure, parms);

            List<PurchaseOrderItem> orderItems = new List<PurchaseOrderItem>();

            foreach (DataRow row in dt.Rows)
            {
                orderItems.Add(PopulateItemRecord(row));
            }

            return orderItems;
        }

        public bool UpdateItem(PurchaseOrderItem item)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@ItemId", item.Id, SqlDbType.Int, ParameterDirection.Input, 0),
                new ParmStruct("@Name", item.Name, SqlDbType.VarChar, ParameterDirection.Input, 0),
                new ParmStruct("@Description", item.Description, SqlDbType.VarChar, ParameterDirection.Input, 50),
                new ParmStruct("@Quantity", item.Quantity, SqlDbType.Int, ParameterDirection.Input, 0),
                new ParmStruct("@Price", item.Price, SqlDbType.Decimal, ParameterDirection.Input, 0),
                new ParmStruct("@Justification", item.Justification, SqlDbType.VarChar, ParameterDirection.Input, 50),
                new ParmStruct("@Location", item.Location, SqlDbType.VarChar, ParameterDirection.Input, 15),
                new ParmStruct("@Status", item.Status, SqlDbType.Int, ParameterDirection.Input),
                new ParmStruct("@Subtotal", item.Subtotal, SqlDbType.Money, ParameterDirection.Input),
                new ParmStruct("@PONumber", item.PONumber, SqlDbType.Int, ParameterDirection.Input),
                new ParmStruct("@ID", item.Id, SqlDbType.Int, ParameterDirection.Output, 0)
            };

            DataAccess db = new DataAccess();

            return db.ExecuteNonQuery("PurchaseOrder_UpdateItem", parms, CommandType.StoredProcedure) > 0;
        }

        public bool InsertItem(PurchaseOrderItem item)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@Name", item.Name, SqlDbType.VarChar, ParameterDirection.Input, 0),
                new ParmStruct("@Description", item.Description, SqlDbType.VarChar, ParameterDirection.Input, 50),
                new ParmStruct("@Quantity", item.Quantity, SqlDbType.Int, ParameterDirection.Input, 0),
                new ParmStruct("@Price", item.Price, SqlDbType.Decimal, ParameterDirection.Input, 0),
                new ParmStruct("@Justification", item.Justification, SqlDbType.VarChar, ParameterDirection.Input, 50),
                new ParmStruct("@Location", item.Location, SqlDbType.VarChar, ParameterDirection.Input, 15),
                new ParmStruct("@Status", item.Status, SqlDbType.Int, ParameterDirection.Input),
                new ParmStruct("@Subtotal", item.Subtotal, SqlDbType.Money, ParameterDirection.Input),
                new ParmStruct("@PONumber", item.PONumber, SqlDbType.Int, ParameterDirection.Input),
                new ParmStruct("@ID", item.Id, SqlDbType.Int, ParameterDirection.Output, 0)
            };

            DataAccess db = new DataAccess();

            return db.ExecuteNonQuery("PurchaseOrders_InsertItem", parms, CommandType.StoredProcedure) > 0;
        }

        public bool DeleteItem(int id)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@Id", id, SqlDbType.Int, ParameterDirection.Input, 0)
            };

            DataAccess db = new DataAccess();

            int retVal = db.ExecuteNonQuery("PurchaseOrder_DeleteItem", parms, CommandType.StoredProcedure);

            if (retVal > 0)
            {
                return true;
            }

            return false;
        }

        public PurchaseOrderItem Get(int id)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@Id", id, SqlDbType.Int, ParameterDirection.Input, 0)
            };

            DataAccess db = new DataAccess();
            DataTable dt = db.Execute("PurchaseOrder_GetItem", CommandType.StoredProcedure, parms);

            if(dt.Rows.Count == 0)
            {
                return null;
            }

            return PopulateItemRecord(dt.Rows[0]);
        }

        private PurchaseOrderItem PopulateItemRecord(DataRow row)
        {
            PurchaseOrderItem item = new PurchaseOrderItem();

            item.Id = Convert.ToInt32(row["Id"]);
            item.Name = row["Name"].ToString();
            item.Description = row["Description"].ToString();
            item.Quantity = Convert.ToInt32(row["Quantity"]);
            item.Price = Convert.ToDouble(row["Price"]);
            item.Justification = row["Justification"].ToString();
            item.Location = row["Location"].ToString();
            item.Subtotal = Convert.ToDouble(row["Subtotal"]);
            item.Status = (ItemStatus)row["Status"];
            item.PONumber = Convert.ToInt32(row["PONumber"]);

            return item;
        }
    }
}
