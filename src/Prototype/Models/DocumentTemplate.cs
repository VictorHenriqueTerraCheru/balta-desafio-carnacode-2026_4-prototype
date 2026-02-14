using Interfaces;

namespace Models
{
    public class DocumentTemplate : IPrototype<DocumentTemplate>
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public List<Section> Sections { get; set; }
        public DocumentStyle Style { get; set; }
        public List<string> RequiredFields { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public ApprovalWorkflow Workflow { get; set; }
        public List<string> Tags { get; set; }

        public DocumentTemplate()
        {
            Sections = new List<Section>();
            RequiredFields = new List<string>();
            Metadata = new Dictionary<string, string>();
            Tags = new List<string>();
        }

        public DocumentTemplate Clone()
        {
            var clone = (DocumentTemplate)this.MemberwiseClone();

            clone.Sections = new List<Section>();
            foreach (var section in this.Sections)
                clone.Sections.Add(section.Clone());

            clone.RequiredFields = new List<string>(this.RequiredFields);

            clone.Tags = new List<string>(this.Tags);

            clone.Metadata = new Dictionary<string, string>(this.Metadata);

            clone.Style = new DocumentStyle
            {
                FontFamily = this.Style.FontFamily,
                FontSize = this.Style.FontSize,
                HeaderColor = this.Style.HeaderColor,
                LogoUrl = this.Style.LogoUrl,
                PageMargins = new Margins
                {
                    Top = this.Style.PageMargins.Top,
                    Bottom = this.Style.PageMargins.Bottom,
                    Left = this.Style.PageMargins.Left,
                    Right = this.Style.PageMargins.Right
                }
            };

            clone.Workflow = new ApprovalWorkflow
            {
                Approvers = new List<string>(this.Workflow.Approvers),
                RequiredApprovals = this.Workflow.RequiredApprovals,
                TimeoutDays = this.Workflow.TimeoutDays
            };

            return clone;
        }
    }
}