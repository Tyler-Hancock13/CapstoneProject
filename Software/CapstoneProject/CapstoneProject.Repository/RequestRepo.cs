using CapstoneProject.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapstoneProject.DAL;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.Repository
{
    public class RequestRepo
    {
        private DataAccess db = new DataAccess();

        /// <summary>
        /// Inserts a Purchase Order Item
        /// </summary>
        /// <param name="item">Item to be inserted.</param>
        /// <returns>The ID of the Item that was inserted.</returns>
        public int InsertItem(PurchaseOrderItem item)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@Timestamp", item.Timestamp, SqlDbType.Timestamp, ParameterDirection.Output),
                new ParmStruct("@Id", item.Id, SqlDbType.Int, ParameterDirection.Output),
                new ParmStruct("@Name", item.Name, SqlDbType.VarChar, ParameterDirection.Input, 0),
                new ParmStruct("@Description", item.Description, SqlDbType.VarChar, ParameterDirection.Input, 50),
                new ParmStruct("@Quantity", item.Quantity, SqlDbType.Int, ParameterDirection.Input, 0),
                new ParmStruct("@Price", item.Price, SqlDbType.Decimal, ParameterDirection.Input, 0),
                new ParmStruct("@Justification", item.Justification, SqlDbType.VarChar, ParameterDirection.Input, 50),
                new ParmStruct("@Location", item.Location, SqlDbType.VarChar, ParameterDirection.Input, 15),
                new ParmStruct("@Status", item.Status, SqlDbType.Int, ParameterDirection.Input),
                new ParmStruct("@Subtotal", item.Subtotal, SqlDbType.Money, ParameterDirection.Input),
                new ParmStruct("@PONumber", item.PONumber, SqlDbType.Int, ParameterDirection.Input, 0)
            };

            DataAccess db = new DataAccess();

            if(db.ExecuteNonQuery("PurchaseOrderItem_Insert", parms, CommandType.StoredProcedure) > 0)
            {
                item.Timestamp = (byte[])parms[0].Value;
                item.Id = Convert.ToInt32(parms[1].Value);
                return item.Id;
            }

            return 0;
        }

        /// <summary>
        /// Inserts a Purchase Order and Purchase Order Item
        /// </summary>
        /// <param name="order">The Purchase Order to be inserted.</param>
        /// <param name="item">The Purchase Order Item to be inserted.</param>
        /// <returns>The ID of the Purchase Order that was inserted.</returns>
        public int InsertRequestAndItem(PurchaseOrder order, PurchaseOrderItem item)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@Timestamp", item.Timestamp, SqlDbType.Timestamp, ParameterDirection.Output),
                new ParmStruct("@ItemId", item.Id, SqlDbType.Int, ParameterDirection.Output),
                new ParmStruct("@Id", order.PONumber, SqlDbType.Int, ParameterDirection.Output),
                new ParmStruct("@ItemsSubtotal", order.Subtotal, SqlDbType.Money, ParameterDirection.Input),
                new ParmStruct("@SalesTax", order.SalesTax, SqlDbType.Money, ParameterDirection.Input),
                new ParmStruct("@Total", order.Total, SqlDbType.Money, ParameterDirection.Input),
                new ParmStruct("@DateCreated", order.DateCreated, SqlDbType.DateTime, ParameterDirection.Input),
                new ParmStruct("@EmployeeId", order.EmployeeId, SqlDbType.VarChar, ParameterDirection.Input, 8),
                new ParmStruct("@DepartmentsId", order.DepartmentId, SqlDbType.Int, ParameterDirection.Input, 0),
                new ParmStruct("@Status", order.Status, SqlDbType.Int, ParameterDirection.Input),
                new ParmStruct("@Name", item.Name, SqlDbType.VarChar, ParameterDirection.Input, 0),
                new ParmStruct("@Description", item.Description, SqlDbType.VarChar, ParameterDirection.Input, 50),
                new ParmStruct("@Quantity", item.Quantity, SqlDbType.Int, ParameterDirection.Input, 0),
                new ParmStruct("@Price", item.Price, SqlDbType.Decimal, ParameterDirection.Input, 0),
                new ParmStruct("@Justification", item.Justification, SqlDbType.VarChar, ParameterDirection.Input, 50),
                new ParmStruct("@Location", item.Location, SqlDbType.VarChar, ParameterDirection.Input, 15),
                new ParmStruct("@Subtotal", item.Subtotal, SqlDbType.Money, ParameterDirection.Input),
                new ParmStruct("@ItemStatus", item.Status, SqlDbType.Int, ParameterDirection.Input)
            };

            DataAccess db = new DataAccess();

            if (db.ExecuteNonQuery("PurchaseOrder_InsertItemAndRequest", parms, CommandType.StoredProcedure) > 0)
            {
                item.Timestamp = (byte[])parms[0].Value;
                order.PONumber = Convert.ToInt32(parms[2].Value);
                return order.PONumber;
            }

            return 0;
        }

        /// <summary>
        /// Retrieves a Purchase Order by an Employee's ID
        /// </summary>
        /// <param name="id">ID of the Employee to retrieve the Purchase Order.</param>
        /// <returns>The selected Purchase Order</returns>
        public PurchaseOrder GetPurchaseOrder(int id)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@EmployeeId", id, SqlDbType.Int, ParameterDirection.Input, 0)
            };

            DataTable dt = db.Execute("PurchaseOrder_Get", CommandType.StoredProcedure, parms);

            return PopulatePurchaseOrderRecord(dt.Rows[0]);
        }

        /// <summary>
        /// Retrieves a Purchase Order by it's Purchase Order ID
        /// </summary>
        /// <param name="id">ID of the Purchase Order to retrieve.</param>
        /// <returns>The Purchase Order that was retrieved.</returns>
        public PurchaseOrder GetPurchaseOrderByPOID(int id)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@PONumber", id, SqlDbType.Int, ParameterDirection.Input, 0)
            };

            DataTable dt = db.Execute("PurchaseOrder_GetByPOID", CommandType.StoredProcedure, parms);

            return PopulatePurchaseOrderRecord(dt.Rows[0]);
        }

        /// <summary>
        /// Retrieves the Purchase Orders for a specific Department
        /// </summary>
        /// <param name="id">ID of the Department to retrieve Purchase Orders for.</param>
        /// <returns>A List of the Departments Purchase Orders</returns>
        public List<PurchaseOrder> GetByDepartment(int id)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@DepartmentId", id, SqlDbType.Int, ParameterDirection.Input, 0)
            };

            DataTable dt = new DataAccess().Execute("PurchaseOrder_GetPOBySupervisorAndDepartment", CommandType.StoredProcedure, parms);

            List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();

            foreach (DataRow row in dt.Rows)
            {
                purchaseOrders.Add(new PurchaseOrder
                {
                    PONumber = Convert.ToInt32(row["PONumber"]),
                    EmployeeName = row["EmployeeName"].ToString(),
                    EmployeeId = row["EmployeeId"].ToString(),
                    DepartmentId = Convert.ToInt32(row["DepartmentsId"]),
                    DepartmentName = row["Name"].ToString(),
                    Subtotal = Convert.ToDecimal(row["ItemsSubtotal"]),
                    SalesTax = Convert.ToDecimal(row["SalesTax"]),
                    Total = Convert.ToDecimal(row["Total"]),
                    Status = (RequestStatus)row["Status"],
                    DateCreated = Convert.ToDateTime(row["DateCreated"])
                });
            }

            return purchaseOrders;
        }

        /// <summary>
        /// Updates a Purchase Order Item
        /// </summary>
        /// <param name="item">Item to be updated.</param>
        /// <returns>The Updated Item.</returns>
        public PurchaseOrderItem UpdateItem(PurchaseOrderItem item)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@Timestamp", item.Timestamp, SqlDbType.Timestamp, ParameterDirection.InputOutput),
                new ParmStruct("@ItemId", item.Id, SqlDbType.Int, ParameterDirection.InputOutput),
                new ParmStruct("@Name", item.Name, SqlDbType.VarChar, ParameterDirection.Input, 20),
                new ParmStruct("@Description", item.Description, SqlDbType.VarChar, ParameterDirection.Input, 50),
                new ParmStruct("@Quantity", item.Quantity, SqlDbType.Int, ParameterDirection.Input, 0),
                new ParmStruct("@Price", item.Price, SqlDbType.Money, ParameterDirection.Input, 0),
                new ParmStruct("@Justification", item.Justification, SqlDbType.VarChar, ParameterDirection.Input, 0),
                new ParmStruct("@Location", item.Location, SqlDbType.VarChar, ParameterDirection.Input, 20),
                new ParmStruct("@Subtotal", item.Subtotal, SqlDbType.Money, ParameterDirection.Input),
                new ParmStruct("@PONumber", item.PONumber, SqlDbType.Int, ParameterDirection.Input),
                new ParmStruct("@Status", item.Status, SqlDbType.Int, ParameterDirection.Input)
            };

            if(db.ExecuteNonQuery("PurchaseOrderItem_Update", parms, CommandType.StoredProcedure) > 0)
            {
                item.Timestamp = (byte[])parms[0].Value;
                item.Id = Convert.ToInt32(parms[1].Value);
                return item;
            }

            return null;
        }

        public PurchaseOrderItem ProcessItem(PurchaseOrderItem item)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@Timestamp", item.Timestamp, SqlDbType.Timestamp, ParameterDirection.InputOutput),
                new ParmStruct("@Id", item.Id, SqlDbType.Int, ParameterDirection.InputOutput),
                new ParmStruct("@Location", item.Location, SqlDbType.VarChar, ParameterDirection.Input, 20),
                new ParmStruct("@Quantity", item.Quantity, SqlDbType.Int, ParameterDirection.Input),
                new ParmStruct("@Price", item.Price, SqlDbType.Money, ParameterDirection.Input),
                new ParmStruct("@Subtotal", item.Subtotal, SqlDbType.Money, ParameterDirection.Input),

                item.Reason == "" || item.Reason == null
                    ? new ParmStruct("@Reason", DBNull.Value, SqlDbType.VarChar, ParameterDirection.Input, 20)
                    : new ParmStruct("@Reason", item.Reason, SqlDbType.VarChar, ParameterDirection.Input, 20),

                new ParmStruct("@Status", item.Status, SqlDbType.Int, ParameterDirection.Input)
            };

            if(db.ExecuteNonQuery("PurchaseOrderItem_Process", parms, CommandType.StoredProcedure) > 0)
            {
                item.Timestamp = (byte[])parms[0].Value;
                item.Id = Convert.ToInt32(parms[1].Value);

                return item;
            }

            return null;
        }

        /// <summary>
        /// Updates a Purchase Order
        /// </summary>
        /// <param name="order">Purchase Order to be updated.</param>
        /// <returns>The Updated Purchase Order.</returns>
        public PurchaseOrder UpdateOrder(PurchaseOrder order)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@PONumber", order.PONumber, SqlDbType.Int, ParameterDirection.InputOutput),
                new ParmStruct("@ItemsSubtotal", order.Subtotal, SqlDbType.Money, ParameterDirection.Input),
                new ParmStruct("@SalesTax", order.SalesTax, SqlDbType.Money, ParameterDirection.Input),
                new ParmStruct("@Total", order.Total, SqlDbType.Money, ParameterDirection.Input),
                new ParmStruct("Status", order.Status, SqlDbType.Int, ParameterDirection.Input)
            };

            if (db.ExecuteNonQuery("PurchaseOrder_Update", parms, CommandType.StoredProcedure) > 0)
            {
                order.PONumber = Convert.ToInt32(parms[0].Value);
            }

            return order;
        }

        /// <summary>
        /// Retrieves Purchase Orders by an Employee's ID
        /// </summary>
        /// <param name="id">ID of the Employee's Purchase Orders to retrieve.</param>
        /// <returns>A list of the Employee's Purchase Orders.</returns>
        public List<PurchaseOrder> SelectById(string id)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@EmployeeId", id, SqlDbType.VarChar, ParameterDirection.Input, 8)
            };

            DataTable dt = new DataAccess().Execute("PurchaseOrder_SelectById", CommandType.StoredProcedure, parms);

            List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();

            foreach(DataRow row in dt.Rows)
            {
                purchaseOrders.Add(new PurchaseOrder
                {
                    PONumber = Convert.ToInt32(row["PONumber"]),
                    EmployeeName = row["EmployeeName"].ToString(),
                    EmployeeId = row["EmployeeId"].ToString(),
                    DepartmentId = Convert.ToInt32(row["DepartmentsId"]),
                    DepartmentName = row["Name"].ToString(),
                    Subtotal = Convert.ToDecimal(row["ItemsSubtotal"]),
                    SalesTax = Convert.ToDecimal(row["SalesTax"]),
                    Total = Convert.ToDecimal(row["Total"]),
                    Status = (RequestStatus)row["Status"],
                    DateCreated = Convert.ToDateTime(row["DateCreated"])
                });

                
            }

            return purchaseOrders;
        }

        /// <summary>
        /// Retrieves Purchase Order Items by their Purchase Order Number
        /// </summary>
        /// <param name="id">Purchase Order Number to retrieve the Items</param>
        /// <returns>A list of PurchaseOrderItems for the PurchaseOrder</returns>
        public List<PurchaseOrderItem> GetItemsByPOId(int id)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@PONumber", id, SqlDbType.Int, ParameterDirection.Input, 0)
            };

            DataTable dt = new DataAccess().Execute("PurchaseOrderItem_SelectItemByPOId", CommandType.StoredProcedure, parms);

            List<PurchaseOrderItem> orderItems = new List<PurchaseOrderItem>();

            foreach(DataRow row in dt.Rows)
            {
                orderItems.Add(new PurchaseOrderItem
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    Description = row["Description"].ToString(),
                    Quantity = Convert.ToInt32(row["Quantity"]),
                    Price = Convert.ToDecimal(row["Price"]),
                    Justification = row["Justification"].ToString(),
                    Location = row["Location"].ToString(),
                    Status = (ItemStatus)row["Status"],
                    Subtotal = Convert.ToDecimal(row["Subtotal"]),
                    PONumber = Convert.ToInt32(row["PONumber"]),
                    Reason = row["Reason"].ToString(),
                    Timestamp = (byte[])(row["Timestamp"])
                });

                
            }

            return orderItems;
        }

        /// <summary>
        /// Retrieves All PurchaseOrderItems
        /// </summary>
        /// <returns>A list of all PurchaseOrderItems</returns>
        public List<PurchaseOrderItem> GetAllItems()
        {
            DataTable dt = db.Execute("PurchaseOrderItem_GetAll");

            List<PurchaseOrderItem> items = new List<PurchaseOrderItem>();

            foreach(DataRow row in dt.Rows)
            {
                items.Add(PopulateItemRecord(row));
            }

            return items;
        }

        //public int CheckIfDuplicateItem(PurchaseOrderItem item)
        //{

        //}

        /// <summary>
        /// Retrieves a PurchaseOrderItem by it's ID
        /// </summary>
        /// <param name="id">ID of the PurchaseOrderItem to retrieve.</param>
        /// <returns>The PurchaseOrderItem that matches the ID</returns>
        public PurchaseOrderItem GetItemById(int id)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@Id", id, SqlDbType.Int, ParameterDirection.Input, 0)
            };

            DataTable dt = db.Execute("PurchaseOrderItem_GetById", CommandType.StoredProcedure, parms);

            return PopulateItemRecord(dt.Rows[0]);
        }

        /// <summary>
        /// Retrieves PurchaseOrders by their ID
        /// </summary>
        /// <param name="id">ID of the PurchaseOrders to retrieve.</param>
        /// <returns>A list of PurchaseOrders that match the ID</returns>
        public List<PurchaseOrder> SearchById(int id, string userId)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@PONumber", id, SqlDbType.Int, ParameterDirection.Input, 0),
                new ParmStruct("@EmployeeId", userId, SqlDbType.VarChar, ParameterDirection.Input, 8)
            };

            DataTable dt = new DataAccess().Execute("PurchaseOrder_SearchByPOID", CommandType.StoredProcedure, parms);

            List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();

            foreach(DataRow row in dt.Rows)
            {
                purchaseOrders.Add(new PurchaseOrder
                {
                    PONumber = Convert.ToInt32(row["PONumber"]),
                    EmployeeId = row["EmployeeId"].ToString(),
                    EmployeeName = row["EmployeeName"].ToString(),
                    DepartmentId = Convert.ToInt32(row["DepartmentsId"]),
                    DepartmentName = row["Name"].ToString(),
                    Subtotal = Convert.ToDecimal(row["ItemsSubtotal"]),
                    SalesTax = Convert.ToDecimal(row["SalesTax"]),
                    Total = Convert.ToDecimal(row["Total"]),
                    Status = (RequestStatus)row["Status"],
                    DateCreated = Convert.ToDateTime(row["DateCreated"])

                });
            }

            return purchaseOrders;
        }

        /// <summary>
        /// Retrieves PurchaseOrders between two dates
        /// </summary>
        /// <param name="startDate">The Start Date to search by.</param>
        /// <param name="endDate">The End Date to search by.</param>
        /// <returns>A list of PurchaseOrders between the two dates.</returns>
        public List<PurchaseOrder> SearchByDate(DateTime startDate, DateTime endDate, string userId)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@StartDate", startDate, SqlDbType.DateTime, ParameterDirection.Input),
                new ParmStruct("@EndDate", endDate, SqlDbType.DateTime, ParameterDirection.Input),
                new ParmStruct("@EmployeeId", userId, SqlDbType.VarChar, ParameterDirection.Input, 8)
            };

            DataTable dt = db.Execute("PurchaseOrder_SearchByDate", CommandType.StoredProcedure, parms);

            List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();

            foreach(DataRow row in dt.Rows)
            {
                purchaseOrders.Add(new PurchaseOrder
                {
                    PONumber = Convert.ToInt32(row["PONumber"]),
                    EmployeeId = row["EmployeeId"].ToString(),
                    EmployeeName = row["EmployeeName"].ToString(),
                    DepartmentId = Convert.ToInt32(row["DepartmentsId"]),
                    DepartmentName = row["Name"].ToString(),
                    Subtotal = Convert.ToDecimal(row["ItemsSubtotal"]),
                    SalesTax = Convert.ToDecimal(row["SalesTax"]),
                    Total = Convert.ToDecimal(row["Total"]),
                    Status = (RequestStatus)row["Status"],
                    DateCreated = Convert.ToDateTime(row["DateCreated"])
                }); 
            }

            return purchaseOrders;
        }

        /// <summary>
        /// Deletes an Item
        /// </summary>
        /// <param name="id">ID of the PurchaseOrderItem to delete.</param>
        /// <returns>Whether the deletion was successful./returns>
        public bool Delete(int id)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@Id", id, SqlDbType.Int, ParameterDirection.Input, 0)
            };

            return db.ExecuteNonQuery("PurchaseOrderItem_Delete", parms, CommandType.StoredProcedure) > 0;
        }

        /// <summary>
        /// Parses a DataRow and populates a PurchaseOrderItem model.
        /// </summary>
        /// <param name="row">The DataRow to be parsed.</param>
        /// <returns>The populated PurchaseOrderItem model.</returns>
        private PurchaseOrderItem PopulateItemRecord(DataRow row)
        {
            PurchaseOrderItem item = new PurchaseOrderItem();

            item.Id = Convert.ToInt32(row["Id"]);
            item.Name = row["Name"].ToString();
            item.Description = row["Description"].ToString();
            item.Quantity = Convert.ToInt32(row["Quantity"]);
            item.Price = Convert.ToDecimal(row["Price"]);
            item.Justification = row["Justification"].ToString();
            item.Location = row["Location"].ToString();
            item.Subtotal = Convert.ToDecimal(row["Subtotal"]);
            item.Status = (ItemStatus)row["Status"];
            item.PONumber = Convert.ToInt32(row["PONumber"]);
            item.Timestamp = (byte[])(row["Timestamp"]);

            return item;
        }

        /// <summary>
        /// Parses a DataRow and populates a PurchaseOrder model.
        /// </summary>
        /// <param name="row">The DataRow to be parsed.</param>
        /// <returns>The populated PurchaseOrder model.</returns>
        private PurchaseOrder PopulatePurchaseOrderRecord(DataRow row)
        {
            PurchaseOrder order = new PurchaseOrder();

            order.PONumber = Convert.ToInt32(row["PONumber"]);
            order.EmployeeId = row["EmployeeId"].ToString();
            order.DepartmentId = Convert.ToInt32(row["DepartmentsId"]);
            order.Subtotal = Convert.ToDecimal(row["ItemsSubtotal"]);
            order.SalesTax = Convert.ToDecimal(row["SalesTax"]);
            order.Total = Convert.ToDecimal(row["Total"]);
            order.Status = (RequestStatus)row["Status"];
            order.Items = new List<PurchaseOrderItem>();

            Math.Round(order.Subtotal, 2);

            return order;
        }
    }
}
