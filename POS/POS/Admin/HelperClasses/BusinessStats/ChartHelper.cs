using System;
using System.Collections.Generic;
using System.Linq;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WinForms;
using SkiaSharp;

namespace POS.Admin
{
    public static class ChartHelper
    {
        public static void LoadRevenueChart(CartesianChart chart, KpiData kpi, bool isLineChart)
        {
            if (kpi.DailyLabels.Count == 0)
            {
                chart.Series = Array.Empty<ISeries>();
                chart.XAxes = new Axis[] { new Axis { Labels = new[] { "No data" } } };
                return;
            }

            chart.XAxes = new Axis[] { new Axis { Labels = kpi.DailyLabels, LabelsPaint = new SolidColorPaint(SKColors.Gray), TextSize = 11, LabelsRotation = -45 } };
            chart.YAxes = new Axis[] { new Axis { LabelsPaint = new SolidColorPaint(SKColors.Gray), TextSize = 11, Labeler = val => $"₱{val:N0}" } };
            chart.Series = new ISeries[] { isLineChart ? CreateLineSeries(kpi) : CreateColumnSeries(kpi) };
            chart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Top;
        }

        public static void LoadBestSellersChart(CartesianChart chart, KpiData kpi)
        {
            if (kpi.TopProductNames.Count == 0)
            {
                chart.Series = Array.Empty<ISeries>();
                chart.XAxes = new Axis[] { new Axis { Labels = new[] { "No data" } } };
                return;
            }

            // Create list and sort by sales descending (highest first)
            var products = kpi.TopProductNames.Zip(kpi.TopProductSales, (n, s) => new { Name = n, Sales = s })
                .OrderByDescending(p => p.Sales)
                .ToList();

            // REVERSE for RowSeries (it displays from bottom to top)
            var displaySales = products.Select(p => p.Sales).Reverse().ToList();
            var displayNames = products.Select(p => p.Name).Reverse().ToList();

            // Create labels with correct numbering (1 at top, 10 at bottom)
            var totalCount = products.Count;
            var labelsWithNumbers = displayNames.Select((name, index) => $"{totalCount - index}. {name}").ToList();

            var series = new RowSeries<decimal>
            {
                Name = "Best Selling Products",
                Values = displaySales,
                Fill = new SolidColorPaint(new SKColor(59, 130, 246)),
                Stroke = null,
                Rx = 4,
                Ry = 4,
                DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                DataLabelsSize = 11,
                DataLabelsFormatter = point => $"{point.Coordinate.PrimaryValue:N0} units",
                MaxBarWidth = 30,
                Padding = 6,
            };

            var xAxis = new Axis
            {
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                TextSize = 11,
                Labeler = val => $"{val:N0} units",
                MinStep = 1,
                MinLimit = 0,
            };

            var yAxis = new Axis
            {
                Labels = labelsWithNumbers,
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                TextSize = 11,
                MinStep = 1,
                ForceStepToMin = true,
                LabelsAlignment = LiveChartsCore.Drawing.Align.Start,
                Padding = new LiveChartsCore.Drawing.Padding(5, 10, 0, 10)
            };

            chart.Series = new ISeries[] { series };
            chart.XAxes = new Axis[] { xAxis };
            chart.YAxes = new Axis[] { yAxis };
            chart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Top;
            chart.TooltipPosition = LiveChartsCore.Measure.TooltipPosition.Hidden;
        }

        private static LineSeries<decimal> CreateLineSeries(KpiData kpi) => new()
        {
            Name = "Daily Revenue",
            Values = kpi.DailyRevenue,
            Fill = new SolidColorPaint(new SKColor(59, 130, 246, 40)),
            Stroke = new SolidColorPaint(new SKColor(59, 130, 246)) { StrokeThickness = 2 },
            GeometrySize = 6,
            GeometryFill = new SolidColorPaint(SKColors.White),
            GeometryStroke = new SolidColorPaint(new SKColor(59, 130, 246)) { StrokeThickness = 2 }
        };

        private static ColumnSeries<decimal> CreateColumnSeries(KpiData kpi) => new()
        {
            Name = "Daily Revenue",
            Values = kpi.DailyRevenue,
            Fill = new SolidColorPaint(new SKColor(59, 130, 246)),
            Rx = 4,
            Ry = 4
        };
    }
}