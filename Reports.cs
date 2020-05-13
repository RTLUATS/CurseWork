using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;

namespace CurseWork
{
    internal static class Reports
    {

        private static System.Data.DataTable ChefReport()
        {
            var table = new System.Data.DataTable();

            using(var context = new MSSQLContext()) 
            {
                var list = context.Inquiries.ToList();
               
                table.Columns.Add(new DataColumn("Название", typeof(string)));
                table.Columns.Add(new DataColumn("Количество", typeof(decimal)));
                table.Columns.Add(new DataColumn("Дата заказа", typeof(DateTime)));

                foreach(var item in list)
                {
                    DataRow newRow = table.NewRow();

                    newRow[0] = context.Ingredients.Find(item.IngredientId).Name;
                    newRow[1] = item.ExpectedQuantity;
                    newRow[2] = item.Date;


                    table.Rows.Add(newRow);
                }
            }

            return table;
        }

        private static System.Data.DataTable ManagerReportIngredient()
        {
            var table = new System.Data.DataTable();

            using (var context = new MSSQLContext())
            {
                var list = context.Ingredients.ToList();

                table.Columns.Add(new DataColumn("Название", typeof(string)));
                table.Columns.Add(new DataColumn("Количество", typeof(decimal)));
                table.Columns.Add(new DataColumn("Последняя цена за 1 ед.", typeof(decimal)));

                foreach (var item in list)
                {
                    DataRow newRow = table.NewRow();

                    newRow[0] = item.Name;
                    newRow[1] = item.Count;
                    newRow[2] = item.Price;

                    table.Rows.Add(newRow);
                }
            }

            return table;
        }

        private static System.Data.DataTable ManagerReportFood()
        {
            var table = new System.Data.DataTable();

            using (var context = new MSSQLContext())
            {
                var list = context.Foods.ToList();

                table.Columns.Add(new DataColumn("Название", typeof(string)));
                table.Columns.Add(new DataColumn("Есть в меню?", typeof(bool)));
                table.Columns.Add(new DataColumn("Текущая цена", typeof(decimal)));

                foreach (var item in list)
                {
                    DataRow newRow = table.NewRow();

                    newRow[0] = item.Name;
                    newRow[1] = item.InMenu;
                    newRow[2] = item.CurrentPrice;

                    table.Rows.Add(newRow);
                }
            }

            return table;
        }

        private static System.Data.DataTable EconomistReportIncome()
        {
            var table = new System.Data.DataTable();
            var listId = new List<(int, int)>();

            using (var context = new MSSQLContext())
            {
                var list = context.Orders
                                .Include(o => o.Food)
                                .ToList();

                table.Columns.Add(new DataColumn("Название", typeof(string)));
                table.Columns.Add(new DataColumn("Общий доход", typeof(decimal)));

                foreach (var item in list)
                {
                    if(!listId.Contains((item.FoodId, item.IdOrderList))) {
                        
                        DataRow newRow = table.NewRow();

                        newRow[0] = item.Food.Name;
                        newRow[1] = list.Where(l => l.FoodId == item.FoodId).Sum(i => i.PriceBoughtFor * i.Count);

                        listId.Add((item.FoodId, item.IdOrderList));

                        table.Rows.Add(newRow);
                    }
                }
            }

            return table;
        }

        private static System.Data.DataTable EconomistReportExpense()
        {
            var table = new System.Data.DataTable();
            var listId = new List<int>();

            using (var context = new MSSQLContext())
            {
                var list = context.PurchaseIngredients
                                .Include(o => o.Ingredient)
                                .ToList();

                table.Columns.Add(new DataColumn("Название", typeof(string)));
                table.Columns.Add(new DataColumn("Общий рассход", typeof(decimal)));

                foreach (var item in list)
                {
                    if (!listId.Contains(item.IngredientId))
                    {

                        DataRow newRow = table.NewRow();

                        newRow[0] = item.Ingredient.Name;
                        newRow[1] = list.Where(l => l.IngredientId == item.IngredientId).Sum(i => i.Price * i.Count);

                        listId.Add(item.Id);

                        table.Rows.Add(newRow);
                    }
                }
            }

            return table;
        }

        internal static void CommonPart(int index)
        {
            Dictionary<int, Func<System.Data.DataTable>> dictionary = new Dictionary<int, Func<System.Data.DataTable>>()
            {
                {0, ChefReport},
                {1, ManagerReportFood},
                {2, ManagerReportIngredient},
                {3, EconomistReportIncome},
                {4, EconomistReportIncome }
            };

            var excelApp = new Excel.Application();

            if (excelApp == null)
            {
                MessageBox.Show("Перед созданием отчёта вы должны установить Excel!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // File save dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls;*.xlsx";

            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.FileName = null;
            saveFileDialog.Title = "Save path of the file to be exported";

            if (saveFileDialog.ShowDialog() == true) {

             
                Excel.Workbooks wkBooks = excelApp.Workbooks;
                Excel.Workbook wkBook = wkBooks.Add();
                Excel.Sheets wkSheets = wkBook.Sheets;
                Excel.Worksheet wkSheet = wkSheets.Add();

                wkSheet.Name = "Отчёт";

                try
                {
                    var dtTable = dictionary[index].Invoke();

                    for (var i = 0; i < dtTable.Columns.Count; i++)
                    {
                        wkSheet.Cells[1, i + 1] = dtTable.Columns[i].ColumnName;
                    }

                    //rows
                    for (var i = 0; i < dtTable.Rows.Count; i++)
                    {
                        for (var j = 0; j < dtTable.Columns.Count; j++)
                        {
                            wkSheet.Cells[i + 2, j + 1] = dtTable.Rows[i][j];
                        }
                    }

                    wkBook.SaveAs(saveFileDialog.FileName);
                    
                    excelApp.Quit();

                    MessageBox.Show("Отчёт создан");

                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    if (wkSheets != null) Marshal.ReleaseComObject(wkSheets);
                    if (wkBook != null) Marshal.ReleaseComObject(wkBook);
                    if (wkBooks != null) Marshal.ReleaseComObject(wkBooks);
                    if (excelApp != null) Marshal.ReleaseComObject(excelApp);
                }
            }
        }
    }
}
