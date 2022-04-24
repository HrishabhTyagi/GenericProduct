using Common;
using Common.ExtendedFunction;
using SqlServerEntity.EntityModel;

namespace DAL
{
    public class BaseDataAccess
    {
        protected readonly DatabaseContext _context;
        protected MemoryCacher cache;

        public BaseDataAccess()
        {
            _context = new DatabaseContext();
            cache = new MemoryCacher();
        }

        public class CacheKeys
        {
            public const string User = "Users";
            public const string Portfolio = "Portfolio";
            public const string Domain = "Domain";            
            public const string Country = "Country";
            public const string PortfolioUserMapping = "PortfolioUserMapping";
            public const string TargetCloud = "TargetCloud";
            public const string UploadedFile = "UploadedFile";
            public const string PortfolioCloudMapping = "PortfolioCloudMapping";
            public const string Account = "Account";
            public const string OfferingMaster = "OfferingMaster";
            public const string ServiceMaster = "ServiceMaster";
            public const string OfferingServiceMappingMaster = "OfferingServiceMappingMaster";
            public const string OfferingAdminMapping = "OfferingAdminMapping";
            public const string Project = "Project";
            public const string OperationMaster = "OperationMaster";
            public const string UserType = "UserType";
            public const string ProjectOfferingServiceMapping = "ProjectOfferingServiceMapping";
            public const string ProjectOfferingServiceLeadMapping = "ProjectOfferingServiceLeadMapping";
            public const string DepartmentMaster = "DepartmentMaster";
            public const string TeamMemberRoleMaster = "TeamMemberRoleMaster";
            public const string Team = "Team";
            public const string AccessControlList = "AccessControlList";
    }


    public string CreateProcedureParamString(string procedureName, int parameterCount)
    {
        string formattedProcParam = procedureName + " " + "@p0";

        if (parameterCount == 1)
            return formattedProcParam;

        if (!procedureName.IsNullOrWhitespaceOrEmpty())
        {
            for (int i = 0; i < parameterCount; i++)
            {
                if (i == parameterCount - 1)
                    formattedProcParam += "@p" + i;
                else
                    formattedProcParam += "@p" + i + ",";
            }
        }
        return formattedProcParam;
    }
}
}
