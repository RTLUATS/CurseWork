using System;
using System.Collections.Generic;
using System.Linq;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Controls;
using System.Data.Entity;

namespace CurseWork
{
   
    public partial class ExpenseChart : Page
    {
        public ExpenseChart(int days)
        {
            InitializeComponent();
            PointLabel = chartPoint =>
          string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;

            List<ValueTuple<string, decimal>> expense = new List<(string, decimal)>();

            LoadOrders(expense, days);
            LoadPieDiagram(expense);
        }


        private void LoadOrders(List<ValueTuple<string, decimal>> expense, int days)
        {
            List<int> listId = new List<int>();

            using (var context = new MSSQLContext())
            {
                var purchaseList = context.PurchaseIngredients
                                        .Include(p => p.Ingredient)    
                                        .ToList();

                foreach(var item in purchaseList)
                {
                    if (!listId.Contains(item.IngredientId))
                    {
                        listId.Add(item.IngredientId);

                        if(days == 0)
                            expense.Add((item.Ingredient.Name, 
                                purchaseList.Where(l => l.IngredientId == item.IngredientId)
                                .Sum(l => l.Price * l.Count)));
                        else
                            expense.Add((item.Ingredient.Name,
                               purchaseList.Where(l => l.IngredientId == item.IngredientId &&
                                               DateTime.Now.Subtract(l.DateOfPurchase.Date).Days <= days)
                                                .Sum(l => l.Price * l.Count)));
                    }
                }
            }
        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        private void LoadPieDiagram(List<ValueTuple<string, decimal>> expense)
        {
            SeriesCollection series = new SeriesCollection();

            foreach (var obj in expense)
                series.Add(new PieSeries() { Title = obj.Item1, Values = new ChartValues<decimal> { obj.Item2 }, DataLabels = true, LabelPoint = PointLabel });

            PieChart.Series = series;
        }
    }
}
