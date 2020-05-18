using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace CurseWork
{
    
    public partial class DiagramIncome : Page
    {
        public DiagramIncome(int days)
        {
            InitializeComponent();
     
            PointLabel = chartPoint =>
                  string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;

            List<ValueTuple<string, decimal>> income = new List<(string, decimal)>();

            LoadOrders(income, days);
            LoadPieDiagram(income);
        }

        private void LoadOrders(List<ValueTuple<string, decimal>> income, int days)
        {
            var listId = new List<int>();

            using(var context = new MSSQLContext())
            {
                var orders = context.Orders
                                        .Include(o => o.Food)
                                        .Include(o => o.OrderList)
                                        .ToList();

                foreach(var item in orders)
                {
                    if (!listId.Contains(item.FoodId)) {

                        listId.Add(item.FoodId);

                        if (days == 0)
                            income.Add((item.Food.Name, orders.Where(o => o.FoodId == item.FoodId)
                                                                .Sum(o => o.PriceBoughtFor * o.Count)));
                        else
                            income.Add((item.Food.Name, orders.Where(o => o.FoodId == item.FoodId
                                                                     && DateTime.Now
                                                                     .Subtract(o.OrderList.DateOrder.Date).TotalDays <= days)
                                                                .Sum(o => o.PriceBoughtFor * o.Count)));
                    }
                }
            }
        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        private void LoadPieDiagram(List<ValueTuple<string, decimal>> income)
        {
            SeriesCollection series = new SeriesCollection();

            foreach (var obj in income)
                series.Add(new PieSeries() { Title = obj.Item1, Values = new ChartValues<decimal> { obj.Item2 }, DataLabels = true, LabelPoint = PointLabel });
            
            PieChart.Series = series;
        }

    }
}
