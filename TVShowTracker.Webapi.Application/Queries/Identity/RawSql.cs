namespace TVShowTracker.Webapi.Application.Queries.Identity
{
    public static class RawSql
    {
       public static string GetIdentity
        { 
            get 
            { 
                return @"
                   SELECT            		cast(I.IdentityId as CHAR(36)) as IdentityId, 
                                            I.Name, 
                                            I.Username, 
                                            I.Password, 
                                            I.CreateDate, 
                                            I.Contact, 
                                            I.Image,
                                            R.Description as 'RoleDescription',
                                            R.RoleId,
                                            R.Name as 'RoleName',
											I.IsAccessExternal
                    FROM 					Identity I
                    JOIN                    IdentityRole IR on I.IdentityId = IR.IdentityId
                    JOIN                    Role R on IR.RoleId = R.RoleId
                    WHERE                   I.Username = @Username and I.IdentityId = @IdentityId
                "; 
            }
        }

        public static string GetProviderIdentity
        {
            get
            {
                return @"
                   SELECT            		cast(I.IdentityId as CHAR(36)) as IdentityId, 
                                            I.Name, 
                                            I.Username, 
                                            I.Password, 
                                            I.CreateDate, 
                                            I.Contact, 
                                            I.Image,
                                            R.Description as 'RoleDescription',
                                            R.RoleId,
                                            R.Name as 'RoleName',
											TP.TypeProviderId as 'ProviderId',
                                            TP.Name as 'ProviderName'
                    FROM 					Identity I
                    JOIN                    IdentityRole IR on I.IdentityId = IR.IdentityId
                    JOIN                    Role R on IR.RoleId = R.RoleId
                    JOIN                    Provider P on P.IdentityId = I.IdentityId
                    JOIN                    TypeProvider TP on TP.TypeProviderId = P.TypeProviderId
                    WHERE                   I.Username = @Username and I.IdentityId = @IdentityId
                ";
            }
        }

        public static string GetRoles
        {
            get
            {
                return @"
                   SELECT                   R.Description as 'RoleDescription',
                                            R.RoleId,
                                            R.Name as 'RoleName'                               
                    FROM                    Role R
                    WHERE                   R.RoleId <> 1 
                ";
            }
        }
    }
}
