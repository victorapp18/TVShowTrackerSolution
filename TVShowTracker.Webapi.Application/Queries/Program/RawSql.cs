namespace TVShowTracker.Webapi.Application.Queries.Program
{
    public static class RawSql
    {
       public static string GetProgramAll
        { 
            get 
            { 
                return @"
                   SELECT            		cast(P.ProgramId as CHAR(36)) as ProgramId,
                                    		cast(P.ChannelId as CHAR(36)) as ChannelId,
                                            G.GenderId,
                                            G.Name,
                                            P.Name,
                                            P.Description,
                                            P.ExhibitionDate,
                                            P.CreateDate,
                                            P.UpdateDate
                    FROM 					Program P
                    INNER JOIN              Gender G on P.GenderId = G.GenderId
                    WHERE                   P.ChannelId = @ChannelId
                "; 
            }
        }
        public static string GetProgramGenderAll
        {
            get
            {
                return @"
                   SELECT            		cast(P.ProgramId as CHAR(36)) as ProgramId,
                                    		cast(P.ChannelId as CHAR(36)) as ChannelId,
                                            G.GenderId,
                                            G.Name,
                                            P.Name,
                                            P.Description,
                                            P.ExhibitionDate,
                                            P.CreateDate,
                                            P.UpdateDate
                    FROM 					Program P
                    INNER JOIN              Gender G on P.GenderId = G.GenderId
                    WHERE                   G.GenderId = @GenderId 
                ";
            }
        }
        public static string GetGenders
        {
            get
            {
                return @"
                   SELECT            		cast(GenderId as CHAR(36)) as GenderId,
                                            Name,
                                            Description
                    FROM 					gender 
                ";
            }
        }
    }
}
