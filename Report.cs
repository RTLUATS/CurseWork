using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;

namespace CurseWork
{
    internal class Report
    {
        private Dictionary<int, Func<int, System.Data.DataTable>> dictionary;
        private string margCellsEnd;
        private string reportName;

        public Report()
        {
            dictionary = new Dictionary<int, Func<int,System.Data.DataTable>>()
            {
                {0, ChefReport},
                {1, ManagerReportFood},
                {2, ManagerReportIngredient},
                {3, EconomistReportIncome},
                {4, EconomistReportExpense }
            };
        }

        private System.Data.DataTable ChefReport(int a = 0)
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

            margCellsEnd = "C1";
            reportName = "Отчёт Шеф-Повара";

            return table;
        }

        private  System.Data.DataTable ManagerReportIngredient(int a = 0)
        {
            var table = new System.Data.DataTable();

            using (var context = new MSSQLContext())
            {
                var list = context.Ingredients.ToList();

                table.Columns.Add(new DataColumn("Название", typeof(string)));
                table.Columns.Add(new DataColumn("Количество", typeof(decimal)));
                table.Columns.Add(new DataColumn("Текущая цена за 1 ед.", typeof(decimal)));

                foreach (var item in list)
                {
                    DataRow newRow = table.NewRow();

                    newRow[0] = item.Name;
                    newRow[1] = item.Count;
                    newRow[2] = item.Price;

                    table.Rows.Add(newRow);
                }
            }

            margCellsEnd = "C1";
            reportName = "Отчёт Менеджера по Ингредиентам";

            return table;
        }

        private System.Data.DataTable ManagerReportFood(int a = 0)
        {
            var table = new System.Data.DataTable();

            using (var context = new MSSQLContext())
            {
                var list = context.Foods
                    .Include(f => f.Structures)
                    .ToList();

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

                reportName = "Отчёт Менеджера по Блюдам";
                margCellsEnd = "C1";
            }

            return table;
        }

        private System.Data.DataTable EconomistReportIncome(int days)
        {
            var table = new System.Data.DataTable();
            var listId = new List<int>();

            using (var context = new MSSQLContext())
            {
                var list = context.Orders
                                .Include(o => o.Food)
                                .Include(o => o.OrderList)
                                .ToList();

                table.Columns.Add(new DataColumn("Название", typeof(string)));
                table.Columns.Add(new DataColumn("Общий доход", typeof(decimal)));
                table.Columns.Add(new DataColumn("Проданное количество", typeof(decimal)));

                foreach (var item in list)
                {
                    if(!listId.Contains(item.FoodId)) {
                        
                        DataRow newRow = table.NewRow();

                        newRow[0] = item.Food.Name;

                        if (days == 0)
                        {
                            newRow[1] = list.Where(l => l.FoodId == item.FoodId).Sum(i => i.PriceBoughtFor * i.Count);
                            newRow[2] = list.Where(l => l.FoodId == item.FoodId).Sum(i => i.Count);
                        }
                        else
                        {
                            newRow[1] = list.Where(l => l.FoodId == item.FoodId &&
                                                  DateTime.Now.Subtract(l.OrderList.DateOrder.Date).TotalDays <= days)
                                                   .Sum(i => i.PriceBoughtFor * i.Count);
                            newRow[2] = list.Where(l => l.FoodId == item.FoodId &&
                                                   DateTime.Now.Subtract(l.OrderList.DateOrder.Date).TotalDays <= days)
                                                   .Sum(i => i.Count);

                        }
                        listId.Add(item.FoodId);

                        table.Rows.Add(newRow);
                    }
                }
                
                margCellsEnd = "C1";
                reportName = "Отчёт Экономита по продажам за" + (days == 0 ? "всё время" : $"последние {days} дней"); 
            }

            return table;
        }

        private System.Data.DataTable EconomistReportExpense(int days)
        {
            var table = new System.Data.DataTable();
            var listId = new List<int>();

            using (var context = new MSSQLContext())
            {
                var list = context.PurchaseIngredients
                                .Include(o => o.Ingredient)
                                .ToList();

                table.Columns.Add(new DataColumn("Название", typeof(string)));
                table.Columns.Add(new DataColumn("Общий расход", typeof(decimal)));
                table.Columns.Add(new DataColumn("Приобретённое количество", typeof(decimal)));

                foreach (var item in list)
                {
                    if (!listId.Contains(item.IngredientId))
                    {

                        DataRow newRow = table.NewRow();

                        newRow[0] = item.Ingredient.Name;

                        if (days == 0)
                        {
                            newRow[1] = list.Where(l => l.IngredientId == item.IngredientId).Sum(i => i.Price * i.Count);
                            newRow[2] = list.Where(l => l.IngredientId == item.IngredientId).Sum(i => i.Count);
                        }
                        else
                        {
                            newRow[1] = list.Where(l => l.IngredientId == item.IngredientId &&
                                                    DateTime.Now.Subtract(item.DateOfPurchase.Date).Days <= days)
                                                            .Sum(i => i.Price * i.Count);
                            newRow[2] = list.Where(l => l.IngredientId == item.IngredientId &&
                                                    DateTime.Now.Subtract(item.DateOfPurchase.Date).Days <= days)
                                                            .Sum(i => i.Count);
                        }

                        listId.Add(item.Id);

                        table.Rows.Add(newRow);
                    }
                }
                margCellsEnd = "C1";
                reportName = "Отчёт Экономита по расходам за " + (days == 0 ? "всё время" : $"последние {days} дней"); ;
            }

            return table;
        }

        internal void CommonPart(int index, int days = 0)
        {
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
             
                Workbooks wkBooks = excelApp.Workbooks;
                Workbook wkBook = wkBooks.Add();
                Sheets wkSheets = wkBook.Sheets;
                Worksheet wkSheet = wkSheets.Add();
                wkSheet.Name = "Отчёт";

                try
                {
                    var dtTable = dictionary[index].Invoke(days);

                    Range _excelCells1 = (Excel.Range)wkSheet.get_Range("A1", margCellsEnd).Cells;
                    Range excelRange = wkSheet.UsedRange;
                    // Производим объединение
                    _excelCells1.Merge();
                    wkSheet.Cells[1, 1] = reportName;


                    for (var i = 0; i < dtTable.Columns.Count; i++)
                    {
                        wkSheet.Cells[2, i + 1] = dtTable.Columns[i].ColumnName;
                        wkSheet.Cells[2, i + 1].Font.Bold = true;
  

                        Range cell = excelRange.Cells[2, i + 1];
                        Borders bd = cell.Borders;
                        bd.LineStyle = XlLineStyle.xlContinuous;
                        bd.Weight = 2;
                     
                    }

                    //rows
                    for (var i = 0; i < dtTable.Rows.Count; i++)
                    {
                        for (var j = 0; j < dtTable.Columns.Count; j++)
                        {
                            wkSheet.Cells[i + 3, j + 1] = dtTable.Rows[i][j];

                            Range cell = excelRange.Cells[i+3, j + 1];
                            Borders bd = cell.Borders;
                            bd.LineStyle = XlLineStyle.xlContinuous;
                            bd.Weight = 2;
                           
                        }
                    }


                    wkSheet.StandardWidth = 30;
                    wkSheet.Columns.ColumnWidth = 30;
                   
                    wkSheet.Columns.HorizontalAlignment = XlVAlign.xlVAlignCenter;
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
