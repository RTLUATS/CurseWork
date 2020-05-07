using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace CurseWork
{
    /// <summary>
    /// Interaction logic for DiagramIncome.xaml
    /// </summary>
    public partial class DiagramIncome : Page
    {
        public DiagramIncome()
        {
            InitializeComponent();
     
            PointLabel = chartPoint =>
                  string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;

            List<ValueTuple<string, decimal>> income = new List<(string, decimal)>();

            LoadOrders(income);
            LoadPieDiagram(income);
        }


        private void LoadOrders(List<ValueTuple<string, decimal>> income)
        {

            using(var context = new MSSQLContext())
            {
                var orders = context.Database.SqlQuery<Order>("select * from Orders").ToList();

                for (int index = 0; index < orders.Count; index++)
                {
                    (string name, decimal AllIncome) tuple = (orders[index].Food.Name, orders[index].PriceBoughtFor);

                    for (int count = index + 1; count < orders.Count; count++)
                    {
                        if (orders[index].FoodId == orders[count].FoodId)
                        {
                            tuple.AllIncome += orders[count].PriceBoughtFor;
                        }
                    }

                    income.Add(tuple);
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
