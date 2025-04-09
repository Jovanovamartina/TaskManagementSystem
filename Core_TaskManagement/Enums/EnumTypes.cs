
namespace Core_TaskManagement.Enums
{
    public static class EnumTypes
    {
        public enum ProjectStatus
        {
            InProgress = 1,
            Completed = 2,
            OnHold = 3
        }

        public enum ProjectPrioriry
        {
            High = 1,
            Medium = 2,
            Low = 3
        }

        public enum IssueType
        {
            Maintenance = 1,
            Bug = 2,
            ChangeRequest = 3,
            Preparation = 4
        }

        public enum IssueStatus
        {
            New = 1,
            Discuss = 2,
            Working = 3,
            Done = 4,
            OnHold = 5,
            Finished = 6,
            Rejected = 7
        }

        public enum IssuePriority
        {
            Low = 1,
            Normal = 2,
            High = 3,
            Urgent = 4,
            Immediate = 5
        }

        public enum Module
        {
            AccountingAndFinnance = 1,
            Accounts = 2,
            Budget = 3,
            FundManagement = 4,
            General = 5,
            HumanResources = 6,
            Insurance = 7,
            Invoicing = 8,
            InventoryManagement = 9,
            Reports = 10
        }
        public enum ActivityType
        {
            Analuze = 1,
            Maintenance = 2,
            Bug = 3,
            ChangeRequest = 4, 
            Education = 5, 
            training = 6,
            preparation = 7,
            PaidLeave = 8, 
            businessTrip = 9,
            Development = 10,
            Testing = 11,
            Design = 12,
            Documentation = 13,
            Meeting = 14,
            CodeReview = 15,
        }

        public enum WorkLocation
        {
            Office = 1,
            OutOfOffice =  2
        }
    }
}
