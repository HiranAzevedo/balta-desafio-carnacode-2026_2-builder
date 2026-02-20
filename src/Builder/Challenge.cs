//// DESAFIO: Gerador de Relatórios Complexos
//// PROBLEMA: Sistema precisa gerar diferentes tipos de relatórios (PDF, Excel, HTML)
//// com múltiplas configurações opcionais (cabeçalho, rodapé, gráficos, tabelas, filtros)
//// O código atual usa construtores enormes ou muitos setters, tornando difícil criar relatórios

//namespace DesignPatternChallenge
//{
//    // Contexto: Sistema de BI que gera relatórios customizados para diferentes departamentos
//    // Cada relatório pode ter dezenas de configurações opcionais

//    public class SalesReport
//    {
//        public string Title { get; set; }
//        public string Format { get; set; }
//        public DateTime StartDate { get; set; }
//        public DateTime EndDate { get; set; }
//        public bool IncludeHeader { get; set; }
//        public bool IncludeFooter { get; set; }
//        public string HeaderText { get; set; }
//        public string FooterText { get; set; }
//        public bool IncludeCharts { get; set; }
//        public string ChartType { get; set; }
//        public bool IncludeSummary { get; set; }
//        public List<string> Columns { get; set; }
//        public List<string> Filters { get; set; }
//        public string SortBy { get; set; }
//        public string GroupBy { get; set; }
//        public bool IncludeTotals { get; set; }
//        public string Orientation { get; set; }
//        public string PageSize { get; set; }
//        public bool IncludePageNumbers { get; set; }
//        public string CompanyLogo { get; set; }
//        public string WaterMark { get; set; }

//        // Problema: Construtor telescópico (muitos parâmetros)
//        public SalesReport(
//            string title,
//            string format,
//            DateTime startDate,
//            DateTime endDate,
//            bool includeHeader,
//            bool includeFooter,
//            string headerText,
//            string footerText,
//            bool includeCharts,
//            string chartType,
//            bool includeSummary,
//            List<string> columns,
//            List<string> filters,
//            string sortBy,
//            string groupBy,
//            bool includeTotals,
//            string orientation,
//            string pageSize,
//            bool includePageNumbers,
//            string companyLogo,
//            string waterMark)
//        {
//            Title = title;
//            Format = format;
//            StartDate = startDate;
//            EndDate = endDate;
//            IncludeHeader = includeHeader;
//            IncludeFooter = includeFooter;
//            HeaderText = headerText;
//            FooterText = footerText;
//            IncludeCharts = includeCharts;
//            ChartType = chartType;
//            IncludeSummary = includeSummary;
//            Columns = columns;
//            Filters = filters;
//            SortBy = sortBy;
//            GroupBy = groupBy;
//            IncludeTotals = includeTotals;
//            Orientation = orientation;
//            PageSize = pageSize;
//            IncludePageNumbers = includePageNumbers;
//            CompanyLogo = companyLogo;
//            WaterMark = waterMark;
//        }

//        // Alternativa problemática: Construtor vazio + setters
//        public SalesReport()
//        {
//            Columns = new List<string>();
//            Filters = new List<string>();
//        }

//        public void Generate()
//        {
//            Console.WriteLine($"\n=== Gerando Relatório: {Title} ===");
//            Console.WriteLine($"Formato: {Format}");
//            Console.WriteLine($"Período: {StartDate:dd/MM/yyyy} a {EndDate:dd/MM/yyyy}");

//            if (IncludeHeader)
//                Console.WriteLine($"Cabeçalho: {HeaderText}");

//            if (IncludeCharts)
//                Console.WriteLine($"Gráfico: {ChartType}");

//            Console.WriteLine($"Colunas: {string.Join(", ", Columns)}");

//            if (Filters.Count > 0)
//                Console.WriteLine($"Filtros: {string.Join(", ", Filters)}");

//            if (!string.IsNullOrEmpty(GroupBy))
//                Console.WriteLine($"Agrupado por: {GroupBy}");

//            if (IncludeFooter)
//                Console.WriteLine($"Rodapé: {FooterText}");

//            Console.WriteLine("Relatório gerado com sucesso!");
//        }
//    }

//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("=== Sistema de Relatórios ===");

//            // Problema 1: Construtor com muitos parâmetros - difícil de ler e usar
//            var report1 = new SalesReport(
//                "Vendas Mensais",           // title
//                "PDF",                       // format
//                new DateTime(2024, 1, 1),   // startDate
//                new DateTime(2024, 1, 31),  // endDate
//                true,                        // includeHeader
//                true,                        // includeFooter
//                "Relatório de Vendas",      // headerText
//                "Confidencial",              // footerText
//                true,                        // includeCharts
//                "Bar",                       // chartType
//                true,                        // includeSummary
//                new List<string> { "Produto", "Quantidade", "Valor" },  // columns
//                new List<string> { "Status=Ativo" },  // filters
//                "Valor",                     // sortBy
//                "Categoria",                 // groupBy
//                true,                        // includeTotals
//                "Portrait",                  // orientation
//                "A4",                        // pageSize
//                true,                        // includePageNumbers
//                "logo.png",                  // companyLogo
//                "Confidencial"               // waterMark
//            );

//            report1.Generate();

//            // Problema 2: Muitos setters - ordem não importa, pode esquecer configurações obrigatórias
//            var report2 = new SalesReport();
//            report2.Title = "Relatório Trimestral";
//            report2.Format = "Excel";
//            report2.StartDate = new DateTime(2024, 1, 1);
//            report2.EndDate = new DateTime(2024, 3, 31);
//            report2.Columns.Add("Vendedor");
//            report2.Columns.Add("Região");
//            report2.Columns.Add("Total");
//            report2.IncludeCharts = true;
//            report2.ChartType = "Line";
//            // Esqueci de configurar algo? O código compila mas pode falhar em runtime
//            report2.IncludeHeader = true;
//            // Esqueci o HeaderText? 
//            report2.GroupBy = "Região";
//            report2.IncludeTotals = true;

//            report2.Generate();

//            // Problema 3: Relatórios com configurações parecidas exigem repetir muito código
//            var report3 = new SalesReport();
//            report3.Title = "Vendas Anuais";
//            report3.Format = "PDF";
//            report3.StartDate = new DateTime(2024, 1, 1);
//            report3.EndDate = new DateTime(2024, 12, 31);
//            report3.IncludeHeader = true;
//            report3.HeaderText = "Relatório de Vendas";
//            report3.IncludeFooter = true;
//            report3.FooterText = "Confidencial";
//            report3.Columns.Add("Produto");
//            report3.Columns.Add("Quantidade");
//            report3.Columns.Add("Valor");
//            report3.IncludeCharts = true;
//            report3.ChartType = "Pie";
//            report3.IncludeTotals = true;
//            report3.Orientation = "Landscape";
//            report3.PageSize = "A4";

//            report3.Generate();

//            // Perguntas para reflexão:
//            // - Como criar relatórios complexos sem construtores gigantes?
//            // - Como garantir que configurações obrigatórias sejam definidas?
//            // - Como reutilizar configurações comuns entre relatórios?
//            // - Como tornar o processo de criação mais legível e fluente?
//        }
//    }
//}
