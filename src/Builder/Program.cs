using Builder;
using Builder.Model;

var report1 =
    SalesReportPresets.StandardPdf("Vendas Mensais", new DateTime(2024, 1, 1), new DateTime(2024, 1, 31))
        .AddColumns("Produto", "Quantidade", "Valor")
        .AddFilter("Status=Ativo")
        .WithCharts("Bar")
        .WithSummary()
        .GroupBy("Categoria")
        .WithTotals()
        .WithCompanyLogo("logo.png")
        .WithWatermark("Confidencial")
        .Build();

report1.Generate();

var report2 =
    SalesReportPresets.StandardExcel("Relatório Trimestral", new DateTime(2024, 1, 1), new DateTime(2024, 3, 31))
        .AddColumns("Vendedor", "Região", "Total")
        .WithCharts("Line")
        .GroupBy("Região")
        .WithTotals()
        .Build();

report2.Generate();

var report3 =
    SalesReportPresets.StandardPdf("Vendas Anuais", new DateTime(2024, 1, 1), new DateTime(2024, 12, 31))
        .AddColumns("Produto", "Quantidade", "Valor")
        .WithCharts("Pie")
        .WithTotals()
        .Layout(Orientation.Landscape, PageSize.A4)
        .Build();

report3.Generate();