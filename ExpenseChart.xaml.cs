using System;
using System.Collections.Generic;
using System.Linq;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Controls;

namespace CurseWork
{
    /// <summary>
    /// Interaction logic for ExpenseChart.xaml
    /// </summary>
    public partial class ExpenseChart : Page
    {
        public ExpenseChart()
        {
            InitializeComponent();
            PointLabel = chartPoint =>
          string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;

            List<ValueTuple<string, decimal>> expense = new List<(string, decimal)>();

            LoadOrders(expense);
            LoadPieDiagram(expense);
        }


        private void LoadOrders(List<ValueTuple<string, decimal>> expense)
        {

            using (var context = new MSSQLContext())
            {
                var purchase = context.Database.SqlQuery<PurchaseIngredient>("select * from PurchaseIngredients").ToList();

                for (int index = 0; index < purchase.Count; index++)
                {
                    (string name, decimal AllIncome) tuple = (purchase[index].ingredient.Name, purchase[index].Price * purchase[index].Count);

                    for (int count = index + 1; count < purchase.Count; count++)
                    {
                        if (purchase[index].IngredientId == purchase[count].IngredientId)
                        {
                            tuple.AllIncome += purchase[count].Price* purchase[count].Count;
                        }
                    }

                    expense.Add(tuple);
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
