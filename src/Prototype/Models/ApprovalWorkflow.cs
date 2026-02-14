namespace Models
{
    public class ApprovalWorkflow
    {
        public List<string> Approvers { get; set; }
        public int RequiredApprovals { get; set; }
        public int TimeoutDays { get; set; }

        public ApprovalWorkflow()
        {
            Approvers = new List<string>();
        }
    }
}