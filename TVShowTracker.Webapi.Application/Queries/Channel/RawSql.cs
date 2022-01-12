namespace TVShowTracker.Webapi.Application.Queries.Channel
{
    public static class RawSql
    {
       public static string GetAll
        { 
            get 
            { 
                return @"
                   SELECT            		cast(ChannelId as CHAR(36)) as ChannelId,
                                            Name,
                                            Namber,
                                            Description,
                                            CreateDate,
                                            UpdateDate
                    FROM 					Channel 
                "; 
            }
        }
    }
}
