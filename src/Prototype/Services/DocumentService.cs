using Models;

namespace Services
{
    public class DocumentService
    {
        private readonly DocumentTemplate _serviceTemplate;
        private readonly DocumentTemplate _consultingTemplate;

        public DocumentService()
        {
            Console.WriteLine("Inicializando templates base...");

            // Cria os templates base apenas 1 vez (custoso)
            _serviceTemplate = CreateServiceContractInternal();
            _consultingTemplate = CreateConsultingContractInternal();

            Console.WriteLine("Templates prontos para uso!\n");
        }
        private DocumentTemplate CreateServiceContractInternal()
        {
            Console.WriteLine("  → Criando template de Serviço...");
            System.Threading.Thread.Sleep(100); // Simulando processo custoso

            var template = new DocumentTemplate
            {
                Title = "Contrato de Prestação de Serviços",
                Category = "Contratos",
                Style = new DocumentStyle
                {
                    FontFamily = "Arial",
                    FontSize = 12,
                    HeaderColor = "#003366",
                    LogoUrl = "https://company.com/logo.png",
                    PageMargins = new Margins { Top = 2, Bottom = 2, Left = 3, Right = 3 }
                },
                Workflow = new ApprovalWorkflow
                {
                    RequiredApprovals = 2,
                    TimeoutDays = 5
                }
            };

            template.Workflow.Approvers.Add("gerente@empresa.com");
            template.Workflow.Approvers.Add("juridico@empresa.com");

            template.Sections.Add(new Section
            {
                Name = "Cláusula 1 - Objeto",
                Content = "O presente contrato tem por objeto...",
                IsEditable = true
            });
            template.Sections.Add(new Section
            {
                Name = "Cláusula 2 - Prazo",
                Content = "O prazo de vigência será de...",
                IsEditable = true
            });
            template.Sections.Add(new Section
            {
                Name = "Cláusula 3 - Valor",
                Content = "O valor total do contrato é de...",
                IsEditable = true
            });

            template.RequiredFields.Add("NomeCliente");
            template.RequiredFields.Add("CPF");
            template.RequiredFields.Add("Endereco");

            template.Tags.Add("contrato");
            template.Tags.Add("servicos");

            template.Metadata["Versao"] = "1.0";
            template.Metadata["Departamento"] = "Comercial";
            template.Metadata["UltimaRevisao"] = DateTime.Now.ToString();

            return template;
        }
        private DocumentTemplate CreateConsultingContractInternal()
        {
            Console.WriteLine("Criando template de Consultoria...");

            var template = _serviceTemplate.Clone();

            template.Title = "Contrato de Consultoria";

            template.Tags.Remove("servicos");
            template.Tags.Add("consultoria");

            template.Sections[0].Content = "O presente contrato de consultoria tem por objeto...";

            template.Sections.RemoveAt(2);

            return template;
        }
        public DocumentTemplate CreateServiceContract() => _serviceTemplate.Clone();
        public DocumentTemplate CreateConsultingContract() => _consultingTemplate.Clone();
        public void DisplayTemplate(DocumentTemplate template)
        {
            Console.WriteLine($"\n=== {template.Title} ===");
            Console.WriteLine($"Categoria: {template.Category}");
            Console.WriteLine($"Seções: {template.Sections.Count}");
            Console.WriteLine($"Campos obrigatórios: {string.Join(", ", template.RequiredFields)}");
            Console.WriteLine($"Aprovadores: {string.Join(", ", template.Workflow.Approvers)}");
        }
    }
}