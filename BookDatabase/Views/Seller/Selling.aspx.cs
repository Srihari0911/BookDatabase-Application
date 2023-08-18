using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookDatabase.Views.Seller
{
    public partial class Selling : System.Web.UI.Page
    {
        Models.Functions Con;
        int Seller = Login.User;
        string SName = Login.UName;
        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            if (!IsPostBack)
            {
                ShowBooks();
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[5]
                {
                    new DataColumn("ID"),
                    new DataColumn("Book"),
                    new DataColumn("Price"),
                    new DataColumn("Quantity"),
                    new DataColumn("Total")
                }
                );
                ViewState["Bill"] = dt;
                this.BindGrid();
            }
        }
        protected void BindGrid()
        {
            BillList.DataSource = ViewState["Bill"];
            BillList.DataBind();
        }
        private void ShowBooks()
        {
            string Query = "Select BId as Code,BName as Name,BQty as Stock,Bprice as Price from BookTbl";
            BooksList.DataSource = Con.GetData(Query);
            BooksList.DataBind();

        }
        int Key = 0;
        int Stock = 0;
        protected void AuthorsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BNameTb.Value = BooksList.SelectedRow.Cells[2].Text;
            Stock= Convert.ToInt32(BooksList.SelectedRow.Cells[3].Text);
            BPriceTb.Value = BooksList.SelectedRow.Cells[4].Text;
          
            if (BNameTb.Value == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(BooksList.SelectedRow.Cells[1].Text);
            }
        }
        private void UpdateStock()
        {
            int NewQty;
            NewQty = Convert.ToInt32(BooksList.SelectedRow.Cells[3].Text) - Convert.ToInt32(BQtyTb.Value);
     
            string Query = "update BookTbl set BQty = {0} where BId = {1}";
            Query = string.Format(Query, NewQty, BooksList.SelectedRow.Cells[1].Text);
            Con.SetData(Query);
            ShowBooks();

        }
        private void InsertBill()
        {
            string Query = "insert into BillTbl values('{0}',{1},{2})";
            Query = string.Format(Query, System.DateTime.Today.Date.ToString(), Seller, Convert.ToInt32(GrdTotalTb.Text.Substring(2)));
            Con.SetData(Query);
            
        }
        int Grdtotal = 0;
        int Amount;
        

        protected void AddToBillBtn_Click(object sender, EventArgs e)
        {
            if(BQtyTb.Value == "" || BPriceTb.Value == "" || BNameTb.Value == "")
            {

            }else
            {
                int total = Convert.ToInt32(BQtyTb.Value) * Convert.ToInt32(BPriceTb.Value);
                DataTable dt = (DataTable)ViewState["Bill"];
                dt.Rows.Add(BillList.Rows.Count + 1,
                    BNameTb.Value.Trim(),
                    BPriceTb.Value.Trim(),
                    BQtyTb.Value.Trim(),
                    total);

                ViewState["Bill"] = dt;
                this.BindGrid();
                UpdateStock();
               // Grdtotal = 0;
                for (int i = 0; i < BillList.Rows.Count - 1; i++)
                {
                    Grdtotal = Grdtotal + Convert.ToInt32(BillList.Rows[i].Cells[5].Text);
                }
                Amount = Grdtotal;
                GrdTotalTb.Text = "Rs" + Grdtotal;
                BNameTb.Value = "";
                BPriceTb.Value = "";
                BQtyTb.Value = "";
               
            }
            
        }

        protected void PrintBtn_Click(object sender, EventArgs e)
        {
            InsertBill();
        }
    }
}
